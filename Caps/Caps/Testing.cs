using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.HotKey;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps
{
	public class Testing
	{
		private HotKeyListener hl;



		public void start()
		{
			hl = new HotKeyListener();
		//	KeyboardSend.Key(VkCodes.VK_CAPITAL);
		hl.HotKeyTriggered+= HlOnHotKeyTriggered;
		}

		private void HlOnHotKeyTriggered(object sender, HotKeyEventArgs e)
		{
			if (e.Key == VkCodes.VK_X)
			{
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL,VkCodes.VK_A);
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL,VkCodes.VK_C);
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL,VkCodes.VK_V);
				KeyboardSend.KeyCombination(VkCodes.VK_LCONTROL, VkCodes.VK_V);
			}
		}
	}
}
