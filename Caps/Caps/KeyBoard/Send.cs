using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.KeyBoard.Structures;

namespace Caps.KeyBoard
{
	public static class Send
	{
		public static  void KeyDown(byte virtualKeyCode)
		{
			NativeMethods.keybd_event(virtualKeyCode, 0, KeyEvents.KeyeventfKeydown, 0);
		}

		public static void KeyUp(byte virtualKeyCode)
		{
			NativeMethods.keybd_event(virtualKeyCode, 0, KeyEvents.KeyeventfKeyup, 0);
		}

		public static void Key(byte virtualKeyCode)
		{
			KeyDown(virtualKeyCode);
			KeyUp(virtualKeyCode);
		}

		public static void KeyCombination(byte modifier, byte key)
		{
			KeyDown(modifier);
			KeyDown(key);
			KeyUp(key);
			KeyUp(modifier);
		}

		public static void KeyCombination(byte modifier1, byte modifier2, byte key)
		{
			KeyDown(modifier1);
			KeyDown(modifier2);
			KeyDown(key);
			KeyUp(key);
			KeyUp(modifier2);
			KeyUp(modifier1);
		}
		
	}
}