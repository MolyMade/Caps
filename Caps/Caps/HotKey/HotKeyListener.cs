﻿using System;
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

		private bool KeyboardEventCallback(int vkCode, KeyboardMessages keyboardMessage, uint time)
		{
			if (vkCode == VkCodes.VK_CAPITAL)
			{
				this._modifierKeyState.CapsLock = keyboardMessage == KeyboardMessages.WmKeydown;
				if (keyboardMessage == KeyboardMessages.WmKeyup && _isSingleCaptial)
				{
					KeyboardSend.Key(VkCodes.VK_CAPITAL);
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
					default:
						if (keyboardMessage != KeyboardMessages.WmKeydown && keyboardMessage != KeyboardMessages.WmSyskeydown)
						{
							return true;
						}
						Task.Run(() => OnHotKeyTriggered(_modifierKeyState, vkCode));
						break;
				}
				return false;
			}
			return true;
		}

		public void OnHotKeyTriggered(ModifierKeyState modifierKeyState, int vkCode)
		{
			HotKeyTriggered?.Invoke(this,
				new HotKeyEventArgs(modifierKeyState.Shift, modifierKeyState.Ctrl, modifierKeyState.Alt, vkCode));
		}

		public void Dispose()
		{
			this._keyboardHook.Unhook();
			this._keyboardHook.Dispose();
		}
	}
}