using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.KeyBoard.Structures;

namespace Caps.KeyBoard
{
	public static class KeyboardSend
	{
		public static void KeyDown(byte visualKeyCode)=> 
			NativeMethods.keybd_event(visualKeyCode, 0, KeyEvents.KEYEVENTF_KEYDOWN, 0);

		public static void KeyUp(byte visualKeyCode)=>
			NativeMethods.keybd_event(visualKeyCode, 0, KeyEvents.KEYEVENTF_KEYUP, 0);	
	}
}