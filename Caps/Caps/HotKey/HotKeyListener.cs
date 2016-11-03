using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caps.HotKey.Structures;
using Caps.KeyBoard;
using Caps.KeyBoard.Structures;

namespace Caps.HotKey
{
	public class HotKeyListener:IDisposable
	{
		private readonly KeyHook _keyboardHook;
		public event EventHandler<HotKeyEventArgs> HotKeyTriggered;
		private ModifierKeyState _modifierKeyState;
		private bool _isSingleCaptial;

		public HotKeyListener()
		{
			this._keyboardHook = new KeyHook(KeyboardEventCallback);
			this._keyboardHook.Hook();
		}

		private bool KeyboardEventCallback(int vkCode, KeyboardMessages keyboardMessage, uint time)
		{
			if (vkCode == VkCodes.VK_CAPITAL)
			{
				this._modifierKeyState.CapsLock = keyboardMessage == KeyboardMessages.WmKeydown;
				if (keyboardMessage == KeyboardMessages.WmKeyup && _isSingleCaptial)
				{
					Send.Key(VkCodes.VK_CAPITAL);
				}
				_isSingleCaptial = true;
				return false;
			}
			else if (this._modifierKeyState.CapsLock)
			{
				_isSingleCaptial = false;
				switch (vkCode)
				{
					case VkCodes.VK_LSHIFT:
					case VkCodes.VK_RSHIFT:
						this._modifierKeyState.Shift = keyboardMessage == KeyboardMessages.WmKeydown;
						break;
					case VkCodes.VK_LCONTROL:
					case VkCodes.VK_RCONTROL:
						this._modifierKeyState.Ctrl = keyboardMessage == KeyboardMessages.WmKeydown;
						break;
					case VkCodes.VK_LMENU:
					case VkCodes.VK_RMENU:
						this._modifierKeyState.Alt = keyboardMessage == KeyboardMessages.WmSyskeydown;
						break;
					case VkCodes.VK_LWIN:
					case VkCodes.VK_RWIN:
						this._modifierKeyState.Win = keyboardMessage == KeyboardMessages.WmKeydown;
						break;
					default:
						if (keyboardMessage != KeyboardMessages.WmKeydown && keyboardMessage != KeyboardMessages.WmSyskeydown)
						{
							return true;
						}
						OnHotKeyTriggered(_modifierKeyState, vkCode);
						break;
				}
				return false;
			}
			return true;
		}

		protected void OnHotKeyTriggered(ModifierKeyState modifierKeyState, int vkCode)
		{
			//HotKeyTriggered?.Invoke(this,
			//	new HotKeyEventArgs(modifierKeyState.Shift, modifierKeyState.Ctrl, modifierKeyState.Alt, modifierKeyState.Win,
			//		vkCode));
			HotKeyTriggered?.BeginInvoke(this,
				new HotKeyEventArgs(modifierKeyState.Shift, modifierKeyState.Ctrl, modifierKeyState.Alt, modifierKeyState.Win,
					vkCode), null, null);
		}

		public void Dispose()
		{
			this._keyboardHook.Unhook();
			this._keyboardHook.Dispose();
		}
	}
}
