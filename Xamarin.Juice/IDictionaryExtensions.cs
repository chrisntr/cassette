using System;
using System.Collections.Generic;

namespace Xamarin.Juice
{
	public static class IDictionaryExtensions
	{
		/// <summary>
		/// Gets the value for the specified, setting the key with a default value if
		/// the dictionary doesn't contain the key.
		/// </summary>
		public static V GetWithDefault<K,V> (this IDictionary<K,V> self, K key, Func<V> getDefault)
		{
			V val;
			if (!self.TryGetValue (key, out val))
				self [key] = val = getDefault ();
			return val;
		}
	}
}

