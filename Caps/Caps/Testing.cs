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
		KeyboardHook kh = new KeyboardHook();
		public void start()
		{
			
			kh.KeyDown += KeyDown;
			kh.Hook();
		}

		private void KeyDown(object sender, KeyBoard.Structures.KeyboardHookEventArgs e)
		{
			
		}
	}
}
