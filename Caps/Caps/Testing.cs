using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Caps.ClipBoard;
using Caps.HotKey;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps
{
	public class Testing
	{
		private HotKeyListener hl;
		private String X;
		public IntPtr Hwnd;
		private ClipBoard.Clipboard c;
		public IDataObject id;

		public Testing(IntPtr i)
		{
			c = new ClipBoard.Clipboard();
			this.start();
		}

		public void start()
		{
			hl = new HotKeyListener();
			hl.HotKeyTriggered += HlOnHotKeyTriggered;
		}

		private void HlOnHotKeyTriggered(object sender, HotKeyEventArgs e)
		{
			if (e.Key == VkCodes.VK_C)
			{
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_C);
				Thread.Sleep(50);
				var a =c.Push();

			}
			else if (e.Key == VkCodes.VK_V)
			{
				c.Pop();
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_V);
			}
		}
	}
}
