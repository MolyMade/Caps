using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps
{
	public class Testing
	{
		KeyboardHook kh = new KeyboardHook((KeyboardEventCallback));

		private static bool KeyboardEventCallback(int vkCode,KeyboardMessages kbm, uint time)
		{
			if (vkCode == VkCodes.VK_CAPITAL)
			{
				return false;
			}
			return true;
		}

		public void start()
		{
			kh.Hook();
		}
	}
}
