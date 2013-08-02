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
		const int AcceptableAngle = 70;

		bool AngleIsAccpetable (UIGestureRecognizer gesture)
		{
			var pan = gesture as UIPanGestureRecognizer;
			var velocity = pan.VelocityInView (View);
			var angle = Math.Atan2 (velocity.Y, velocity.X) * 180 / Math.PI;
			var accept = Math.Abs (angle) < AcceptableAngle || Math.Abs (angle - 180) < AcceptableAngle;
			return accept;
		}

		public RevealMenuGestureRecognizer (Action<RevealMenuGestureRecognizer> action) :
			base (pan => action (pan as RevealMenuGestureRecognizer))
		{
			ShouldBegin = AngleIsAccpetable;
		}
	}
	
}
