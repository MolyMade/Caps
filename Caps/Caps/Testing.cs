using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		private ClipBoard.ClipBoard _clipBoard = new ClipBoard.ClipBoard();
		private String X;
		internal ConcurrentStack<System.Windows.Forms.IDataObject> DataObjectStack = new ConcurrentStack<IDataObject>();
		public IntPtr Hwnd;
		public void start()
		{
			hl = new HotKeyListener();
			hl.HotKeyTriggered += HlOnHotKeyTriggered;
		}

		private void HlOnHotKeyTriggered(object sender, HotKeyEventArgs e)
		{
			if (e.Key == VkCodes.VK_C)
			{
				List<string> l = new List<string>();
				List<uint> u = new List<uint>();
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_C);
				bool x =ClipBoard.NativeMethods.OpenClipboard(Hwnd);
				if (x)
				{
					uint i = 0;
					do
					{
						i = ClipBoard.NativeMethods.EnumClipboardFormats(i);
						StringBuilder sb = new StringBuilder();
						ClipBoard.NativeMethods.GetClipboardFormatName(i, sb, 100);
						l.Add(sb.ToString());
						u.Add(i);
					} while (i!=0);
					foreach (uint u1 in u)
					{
						var ptr = ClipBoard.NativeMethods.GetClipboardData(u1);
					}
				}

			}
			else if (e.Key == VkCodes.VK_V)
			{
				IDataObject ida;
				if (DataObjectStack.TryPop(out ida))
				{
					Clipboard.Clear();
					Clipboard.SetDataObject(ida,false,200,10);
				}
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_V);
			}
		}
	}
}
