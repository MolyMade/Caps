using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caps.HotKey.Structures
{
	public class HotKeyEventArgs:EventArgs
	{
		
		public bool Ctrl;
		public bool Alt;
		public bool Shift;
		public bool Win;
		public int Key;

		public int HardCode => (Ctrl ? 0x0800 : 0) | (Shift ? 0x0400 : 0) | (Alt ? 0x0200 : 0) | (Win ? 0x100 : 0) | Key;

		public HotKeyEventArgs(bool shift, bool ctrl, bool alt,bool win, int kvCode)
		{
			this.Shift = shift;
			this.Ctrl = ctrl;
			this.Alt = alt;
			this.Win = win;
			this.Key = kvCode;
		}
	}
}
