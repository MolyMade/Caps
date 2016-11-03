using System.Windows.Input;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;
using static Caps.KeyBoard.Structures.VkCodes;

namespace Caps.HotKey.Structures
{
	public class ModifierKeyState
	{
		public bool CapsLock;
		public bool Shift;
		public bool Ctrl;
		public bool Alt;
		public bool Win;

		public ModifierKeyState()
		{
			Shift = IsKeyDown(VK_LSHIFT) || IsKeyDown(VK_RSHIFT);
			Ctrl = IsKeyDown(VK_LCONTROL) || IsKeyDown(VK_RCONTROL);
			Alt = IsKeyDown(VK_LMENU) || IsKeyDown(VK_RMENU);
			Win = IsKeyDown(VK_LWIN) || IsKeyDown(VK_RWIN);
		}

		private static bool IsKeyDown(int key)
		{
			short retVal = NativeMethods.GetKeyState((int)key);
			return (retVal & 0x8000) == 0x8000;
		}
	}
}
