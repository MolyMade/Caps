using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caps.ClipBoard.Structures;


namespace Caps.ClipBoard
{
	public class ClipBoard
	{
		internal ConcurrentStack<ClipBoardDataObject> DataStack;
		internal IntPtr Ptr;

		public  ClipBoard(IntPtr intPtr)
		{
			Ptr = intPtr;
			DataStack = new ConcurrentStack<ClipBoardDataObject>();
		}

		public  ClipBoardDataObject GetData()
		{
			ClipBoardDataObject dataObject = new ClipBoardDataObject();
			WinApi.OpenClipBoard(Ptr);
			var x = WinApi.GetFormats();
			foreach (uint format in x)
			{
				IntPtr p = WinApi.GetClipBoardData(format);
				using (Memory m = new Memory(p))
				{
					int length = (int)m.Size;
					IntPtr memPtr = m.Lock();
					byte[] buffer = new byte[length];
					Marshal.Copy(memPtr, buffer, 0, length);
					dataObject.Data[format] = buffer;
				}
			}
			WinApi.CloseClipBoard();
			return dataObject;
		}

		public  void SetData(ClipBoardDataObject dataObject)
		{
			WinApi.OpenClipBoard(Ptr);
			WinApi.EmptyClipBoard();
			foreach (var kv in dataObject.Data)
			{
				int size = kv.Value.Length;
				using (Memory m = new Memory())
				{
					IntPtr handle = m.Alloc(size + 1);
					IntPtr i = m.Lock();
					Marshal.Copy(kv.Value, 0, i, size);
					m.UnLock();
					NativeMethods.SetClipboardData(kv.Key, handle);
				}
			}
			WinApi.CloseClipBoard();
		}

		public  string GetString()
		{
			return "";
		}

		public  void SetString(string s)
		{
			
		}

		public  void Clear()
		{
			
		}

		public  void Push()
		{
			var x = this.GetData();
			DataStack.Push(x);
		}

		public  void Pop()
		{
			ClipBoardDataObject cbo;
			if (DataStack.TryPop(out cbo))
			{
				this.SetData(cbo);
			}
		}
	}
}
