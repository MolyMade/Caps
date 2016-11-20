using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Caps.CapShow.DataTypes;

namespace Caps.CapShow
{
	/// <summary>
	/// CapShow.xaml 的交互逻辑
	/// </summary>
	public partial class CapShow : Window
	{
		ObservableCollection<QueryItem> a = new ObservableCollection<QueryItem>();
		public CapShow()
		{
			InitializeComponent();
			//this.TextBox.Focus();
			a.Add(new QueryItem("sssssssssss", "bbbbb"));
			ItemList.ItemsSource = a;
		}


		private void Window_Deactivated(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				this.Hide();
			}
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			
		}

		

	}
}
