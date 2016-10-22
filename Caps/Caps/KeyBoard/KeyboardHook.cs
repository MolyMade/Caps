using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Caps.KeyBoard.Structures;

namespace Caps.KeyBoard
{
    public sealed class KeyboardHook : IDisposable
    {
        public event EventHandler<KeyboardHookEventArgs> KeyDown;
		public event EventHandler<KeyboardHookEventArgs> KeyUp;
		private IntPtr _hookId;
		private readonly LowLevelProc _callback;
		private bool _hooked;
	    private bool _skip;

		private void OnKeyDown(KeyboardHookEventArgs e) => KeyDown?.Invoke(this, e);
        private void OnKeyUp(KeyboardHookEventArgs e)=> KeyUp?.Invoke(this, e);

        public KeyboardHook()
        {
            _callback = new LowLevelProc(this.KeyboardHookCallback);
        }

        public void Hook()
        {
			if(_hooked) return;
            _hookId = SetWindowsHook(HookType.WH_KEYBOARD_LL, _callback);
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
                var e = new KeyboardHookEventArgs(lParamStruct,(KeyboardMessages)wParam);
					switch (e.KeyboardMessages)
                {
                    case KeyboardMessages.WmKeydown:
		                _skip = true;          
		                if (e.VirtualKeyCode == VkCodes.VK_CAPITAL && (e.Flag >> 4 & 1) == 0)
		                {
			                _skip = false;
			                return (IntPtr) 1;
		                }
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.WmKeyup:						
		                if (e.VirtualKeyCode == VkCodes.VK_CAPITAL && !_skip)
		                {
			                KeyboardSend.KeyDown(VkCodes.VK_CAPITAL);
		                }
                        OnKeyUp(e);
                        break;
                    case KeyboardMessages.WmSyskeydown:
                        OnKeyDown(e);
                        break;
                    case KeyboardMessages.WmSyskeyup:
                        OnKeyUp(e);
                        break;
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
