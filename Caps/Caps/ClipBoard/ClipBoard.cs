using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Caps.ClipBoard
{
	public class ClipBoard
	{
		private readonly ConcurrentStack<IDataObject> _clipBoardQueue = new ConcurrentStack<IDataObject>();
		private IDataObject _save;


		public void Push()
		{
			IDataObject clipData = Clipboard.GetDataObject();
			DataObject clipCopy = new DataObject();
			foreach (string format in clipData.GetFormats())
			{
				try
				{
					clipCopy.SetData(format, clipData.GetData(format));
				}
				catch
				{
					// ignored
				}
			}
			_clipBoardQueue.Push(clipCopy);
		}

		public void Pop()
		{
			IDataObject result;
			if (_clipBoardQueue.TryPop(out result))
			{
				Clipboard.Clear();
				Clipboard.SetDataObject(result, true);
			}
		}
	}
}
