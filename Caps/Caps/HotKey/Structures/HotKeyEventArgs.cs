using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caps.HotKey.Structures
{
	public class HotKeyEventArgs:EventArgs
	{
		public bool Shift;
		public bool Ctrl;
		public bool Alt;
		public int Key;

		public HotKeyEventArgs(bool shift, bool ctrl, bool alt, int kvCode)
		{
			this.Shift = shift;
			this.Ctrl = ctrl;
			this.Alt = alt;
			this.Key = kvCode;

		}
	}
}
