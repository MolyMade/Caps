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
		
		public SynchronizationContext Context = SynchronizationContext.Current;
		private Core.Core _core;
		private readonly CapShow.CapShow _cap = new CapShow.CapShow();
		public MainWindow()
		{
			InitializeComponent();
			this.Hide();
			_core = new Core.Core(this);
			_core.Run();
		}

		public void ShowCap(object o)
		{
			_cap.Show();
		}

	}
}
