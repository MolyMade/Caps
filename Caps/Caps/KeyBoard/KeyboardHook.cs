using System;
using System.Runtime.InteropServices;

namespace Caps.KeyBoard
{
    public sealed class KeyboardHook : IDisposable
    {
        #region Custom Events

        public event EventHandler<KeyboardHookEventArgs> KeyDown;

        private void OnKeyDown(KeyboardHookEventArgs e)
        {
	        KeyDown?.Invoke(this, e);
	        OnKeyEvent(e);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyUp;

        private void OnKeyUp(KeyboardHookEventArgs e)
        {
	        KeyUp?.Invoke(this, e);
	        OnKeyEvent(e);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyEvent;

        private void OnKeyEvent(KeyboardHookEventArgs e)
        {
	        KeyEvent?.Invoke(this, e);
        }

	    #endregion

        /// <summary>
        /// The hook Id we create. This is stored so we can unhook later.
        /// </summary>
        private IntPtr hookId;
        private readonly LowLevelProc callback;
        private bool hooked;

        public KeyboardHook()
        {
            callback = KeyboardHookCallback;
        }

        public void Hook()
        {
            hookId = Win32.SetWindowsHook(Win32.Hooks.WH_KEYBOARD_LL, callback);
            hooked = true;
        }

        public void Unhook()
        {
            if (!hooked) return;
            NativeMethods.UnhookWindowsHookEx(hookId);
            hooked = false;
        }

        /// <summary>
        /// This is the callback method that is called whenever a low level keyboard event is triggered.
        /// We use it to call our individual custom events.
        /// </summary>
        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var lParamStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var e = new KeyboardHookEventArgs(lParamStruct);
                switch ((KeyboardMessages)wParam)
                {
                    case KeyboardMessages.WmKeydown:
                        e.KeyboardEventName = KeyboardEventNames.KeyDown;
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.WmKeyup:
                        e.KeyboardEventName = KeyboardEventNames.KeyUp;
                        OnKeyUp(e);
                        break;
                    case KeyboardMessages.WmSyskeydown:
                        e.KeyboardEventName = KeyboardEventNames.SystemKeyDown;
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.WmSyskeyup:
                        e.KeyboardEventName = KeyboardEventNames.SystemKeyUp;
                        OnKeyUp(e);
                        break;
                }
            }
            return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            Unhook();
            GC.SuppressFinalize(this);
        }

        ~KeyboardHook()
        {
            Unhook();
        }
    }
}
