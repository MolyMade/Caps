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
using  Caps.KeyBoard;

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
					Send.KeyCombination(VkControl, VkX);
					Delay();
					Clipboard.Push();
					break;
				case VkC: // A stack-like clipboard, copy
					Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.Push();
					break;
				case VkV: // A stack-like clipboard, paste
					Clipboard.Pop();
					Send.KeyCombination(VkShift, VkInsert);
					break;
				case VkReturn: //Create a new line wherever cursor is located
					Send.Key(VkEnd);
					Send.Key(VkReturn);
					break;
				case VkBack: //Delete the whole line
					Send.Key(VkEnd);
					Send.KeyCombination(VkShift, VkHome);
					Send.Key(VkBack);
					break;
				case Shift | VkBack: //Delete previous line
					Send.Key(VkUp);
					Send.Key(VkEnd);
					Send.KeyCombination(VkShift, VkHome);
					Send.Key(VkBack);
					Send.Key(VkBack);
					break;
				case VkH: //Left arrow
					Send.Key(VkLeft);
					break;
				case VkJ: //Down arrow
					Send.Key(VkDown);
					break;
				case VkK: //Up arrow
					Send.Key(VkUp);
					break;
				case VkL: //Right arrow
					Send.Key(VkRight);
					break;
				case VkQ: //Show Cap window
					MainWindow.Context.Send(MainWindow.ShowCap, null);
					break;
				case VkQuote: //Add selected text ("")
					Clipboard.Push();
					Clipboard.SetText("");
					Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText($"\"{Clipboard.GetText()}\"");
					Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
				case VkLBrace://Add selected text (())
					Clipboard.Push();
					Clipboard.SetText("");
					Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText(string.Concat("(", Clipboard.GetText(), ")"));
					Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
				case VkRBrace://Add selected text ({})
					Clipboard.Push();
					Clipboard.SetText("");
					Send.KeyCombination(VkControl, VkInsert);
					Delay();
					Clipboard.SetText($"{{{Clipboard.GetText()}}}");
					Send.KeyCombination(VkControl, VkV);
					Delay();
					Clipboard.Pop();
					break;
				case VkEscape: //Exit Application
					this.MainWindow.Context.Post(x => { this.MainWindow.Close(); }, null);
					break;

			}
		}
	}
}
