using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows;
using Caps.ClipBoard.Structures;

namespace Caps.ClipBoard
{
	public static class Clipboard
	{
		internal static ConcurrentStack<IDataObject> ObjectStack = new ConcurrentStack<IDataObject>();
		internal static BlockingCollection<Command> Commands = new BlockingCollection<Command>(1);
		internal static BlockingCollection<bool> Returns = new BlockingCollection<bool>();
		internal static string GetString = string.Empty;
		internal static string SetString = string.Empty;
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
			while (true)
			{
				var c = Commands.Take();
				switch (c)
				{
					case Command.Push:
						var obj = HandleOleApi.GetDataObject();
						var formats = obj.GetFormats();
						IDataObject newObj = new DataObject();
						foreach (var format in formats)
						{
							object d;
							try
							{
								d = obj.GetData(format, false);
							}
							catch
							{
								d = null;
							}
							if (d != null)
							{
								newObj.SetData(format, d);
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
						Returns.Add(true);
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
			return Returns.Take() ? GetString : string.Empty;
		}

		public static bool SetText(string s)
		{
			SetString = s;
			Commands.Add(Command.SetText);
			return Returns.Take();
		}
	}
}
