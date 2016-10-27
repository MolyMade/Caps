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
using System.Windows.Forms;
using Caps.ClipBoard.Structures;


namespace Caps.ClipBoard
{
	public class ClipBoard:IDisposable
	{
		internal BlockingCollection<Command> CommandeQueue = new BlockingCollection<Command>(1);
		internal BlockingCollection<string> StringBuffer = new BlockingCollection<string>(1);
		internal ConcurrentStack<ClipDataObject> DataObjectStack = new ConcurrentStack<ClipDataObject>();
		internal Thread ClipBoardWorker;
		internal bool Running;

		public string String
		{
			get { return this.GetString(); }
			set { this.SetString(value);}
		}

		public ClipBoard()
		{
			Running = true;
			ClipBoardWorker = new Thread(ThreadAction);
			ClipBoardWorker.SetApartmentState(ApartmentState.STA);
			ClipBoardWorker.IsBackground = true;
			ClipBoardWorker.Start();
		}

		internal void ThreadAction()
		{
			while (Running)
			{
				var c = CommandeQueue.Take();
				switch (c)
				{
					case Command.Push:
						DataObjectStack.Push(new ClipDataObject(Clipboard.GetDataObject()));
						break;
					case Command.Pop:
						ClipDataObject cdo;
						if (DataObjectStack.TryPop(out cdo))
						{
							Clipboard.SetDataObject(cdo.Object,true,10,20);
						}
						break;
					case Command.GetString:
						StringBuffer.Add(Clipboard.GetText());
						break;
					case Command.SetString:
						Clipboard.SetText(StringBuffer.Take());
						break;
					case Command.Exit:
						this.Running = false;
						return;
					default:
						throw new NotImplementedException($"{c.ToString()} not implemented.");
				}
			}
		}

		public void Push()
		{
			CommandeQueue.Add(Command.Push);
		}

		public void Pop()
		{
			CommandeQueue.Add(Command.Pop);
		}

		public string GetString()
		{
			CommandeQueue.Add(Command.GetString);
			return StringBuffer.Take();
		}

		public void SetString(string s)
		{
			StringBuffer.Add(s);
			CommandeQueue.Add(Command.SetString);
		}

		public void Dispose()
		{
			this.Running = false;
		}
	}
}
