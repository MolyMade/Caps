using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caps.HotKey;
using static Caps.KeyBoard.Structures.VkCodes;
using static Caps.KeyBoard.Structures.Modifiers;
using static Caps.Core.Utils;
using Clipboard = Caps.ClipBoard.Clipboard;

namespace Caps.Core
{
	public class Core
	{
		protected HotKeyListener HotKeyListener = new HotKeyListener();
		protected MainWindow MainWindow;

		public Core(MainWindow mainWindow)
		{
			MainWindow = mainWindow;
		}

		public void Run()
		{
			HotKeyListener.HotKeyTriggered += OnKey;
		}

		private void OnKey(object sender, HotKey.Structures.HotKeyEventArgs e)
		{
			switch (e.HardCode)
			{
				case VkX: // A stack-like clipboard, cut
					KeyBoard.Send.KeyCombination(VkControl, VkX);
					Delay();
					Clipboard.Push();
					break;
				case VkC: // A stack-like clipboard, copy
					KeyBoard.Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.Push();
					break;
				case VkV: // A stack-like clipboard, paste
					Clipboard.Pop();
					KeyBoard.Send.KeyCombination(VkShift, VkInsert);
					break;
				case VkReturn: //Create a new line wherever cursor is located
					KeyBoard.Send.Key(VkEnd);
					KeyBoard.Send.Key(VkReturn);
					break;
				case VkBack:
					KeyBoard.Send.Key(VkEnd);
					KeyBoard.Send.KeyCombination(VkShift, VkHome);
					KeyBoard.Send.Key(VkBack);
					KeyBoard.Send.Key(VkBack);
					break;
				case Shift | VkBack:
					KeyBoard.Send.Key(VkUp);
					KeyBoard.Send.Key(VkEnd);
					KeyBoard.Send.KeyCombination(VkShift, VkHome);
					KeyBoard.Send.Key(VkBack);
					KeyBoard.Send.Key(VkBack);
					break;
				case VkDelete:
					KeyBoard.Send.Key(VkEnd);
					KeyBoard.Send.KeyCombination(VkShift, VkHome);
					KeyBoard.Send.Key(VkDelete);
					break;
				case Shift | VkDelete:
					KeyBoard.Send.Key(VkUp);
					KeyBoard.Send.Key(VkEnd);
					KeyBoard.Send.KeyCombination(VkShift, VkHome);
					KeyBoard.Send.Key(VkDelete);
					break;
				case VkH:
					KeyBoard.Send.Key(VkLeft);
					break;
				case VkJ:
					KeyBoard.Send.Key(VkDown);
					break;
				case VkK:
					KeyBoard.Send.Key(VkUp);
					break;
				case VkL:
					KeyBoard.Send.Key(VkRight);
					break;
				case VkQ:
					MainWindow.Context.Send(MainWindow.ShowCap, null);
					break;
				case VkQuote:
					Clipboard.Push();
					Clipboard.SetText("");
					KeyBoard.Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText($"\"{Clipboard.GetText()}\"");
					KeyBoard.Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
				case VkLBrace:
					Clipboard.Push();
					Clipboard.SetText("");
					KeyBoard.Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText(string.Concat("(", Clipboard.GetText(), ")"));
					KeyBoard.Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
				case VkRBrace:
					Clipboard.Push();
					Clipboard.SetText("");
					KeyBoard.Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText($"{{{Clipboard.GetText()}}}");
					KeyBoard.Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
			}
		}
	}
}
