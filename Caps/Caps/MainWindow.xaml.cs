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
		private HotKeyListener hl;
		private String X;
		public IntPtr Hwnd;
		public IDataObject id;
		public MainWindow()
		{
			InitializeComponent();
			hl = new HotKeyListener();
			hl.HotKeyTriggered += HlOnHotKeyTriggered;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			textBox.Text = ClipBoard.Clipboard.GetText();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ClipBoard.Clipboard.SetText(textBox1.Text);
		}
		private void HlOnHotKeyTriggered(object sender, HotKeyEventArgs e)
		{
			label.Content = e.HardCode.ToString("X8");
			var c = Ctrl | Alt | VK_1;
			if (e.Key == VK_C)
			{

				Task.Run(() =>
				{
					KeyboardSend.KeyCombination(VK_LCONTROL, VK_C);
					Thread.Sleep(50);
					ClipBoard.Clipboard.Push();
				});

			}
			else if (e.Key == VK_V)
			{
				Task.Run(() =>
				{
					ClipBoard.Clipboard.Pop();
					KeyboardSend.KeyCombination(VK_LCONTROL, VK_V);
				});

			}
			else if (e.HardCode ==(Ctrl|Alt|VK_1))
			{
				
			}
			{
				
			}
		}
	}
}
