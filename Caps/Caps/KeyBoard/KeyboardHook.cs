using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Caps.KeyBoard.Structures;

namespace Caps.KeyBoard
{
    public sealed class KeyboardHook : IDisposable
    {
		private IntPtr _hookId;
		private readonly LowLevelProc _lowLevelcallback;
	    private readonly KeyboardEventCallback _keyboardEventCallback;
		private bool _hooked;

        public KeyboardHook(KeyboardEventCallback keyboardEventCallback)
        {
	        _keyboardEventCallback = keyboardEventCallback;
	        _lowLevelcallback = KeyboardHookCallback;
        }

        public void Hook()
        {
			if(_hooked) return;
            _hookId = SetWindowsHook(HookType.WH_KEYBOARD_LL, _lowLevelcallback);
            _hooked = true;
        }

        public void Unhook()
        {
            if (!_hooked) return;
            NativeMethods.UnhookWindowsHookEx(_hookId);
            _hooked = false;
        }

        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var lParamStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
	            if ((lParamStruct.Flags >> 4 & 1) == 0 &&
					!_keyboardEventCallback(lParamStruct.VkCode,(KeyboardMessages)wParam, lParamStruct.Time))
	            {
		            return (IntPtr) 1;
	            }
            }
            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

		public static IntPtr SetWindowsHook(int hookType, LowLevelProc callback)
		{
			IntPtr hookId;
			using (var currentProcess = Process.GetCurrentProcess())
			using (var currentModule = currentProcess.MainModule)
			{
				var handle = NativeMethods.GetModuleHandle(currentModule.ModuleName);
				hookId = NativeMethods.SetWindowsHookEx(hookType, callback, handle, 0);
			}
			return hookId;
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
