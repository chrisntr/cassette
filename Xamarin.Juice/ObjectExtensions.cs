using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

namespace Xamarin.Juice
{
	public static class ObjectExtensions
	{
		public static IEnumerable<T> Cons<T> (this T self, IEnumerable<T> tail)
		{
			return new [] { self }.Concat (tail);
		}
	}
}

