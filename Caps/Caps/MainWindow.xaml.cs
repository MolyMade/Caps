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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;


namespace Caps
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		private Testing t = new Testing(IntPtr.Zero);
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			textBox.Text = ClipBoard.Clipboard.GetText();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ClipBoard.Clipboard.SetText(textBox1.Text);
		}
	}
}
