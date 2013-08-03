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

		const float DefaultShadowOpacity = 0.5f;
		const int DefaultShadowRadius = 5;

		float _shadowOpacity;
		public float ShadowOpacity {
			get { return _shadowOpacity; }
			set {
				Layer.ShadowOpacity = _shadowOpacity = value;
			}
		}

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

		public bool DrawShadow {
			get {
				return Layer.ShadowOpacity != 0;
			}
			set {
				Layer.ShadowOpacity = value ? ShadowOpacity : 0;
			}
		}

		public AddShadowView ()
		{
			ClipsToBounds = false;
			Layer.ShouldRasterize = true;
			Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			Layer.ShadowRadius = DefaultShadowRadius;
			Layer.ShadowColor = new CGColor (0, 0, 0);

			ShadowOpacity = DefaultShadowOpacity;
		}

		public AddShadowView (UIView view) : this ()
		{
			View = view;
		}
	}
	
}
