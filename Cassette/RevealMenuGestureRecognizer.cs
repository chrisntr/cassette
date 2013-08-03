using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

using Xamarin.Juice;

namespace Cassette
{

	class RevealMenuGestureRecognizer : UIPanGestureRecognizer
	{
		public RevealMenuGestureRecognizer (Action<RevealMenuGestureRecognizer> action) :
			base (pan => action (pan as RevealMenuGestureRecognizer))
		{
		}
	}
	
}
