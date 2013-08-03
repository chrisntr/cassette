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

		event Action MenuOpened = delegate {};

		PointF PanOrigin;
		UIView CloseMenuTapDetectView;

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

			MenuOpened += AddCloseMenuTapDetectView;

			View.Add (new MenuView (View.Frame));
		}

		void AddCloseMenuTapDetectView ()
		{
			CloseMenuTapDetectView = new UIView {
				Alpha = 0.011f,
				BackgroundColor = UIColor.Black,
				Frame = new RectangleF (View.Frame.Width / 2, 0, View.Frame.Width / 2, View.Frame.Height)
			};

			CloseMenuTapDetectView.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				CloseMenuTapDetectView.RemoveFromSuperview ();

				UIView.Animate (0.2, 0, UIViewAnimationOptions.CurveEaseOut, () => {
					ContentController.View.Frame = View.Frame;
				}, () => {
				});
				
				CloseMenuTapDetectView = null;
			}));

			View.AddSubview (CloseMenuTapDetectView);
		}

		void Pan (RevealMenuGestureRecognizer gesture)
		{
			var viewToSlide = ContentController.View;

			if (gesture.State == UIGestureRecognizerState.Began) {
				PanOrigin = viewToSlide.Frame.Location;

				// We may have started to pan the menu while it was open.
				// In that case we need to remove the CloseTapDetectView.
				if (CloseMenuTapDetectView != null) {
					CloseMenuTapDetectView.RemoveFromSuperview ();
					CloseMenuTapDetectView = null;
				}
			}

			var movement = gesture.TranslationInView (View);
			viewToSlide.Frame = viewToSlide.Frame.With (X: PanOrigin.X + movement.X);

			if (gesture.State == UIGestureRecognizerState.Ended) {
				var destination = PointF.Empty;
				var menuShouldOpen = viewToSlide.Frame.X > View.Frame.Width / 4;

				if (menuShouldOpen)
					destination = new PointF (View.Frame.Width / 2, 0);

				UIView.Animate (0.3, () => {
					viewToSlide.Frame = viewToSlide.Frame.With (Location: destination);
				}, () => {
					if (menuShouldOpen)
						MenuOpened ();
				});
			}
		}
	}
}
