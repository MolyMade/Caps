using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using Caps.HotKey;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;
using IDataObject = System.Windows.Forms.IDataObject;

namespace Caps
{
	public class Testing
	{
		private HotKeyListener hl;
		private ClipBoard.ClipBoard _clipBoard;
		private String X;
		internal ConcurrentStack<System.Windows.Forms.IDataObject> DataObjectStack = new ConcurrentStack<IDataObject>();
		public IntPtr Hwnd;
		private ClipBoard.Structures.ClipBoardDataObject cbo;

		public Testing(IntPtr i)
		{
			Hwnd = i;
			_clipBoard = new ClipBoard.ClipBoard(i);
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
				Thread.Sleep(100);
				_clipBoard.Push();

			}
			else if (e.Key == VkCodes.VK_V)
			{
				_clipBoard.Pop();
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_V);
			}
		}
	}
}
