using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caps.ClipBoard.Structures;

namespace Caps.ClipBoard
{
	public class Clipboard:IDisposable
	{
		internal ConcurrentStack<IDataObject> ObjectStack = new ConcurrentStack<IDataObject>();
		internal BlockingCollection<Command> Commands = new BlockingCollection<Command>(1);
		internal BlockingCollection<bool> Returns = new BlockingCollection<bool>();
		internal string GetString = "";
		internal string SetString = "";
		internal Thread ClipboardDaemon;

		public Clipboard()
		{
			ClipboardDaemon = new Thread(EventLoop);
			ClipboardDaemon.SetApartmentState(ApartmentState.STA);
			ClipboardDaemon.IsBackground = true;
			ClipboardDaemon.Start();

		}

		internal void EventLoop()
		{
			UIPermission clipboard = new UIPermission(PermissionState.None);
			clipboard.Clipboard = UIPermissionClipboard.AllClipboard;
			while (true)
			{
				Command c = Commands.Take();
				switch (c)
				{
					case Command.Push:
						IDataObject obj = HandleOleApi.GetDataObject();
						var formats = obj.GetFormats();
						IDataObject newObj = new DataObject();
						foreach (string format in formats)
						{
							object d;
							try
							{
								d = obj.GetData(format,false);
							}
							catch (OutOfMemoryException)
							{
								d = null;
							}
							catch (ExternalException)
							{
								d = null;
							}
							if (d != null)
							{
								newObj.SetData(format,d );
							}
						}
						ObjectStack.Push(newObj);
						Returns.Add(true);
						break;
					case Command.Pop:
						IDataObject iobj;
						if (ObjectStack.TryPop(out iobj))
						{
							HandleOleApi.SetDataObject(iobj);
						}
						Returns.Add(true);
						break;
					case Command.GetText:
						GetString = HandleOleApi.GetText();
						Returns.Add(true);
						break;
					case Command.SetText:
						HandleOleApi.SetText(SetString);
						Returns.Add(true);
						break;
					case Command.Exit:
						return;
					default:
						throw new Exception("No such command");
				}
			}
		}

		public bool Push()
		{
			Thread.Sleep(50);
			Commands.Add(Command.Push);
			return Returns.Take();
		}

		public bool Pop()
		{
			Commands.Add(Command.Pop);
			return Returns.Take();
		}

		public string GetText()
		{
			Thread.Sleep(50);
			Commands.Add(Command.GetText);
			if (Returns.Take())
			{
				return GetString;
			}
			return "";
		}

		public bool SetText(string s)
		{
			SetString = s;
			Commands.Add(Command.SetText);
			return Returns.Take();
		}

		public void Dispose()
		{
			Commands.Add(Command.Exit);
			ObjectStack.Clear();
		}
	}
}
