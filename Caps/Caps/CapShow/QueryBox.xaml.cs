using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Caps.CapShow.DataTypes;

namespace Caps.CapShow
{
	/// <summary>
	/// QueryBox.xaml 的交互逻辑
	/// </summary>
	public partial class QueryBox : UserControl
	{
		public QueryBox()
		{
			InitializeComponent();
			this.ItemList.ItemsSource = QueryItems;
		}

		public static readonly DependencyProperty QueryItemsProperty = DependencyProperty.Register(
			"QueryItems", typeof(IEnumerable<QueryItem>), typeof(QueryBox), new PropertyMetadata(default(IEnumerable)));



		public IEnumerable<QueryItem> QueryItems
		{
			get { return (IEnumerable<QueryItem>) GetValue(QueryItemsProperty); }
			set { SetValue(QueryItemsProperty, value); }
		}
	}
}
