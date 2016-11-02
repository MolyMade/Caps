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
				case VK_X:    // A stack-like clipboard,cut
					KeyBoard.Send.KeyCombination(VK_CONTROL, VK_X);
					Thread.Sleep(50);
					Clipboard.Push();
					break;
				case VK_C:    // A stack-like clipboard,copy
					KeyBoard.Send.KeyCombination(VK_CONTROL, VK_INSERT);
					Thread.Sleep(50);
					Clipboard.Push();
					break;
				case VK_V:    // A stack-like clipboard,paste
					Clipboard.Pop();
					KeyBoard.Send.KeyCombination(VK_SHIFT, VK_INSERT);
					break;
			}
		}
	}
}
