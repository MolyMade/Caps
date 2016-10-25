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
		private ConcurrentQueue<IDataObject> _clipBoardQueue = new ConcurrentQueue<IDataObject>();
		private IDataObject _save;


		public void EnQueue()
		{
			_clipBoardQueue.Enqueue(Clipboard.GetDataObject());
		}

		public void DeQueue()
		{
			IDataObject result;
			if (_clipBoardQueue.TryDequeue(out result))
			{
				
				
				Clipboard.SetDataObject(result);
			}
		}

		public string GetText()
		{
			return Clipboard.GetText();
		}

		public void SetText(string s)
		{
			Clipboard.SetText(s);
		}

		public void BackupClipBoard()
		{
			_save = Clipboard.GetDataObject();
		}

		public void RecoverClipBoard()
		{
			if (_save != null)
			{
				Clipboard.SetDataObject(_save);
			}
		}

	
	}
}
