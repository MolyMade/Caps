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
using Caps.ClipBoard;
using Clipboard = Caps.ClipBoard.Clipboard;

namespace Caps.Core
{
	public class Core
	{
		protected HotKeyListener HotKeyListener = new HotKeyListener();

		public Core()
		{

		}

		public void Run()
		{
			HotKeyListener.HotKeyTriggered += OnKey;
		}

		private void OnKey(object sender, HotKey.Structures.HotKeyEventArgs e)
		{
			switch (e.HardCode)
			{
				case VK_X:    // A stack-like clipboard, cut
					KeyBoard.Send.KeyCombination(VK_CONTROL, VK_X);
					Thread.Sleep(50);
					Clipboard.Push();
					break;
				case VK_C:    // A stack-like clipboard, copy
					KeyBoard.Send.KeyCombination(VK_CONTROL, VK_INSERT);
					Thread.Sleep(50);
					Clipboard.Push();
					break;
				case VK_V:    // A stack-like clipboard, paste
					Clipboard.Pop();
					KeyBoard.Send.KeyCombination(VK_SHIFT, VK_INSERT);
					break;
				case VK_RETURN: //Create a new line wherever cursor is located
					KeyBoard.Send.Key(VK_END);
					KeyBoard.Send.Key(VK_RETURN);
					break;
				case VK_BACK:
					KeyBoard.Send.Key(VK_END);
					KeyBoard.Send.KeyCombination(VK_SHIFT,VK_HOME);
					KeyBoard.Send.Key(VK_BACK);
					KeyBoard.Send.Key(VK_BACK);
					break;
				case VK_DELETE:
					KeyBoard.Send.Key(VK_END);
					KeyBoard.Send.KeyCombination(VK_SHIFT, VK_HOME);
					KeyBoard.Send.Key(VK_DELETE);
					break;
				case VK_H: 
					KeyBoard.Send.Key(VK_LEFT);
					break;
				case VK_J:
					KeyBoard.Send.Key(VK_DOWN);
					break;
				case VK_K:
					KeyBoard.Send.Key(VK_UP);
					break;
				case VK_L:
					KeyBoard.Send.Key(VK_RIGHT);
					break;
			}
		}
	}
}
