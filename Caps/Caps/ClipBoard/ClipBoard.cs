using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace Caps.ClipBoard
{
	public class ClipBoard
	{
		private readonly Stack<IDataObject> _clipBoardQueue = new Stack<IDataObject>();
		private IDataObject _save;


		public void Push()
		{
			IDataObject clipData;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					clipData = Clipboard.GetDataObject();
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
				catch
				{
					// ignored
				}
			}


		}

		public void Pop()
		{

			if (_clipBoardQueue.Count > 0)
			{
				var x = _clipBoardQueue.Pop();
				for (int i = 0; i < 10; i++)
				{
					try
					{
						Clipboard.Clear();
						Clipboard.SetDataObject(x, false);
					}
					catch (Exception)
					{
						// ignored
					}
				}

			}
		}
	}
}
