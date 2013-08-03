using System;
using System.Linq;
using System.Collections.Generic;

namespace Xamarin.Juice
{
	public static class EventExtensions
	{
		public static T Delegate<T> (this Func<T> self, T def = default(T))
		{
			// Take raised values in reverse order so that last-added handler overrides all.
			return self.Raise ().Reverse ().Concat (new [] { def }).First ();
		}

		public static T Delegate<Q, T> (this Func<Q, T> self, Q arg1, T def = default(T))
		{
			// Take raised values in reverse order so that last-added handler overrides all.
			return self.Raise (arg1).Reverse ().Concat (new [] { def }).First ();
		}

		public static T Delegate<Q, R, T> (this Func<Q, R, T> self, Q arg1, R arg2, T def = default(T))
		{
			// Take raised values in reverse order so that last-added handler overrides all.
			return self.Raise (arg1, arg2).Reverse ().Concat (new [] { def }).First ();
		}

		public static IEnumerable<T> Raise<T> (this Func<T> self)
		{
			if (self != null)
				foreach (Delegate del in self.GetInvocationList ())
					yield return (T) del.DynamicInvoke ();
		}
		
		public static IEnumerable<T> Raise<Q, T> (this Func<Q, T> self, Q arg1)
		{
			if (self != null)
				foreach (Delegate del in self.GetInvocationList ())
					yield return (T) del.DynamicInvoke (arg1);
		}

		public static IEnumerable<T> Raise<Q, R, T> (this Func<Q, R, T> self, Q arg1, R arg2)
		{
			if (self != null)
				foreach (Delegate del in self.GetInvocationList ())
					yield return (T) del.DynamicInvoke (arg1, arg2);
		}

		public static void Raise (this Action self)
		{
			if (self != null)
				self ();
		}

		public static void Raise<Q> (this Action<Q> self, Q arg1)
		{
			if (self != null)
				self (arg1);
		}

		public static void Raise<Q, R> (this Action<Q, R> self, Q arg1, R arg2)
		{
			if (self != null)
				self (arg1, arg2);
		}

		public static void Raise<Q, R, S> (this Action<Q, R, S> self, Q arg1, R arg2, S arg3)
		{
			if (self != null)
				self (arg1, arg2, arg3);
		}
	}
}