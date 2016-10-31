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
	public static class Clipboard
	{
		internal static ConcurrentStack<IDataObject> ObjectStack = new ConcurrentStack<IDataObject>();
		internal static BlockingCollection<Command> Commands = new BlockingCollection<Command>(1);
		internal static BlockingCollection<bool> Returns = new BlockingCollection<bool>();
		private static string GetString = "";
		private static string SetString = "";
		internal static Thread ClipboardDaemon;

		static Clipboard()
		{
			ClipboardDaemon = new Thread(EventLoop);
			ClipboardDaemon.SetApartmentState(ApartmentState.STA);
			ClipboardDaemon.IsBackground = true;
			ClipboardDaemon.Start();

		}

		internal static void EventLoop()
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
							catch
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

		public static bool Push()
		{
			Commands.Add(Command.Push);
			return Returns.Take();
		}

		public static bool Pop()
		{
			Commands.Add(Command.Pop);
			return Returns.Take();
		}

		public static string GetText()
		{
			Commands.Add(Command.GetText);
			if (Returns.Take())
			{
				return GetString;
			}
			return "";
		}

		public static bool SetText(string s)
		{
			SetString = s;
			Commands.Add(Command.SetText);
			return Returns.Take();
		}
	}
}
