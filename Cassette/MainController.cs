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
	public class MainController : UIViewController
	{

		PointF PanOrigin;

		UIViewController _ContentController;
		public UIViewController ContentController {
			get { return _ContentController; }
			set {
				if (_ContentController == value)
					return;

				if (_ContentController != null) {
					_ContentController.View.RemoveFromSuperview ();
					_ContentController = null;
				}

				_ContentController = value;
				View.AddSubview (_ContentController.View);
			}
		}

		public MainController ()
		{
			View = new UIView (UIScreen.MainScreen.Bounds);

			var recognizer = new RevealMenuGestureRecognizer (Pan);
			View.AddGestureRecognizer (recognizer);

			View.Add (new MenuView (View.Frame));
		}

		void Pan (RevealMenuGestureRecognizer gesture)
		{
			var viewToSlide = ContentController.View;

			if (gesture.State == UIGestureRecognizerState.Began)
				PanOrigin = viewToSlide.Frame.Location;

			var movement = gesture.TranslationInView (View);
			viewToSlide.Frame = viewToSlide.Frame.With (X: PanOrigin.X + movement.X);

			if (gesture.State == UIGestureRecognizerState.Ended) {
				var destination = PointF.Empty;

				if (viewToSlide.Frame.X > View.Frame.Width / 4) {
					destination = new PointF (View.Frame.Width / 2, 0);
				}

				UIView.Animate (0.3, () => {
					viewToSlide.Frame = viewToSlide.Frame.With (Location: destination);
				});
			}
		}
	}
}
