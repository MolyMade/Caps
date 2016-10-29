using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caps.ClipBoard
{
	public class Clipboard
	{
		internal ConcurrentStack<IDataObject> ObjectStack = new ConcurrentStack<IDataObject>();
		public int Count => ObjectStack.Count;

		internal IDataObject Get()
		{
			var o = HandleOleApi.GetDataObject();
			return o;
		}

		internal void Set(IDataObject obj)
		{
			HandleOleApi.SetDataObject(obj);
		}

		internal IDataObject Retrieve()
		{
			var clipdata = this.Get();
			DataObject data = new DataObject();
			var formats = clipdata.GetFormats();
			foreach (string format in formats)
			{
				object d;
				try
				{
					d = clipdata.GetData(format,false);
				}
				catch (OutOfMemoryException)
				{
					d = null;
				}
				catch (ExternalException)
				{
					d = null;
				}
				if (d != null)
				{
					data.SetData(format, d);
				}
			}
			return data;
		}

		public bool Push()
		{
			try
			{
				ObjectStack.Push(this.Retrieve());
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Pop()
		{
			IDataObject id;
			if (ObjectStack.TryPop(out id))
			{
				this.Set(id);
				return true;
			}
			return false;
		}

		public void ClearStack()
		{
			this.ObjectStack.Clear();
		}

		public void Clear()
		{
			HandleOleApi.Clear();
		}

	}
}
