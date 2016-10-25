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
	public class HotKeyListener:IDisposable
	{
		private readonly KeyboardHook _keyboardHook;
		public event EventHandler<HotKeyEventArgs> HotKeyTriggered;
		private ModifierKeyState _modifierKeyState;
		private bool _isSingleCaptial;

		public HotKeyListener()
		{
			this._keyboardHook = new KeyboardHook(KeyboardEventCallback);
			this._keyboardHook.Hook();
		}

		private void ModifierKeyStateUpdate(int vkCode, KeyboardMessages keyboardMessage)
		{
			if (vkCode == VkCodes.VK_CAPITAL)
			{
				this._modifierKeyState.CapsLock = keyboardMessage == KeyboardMessages.WmKeydown;
			}
			else if (vkCode == VkCodes.VK_LSHIFT || vkCode == VkCodes.VK_RSHIFT)
			{
				this._modifierKeyState.Shift = keyboardMessage == KeyboardMessages.WmKeydown;
			}
			else if (vkCode == VkCodes.VK_LCONTROL || vkCode == VkCodes.VK_RCONTROL)
			{
				this._modifierKeyState.Ctrl = keyboardMessage == KeyboardMessages.WmKeydown;
			}
			else if (vkCode == VkCodes.VK_LMENU || vkCode == VkCodes.VK_RMENU)
			{
				this._modifierKeyState.Alt = keyboardMessage == KeyboardMessages.WmSyskeydown;
			}
		}

		private  bool KeyboardEventCallback(int vkCode, KeyboardMessages keyboardMessage, uint time)
		{
			this.ModifierKeyStateUpdate(vkCode,keyboardMessage);
			if (vkCode == VkCodes.VK_CAPITAL)
			{
				if (keyboardMessage == KeyboardMessages.WmKeyup && _isSingleCaptial)
				{
					KeyboardSend.Key(VkCodes.VK_CAPITAL);
				}
				_isSingleCaptial = true;
				return false;
			}
			if (this._modifierKeyState.CapsLock)
			{
				_isSingleCaptial = false;
				return false;
			}
			return true;
		}

		public void OnHotKeyTriggered()
		{
			HotKeyTriggered?.Invoke(this,new HotKeyEventArgs(HotKeyGroup.a));
		}

		public void Dispose()
		{
			this._keyboardHook.Unhook();
			this._keyboardHook.Dispose();
		}
	}
}
