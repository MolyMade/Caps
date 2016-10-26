using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caps.HotKey;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps
{
	public class Testing
	{
		private HotKeyListener hl;
		private ClipBoard.ClipBoard _clipBoard = new ClipBoard.ClipBoard();


		public void start()
		{
			hl = new HotKeyListener();
			//	KeyboardSend.Key(VkCodes.VK_CAPITAL);
			hl.HotKeyTriggered += HlOnHotKeyTriggered;
		}

		private void HlOnHotKeyTriggered(object sender, HotKeyEventArgs e)
		{
			if (e.Key == VkCodes.VK_C)
			{
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_C);
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
