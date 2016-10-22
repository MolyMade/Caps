using System;

namespace Caps.KeyBoard.Structures
{
    public abstract class HookEventArgs : EventArgs
    {
        protected HookEventType EventType { get; set; }
    }

    public enum HookEventType { Keyboard, Mouse }
}
