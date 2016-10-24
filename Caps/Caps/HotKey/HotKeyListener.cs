using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps.HotKey
{
	public class HotKeyListener
	{
		private KeyboardHook _keyboardHook;
		public event EventHandler<HotKeyEventArgs> HotKeyTriggered;

		public HotKeyListener()
		{
			this._keyboardHook = new KeyboardHook(KeyboardEventCallback);
		}

		private  bool KeyboardEventCallback(int vkCode, KeyboardMessages keyboardMessages, uint time)
		{
			throw new NotImplementedException();
		}

		public void OnHotKeyTriggered()
		{
			HotKeyTriggered?.Invoke(this,new HotKeyEventArgs(HotKeyGroup.a));
		}
	}
}
