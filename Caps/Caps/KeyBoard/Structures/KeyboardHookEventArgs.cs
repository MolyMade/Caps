using System;

namespace Caps.KeyBoard.Structures
{
    public class KeyboardHookEventArgs : HookEventArgs
    {
        public KeyboardHookEventArgs(KBDLLHOOKSTRUCT lparam)
        {
            EventType = HookEventType.Keyboard;
            LParam = lparam;
        }

        private KBDLLHOOKSTRUCT lParam;

        private KBDLLHOOKSTRUCT LParam
        {
            get { return lParam; }
            set
            {
                lParam = value;
                var nonVirtual = NativeMethods.MapVirtualKey((uint)VirtualKeyCode, 2);
                Char = Convert.ToChar(nonVirtual);
            }
        }

        public int VirtualKeyCode { get { return LParam.VkCode; } }

        public char Char { get; private set; }

        public KeyboardEventNames KeyboardEventName { get; internal set; }
    }
}
