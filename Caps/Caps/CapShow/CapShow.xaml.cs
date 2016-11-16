using System;
using System.Windows;
using System.Windows.Input;

namespace Caps.CapShow
{
	/// <summary>
	/// CapShow.xaml 的交互逻辑
	/// </summary>
	public partial class CapShow : Window
	{
		public CapShow()
		{
			InitializeComponent();
			//this.TextBox.Focus();
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
