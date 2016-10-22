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
		public static async void KeyDown(byte virtualKeyCode)
		{
			NativeMethods.keybd_event(virtualKeyCode, 0, KeyEvents.KEYEVENTF_KEYDOWN, 0);
		}

		public static void KeyUp(byte virtualKeyCode)
		{
			NativeMethods.keybd_event(virtualKeyCode, 0, KeyEvents.KEYEVENTF_KEYUP, 0);
		}

		public static void KeyClick(byte virtualKeyCode)
		{
			KeyDown(virtualKeyCode);
			KeyUp(virtualKeyCode);
		}

		
	}
}