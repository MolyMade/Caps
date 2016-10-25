using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.HotKey;
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
		}
	}
}
