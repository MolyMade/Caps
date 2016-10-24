using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caps.HotKey.Structures
{
	public class HotKeyEventArgs:EventArgs
	{
		public HotKeyGroup HotKey { get; internal set; }

		public HotKeyEventArgs(HotKeyGroup hotKey)
		{
			this.HotKey = hotKey;
		}
	}
}
