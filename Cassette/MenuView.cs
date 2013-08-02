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
	class MenuView : UIView
	{
		public MenuView (RectangleF frame) : base (frame)
		{
			AddSubview (new UIImageView (UIImage.FromFile ("purple.png")));

			var menu = UIImage.FromFile ("menu.png");
			AddSubview (new UIImageView (menu) {
				Frame = new RectangleF (new PointF (12, 12), menu.Size)
			});
		}
	}
	
}
