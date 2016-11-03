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
			Shift = IsKeyDown(VkLshift) || IsKeyDown(VkRshift);
			Ctrl = IsKeyDown(VkLcontrol) || IsKeyDown(VkRcontrol);
			Alt = IsKeyDown(VkLmenu) || IsKeyDown(VkRmenu);
			Win = IsKeyDown(VkLwin) || IsKeyDown(VkRwin);
		}

		private static bool IsKeyDown(int key)
		{
			short retVal = NativeMethods.GetKeyState((int)key);
			return (retVal & 0x8000) == 0x8000;
		}
	}
}
