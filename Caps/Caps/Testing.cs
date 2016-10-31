using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
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
		public IDataObject id;

		public Testing(IntPtr i)
		{
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
				
				Task.Run(() =>
				{
					KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_C);
					ClipBoard.Clipboard.Push();
				});

			}
			else if (e.Key == VkCodes.VK_V)
			{
				Task.Run(() =>
				{
					ClipBoard.Clipboard.Pop();
					KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_V);
				});

			}
		}
	}
}
