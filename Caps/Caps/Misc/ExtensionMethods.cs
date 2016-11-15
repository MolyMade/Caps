using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caps.Misc
{
	static class ExtensionMethods
	{
		public static void ForEach<T>(this T[] array, Action<T> action)
		{
			foreach (T item in array)
			{
				action(item);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> iEnumerable, Action<T> action)
		{
			foreach (T item in iEnumerable)
			{
				action(item);
			}
		}
	}
}
