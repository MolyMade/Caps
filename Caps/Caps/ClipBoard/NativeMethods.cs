using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Caps.ClipBoard
{
	public static class NativeMethods
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll")]
		public static extern bool EmptyClipboard();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseClipboard();

		[DllImport("user32.dll")]
		public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

		[DllImport("user32.dll")]
		public static extern IntPtr GetClipboardData(uint uFormat);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint EnumClipboardFormats(uint format);

		[DllImport("user32.dll")]
		public static extern int GetClipboardFormatName(uint format, [Out] StringBuilder
			lpszFormatName, int cchMaxCount);

		[DllImport("user32.dll")]
		public static extern int CountClipboardFormats();

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GlobalLock(IntPtr hMem);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("kernel32.dll")]
		public static extern UIntPtr GlobalSize(IntPtr hMem);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GlobalFree(IntPtr hMem);


	}
}
