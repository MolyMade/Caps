using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caps.Core
{
	public static class Utils
	{
		public static void Delay(int time = 100)
		{
			Thread.Sleep(time);
		}
	}
}
