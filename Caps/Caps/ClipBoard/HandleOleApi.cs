using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Caps.ClipBoard
{
	public static class HandleOleApi
	{
		public static IDataObject GetDataObject()
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					return System.Windows.Clipboard.GetDataObject();
				}
				catch (Exception)
				{
					Thread.Sleep(10);
				}
			}
			return null;
		}

		public static object GetData(string format)
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					return System.Windows.Clipboard.GetData(format);
				}
				catch (Exception)
				{
					Thread.Sleep(10);
				}
			}
			return null;

		}

		public static void SetDataObject(IDataObject obj)
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					System.Windows.Clipboard.SetDataObject(obj);
					return;
				}
				catch
				{
					Thread.Sleep(10);
				}
			}
		}

		public static void SetData(string format, IDataObject data)
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					System.Windows.Clipboard.SetData(format, data);
					return;
				}
				catch
				{
					Thread.Sleep(10);
				}
			}
		}

		public static void Clear()
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					System.Windows.Clipboard.Clear();
					return;
				}
				catch
				{
					Thread.Sleep(10);
				}
			}
		}

		public static string GetText()
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					return System.Windows.Clipboard.GetText();
				}
				catch (Exception)
				{
					Thread.Sleep(10);
				}
			}
			return "";
		}

		public static void SetText(string s)
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					System.Windows.Clipboard.SetText(s);
					return;
				}
				catch (Exception)
				{
					Thread.Sleep(10);
				}
			}
		}

		public static void Flush()
		{
			for (int i = 0; i < 20; i++)
			{
				try
				{
					System.Windows.Clipboard.Flush();
					return;

				}
				catch (Exception)
				{
					Thread.Sleep(10);
				}
			}
		}
	}
}
