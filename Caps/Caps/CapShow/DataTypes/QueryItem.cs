using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Caps.CapShow.DataTypes
{
	public class QueryItem
	{
		public QueryItem(string queryText, string descriptionText = "", BitmapImage queryIcon = null, EventHandler<KeyEventArgs> invokeAction = null)
		{
			this.QueryText = queryText;
			this.DescriptionText = descriptionText;
			this.QueryIcon = queryIcon;
			this.InvokeAction = invokeAction;
		}

		public string QueryText { get; internal set; }
		public string DescriptionText { get; internal set; }
		public BitmapImage QueryIcon { get; internal set; }
		public EventHandler<KeyEventArgs> InvokeAction { get; internal set; }
	}
}
