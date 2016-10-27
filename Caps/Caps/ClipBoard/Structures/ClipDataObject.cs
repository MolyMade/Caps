using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caps.ClipBoard.Structures
{
	public class ClipDataObject
	{
		public string[] Formats;
		public IDictionary<string, object> DataDict { get; }
		public DataObject Object => this.GetDataObject(DataDict);

		public ClipDataObject(IDataObject dataObject)
		{
			Formats = dataObject.GetFormats();
			this.DataDict = new Dictionary<string, object>();
			foreach (string format in Formats)
			{
				try
				{
					DataDict.Add(format, dataObject.GetData(format));
				}
				catch (Exception)
				{
					// ignored
				}
			}
		}

		internal DataObject GetDataObject(IDictionary<string,object> dict)
		{
			DataObject o = new DataObject();
			foreach (var kv in dict)
			{
				o.SetData(kv.Key,false,kv.Value);
			}
			return o;
		}
	}
}
