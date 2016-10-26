using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Caps.ClipBoard
{
	class NativeMethods
	{
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("user32.dll")]
		static extern bool EmptyClipboard();

		[DllImport("kernel32.dll")]
		static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

		[DllImport("user32.dll")]
		static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool CloseClipboard();

		[DllImport("user32.dll")]
		static extern IntPtr GetClipboardData(uint uFormat);

		[DllImport("user32.dll")]
		static extern int CountClipboardFormats();

		[DllImport("user32.dll", SetLastError = true)]
		static extern uint EnumClipboardFormats(uint format);

		[DllImport("user32.dll")]
		static extern int GetClipboardFormatName(uint format, [Out] StringBuilder
			lpszFormatName, int cchMaxCount);

	}
}
