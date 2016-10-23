using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caps.KeyBoard.Structures
{
	public struct KeyState
	{
		public bool CapsLock;
		public bool Shift;
		public bool Ctrl;
		public bool Alt;
		public byte Key;
	}
}
