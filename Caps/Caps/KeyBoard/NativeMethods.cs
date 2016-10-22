using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Caps.KeyBoard
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("user32.dll")]
		static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
	}

    internal static class Win32
    {
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

        /// <summary>
        /// nCode numbers identifying low level Keyboard/Mouse events
        /// </summary>
        public static class Hooks
        {
            public const uint WH_KEYBOARD_LL = 13;
            public const uint WH_MOUSE_LL = 14;
        }

		public static class KeyEvents
		{
			public const uint KEYEVENTF_KEYDOWN = 0x0000;
			public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
			public const uint KEYEVENTF_KEYUP = 0x0002;
		}
    }
}
