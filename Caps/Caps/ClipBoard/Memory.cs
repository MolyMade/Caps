using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Caps.ClipBoard
{
	public class Memory:IDisposable
	{
		internal IntPtr Ptr;
		private const int ERROR_NOT_LOCKED = 158;
		public uint Size => NativeMethods.GlobalSize(Ptr).ToUInt32();

		public Memory(IntPtr intPtr)
		{
			Ptr = intPtr;
		}

		public Memory()
		{
			
		}

		public IntPtr Lock()
		{
			IntPtr memPtr = NativeMethods.GlobalLock(this.Ptr);
			return memPtr;	
		}

		public IntPtr Alloc(int bytes)
		{
			this.Ptr = NativeMethods.GlobalAlloc(2, new UIntPtr((uint) bytes));
			return Ptr;
		}

		public void UnLock()
		{
			if (NativeMethods.GlobalUnlock(Ptr))
			{
				var err = Marshal.GetLastWin32Error();
				if (err == 0 || err == ERROR_NOT_LOCKED)
				{
					return;
				}
					throw new Exception("Fail to Unlock");
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
