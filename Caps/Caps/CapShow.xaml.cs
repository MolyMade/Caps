using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Caps
{
	/// <summary>
	/// CapShow.xaml 的交互逻辑
	/// </summary>
	public partial class CapShow : Window
	{
		public CapShow()
		{
			InitializeComponent();
		}

		public void Toggle(object o)
		{
			if (this.Visibility == Visibility.Visible)
			{
				this.Hide();
			}
			else
			{		
				this.Show();
				this.textBox.Focus();
			}
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

		private void textBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.Left = e.GetPosition(this).X - this.Left;
			this.Top = e.GetPosition(this).Y - this.Top;
		}
	}
}
