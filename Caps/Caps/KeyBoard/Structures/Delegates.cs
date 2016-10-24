using System;

namespace Caps.KeyBoard.Structures
{
    public delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

	public delegate bool KeyboardEventCallback(int vkCode,KeyboardMessages keyboardMessages, uint time);
}
