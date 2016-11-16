using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caps.CapShow
{
	/// <summary>
	/// QueryBox.xaml 的交互逻辑
	/// </summary>
	public partial class QueryBoxItem : UserControl
	{
		public QueryBoxItem()
		{
			InitializeComponent();
		}

		[Description("Get or set query text.")]
		[Category("Common Properties")]
		public string QueryText
		{
			get { return QueryTextBlock.Text; }
			set { QueryTextBlock.Text = value; }
		}

		[Description("Get or set description text.")]
		[Category("Common Properties")]
		public string DescriptionText
		{
			get { return DescriptionTextBlock.Text; }
			set { DescriptionTextBlock.Text = value; }
		}

		[Description("Get or set query icon.")]
		[Category("Common Properties")]
		public BitmapImage QueryIcon
		{
			get { return QueryIconBlock.Source as BitmapImage;}
			set { QueryIconBlock.Source = value; }
		}
	}
}
