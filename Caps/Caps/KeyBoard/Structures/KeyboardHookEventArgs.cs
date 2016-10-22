using System;

namespace Caps.KeyBoard.Structures
{
    public class KeyboardHookEventArgs
    {
        public KeyboardHookEventArgs(KBDLLHOOKSTRUCT lparam,KeyboardMessages keyboardMessage)
        {
            this._lParam = lparam;
	        this.KeyboardMessages = keyboardMessage;
        }
        private KBDLLHOOKSTRUCT _lParam;
	    public KeyboardMessages KeyboardMessages;
	    public int VirtualKeyCode => _lParam.VkCode;
	    public uint ScanCode => _lParam.ScanCode;
	    public uint Flag => _lParam.Flags;
	    public uint Time => _lParam.Time;
	    public char Char => Convert.ToChar(NativeMethods.MapVirtualKey((uint) VirtualKeyCode, 2));
    }
}
