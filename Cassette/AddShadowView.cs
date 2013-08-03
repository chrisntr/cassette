using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

using Xamarin.Juice;

namespace Cassette
{
	class AddShadowView : UIView
	{
		UIView _view;

		public UIView View {
			get { return _view; }
			set {
				if (_view == value)
					return;

				if (_view != null) {
					// Remove previous view
					_view.RemoveFromSuperview ();
					_view = null;
				}

				_view = value;
				_view.Frame = _view.Frame.With (Location: PointF.Empty);
				Frame = Frame.With (Size: _view.Frame.Size);
				AddSubview (_view);
			}
		}

		public AddShadowView ()
		{
			ClipsToBounds = false;
			Layer.ShouldRasterize = true;
			Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			Layer.ShadowRadius = 10;
			Layer.ShadowColor = new CGColor (0, 0, 0);
			Layer.ShadowOpacity = 0.5f;
		}

		public AddShadowView (UIView view) : this ()
		{
			View = view;
		}
	}
	
}
