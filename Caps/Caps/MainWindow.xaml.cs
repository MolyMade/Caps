using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caps.HotKey;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;
using static Caps.KeyBoard.Structures.VkCodes;
using static Caps.KeyBoard.Structures.Modifiers;


namespace Caps
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		Core.Core core = new Core.Core();
		private SynchronizationContext c = SynchronizationContext.Current;
		public MainWindow()
		{
			InitializeComponent();
			core.Run();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
