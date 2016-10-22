﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Caps.KeyBoard.Structures;

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
    }
}
