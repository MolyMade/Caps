using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.ClipBoard.Structures;

namespace Caps.ClipBoard
{
	public static class WinApi
	{
		public static void OpenClipBoard(IntPtr intPtr)
		{
			if (NativeMethods.OpenClipboard(intPtr))
			{
				return;
			}
			throw new Exception("Fail to Open ClipBoard");

		}

		public static uint[] GetFormats()
		{
			List<uint> formats = new List<uint>();
			uint i = 0;
			while (true)
			{
				i = NativeMethods.EnumClipboardFormats(i);
				if (i == 0) break;
				formats.Add(i);
			}
			return formats.ToArray();
		}

		public static void EmptyClipBoard()
		{
			if (NativeMethods.EmptyClipboard())
			{
				return;
			}
			throw new Exception("Fail to Empty ClipBoard");
		}

		public static IntPtr GetClipBoardData(uint format)
		{
			var r = NativeMethods.GetClipboardData(format);
			if (r == IntPtr.Zero)
			{
				throw new Exception("Fail to GetData");
			}
			return r;
		}

		public static IntPtr SetClipBoardData(uint format, IntPtr hMem)
		{
			var r = NativeMethods.SetClipboardData(format, hMem);
			if (r == IntPtr.Zero)
			{
				throw new Exception("Fail to SetData");
			}
			return r;
		}

		public static void CloseClipBoard()
		{
			if (NativeMethods.CloseClipboard())
			{
				return;
			}
			throw new Exception("Fail to close ClipBoard");
		}
	}
}
