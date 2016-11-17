using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Caps.CapShow.DataTypes
{
	public class ItemTriggeredEvent : EventArgs
	{
		public Key Key { get; internal set; }
		public ItemTriggeredEvent(Key key)
		{
			this.Key = key;
		}
	}
}
