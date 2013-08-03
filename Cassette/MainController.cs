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
	public class MainController : UIViewController
	{
		PointF PanOrigin;

		event Action MenuClosed, MenuOpened;

		readonly UIView CloseMenuTapDetectView;
		readonly AddShadowView ContentView;

		UIViewController _ContentController;
		public UIViewController ContentController {
			get { return _ContentController; }
			set {
				if (_ContentController == value)
					return;

				_ContentController = value;
				ContentView.View = _ContentController.View;
			}
		}

		public MainController ()
		{
			View = new UIView (UIScreen.MainScreen.Bounds);
			View.AddGestureRecognizer (new RevealMenuGestureRecognizer (Pan));

			ContentView = new AddShadowView { DrawShadow = false };

			CloseMenuTapDetectView = new UIView {
				Alpha = 0.011f,
				BackgroundColor = UIColor.Black,
				Frame = new RectangleF (View.Frame.Width / 2, 0, View.Frame.Width / 2, View.Frame.Height)
			};

			CloseMenuTapDetectView.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				View.SendSubviewToBack (CloseMenuTapDetectView);

				UIView.Animate (0.2, 0, UIViewAnimationOptions.CurveEaseOut, () => {
					ContentView.Frame = View.Frame;
				}, () => {
					MenuClosed.Raise ();
				});
			}));

			MenuOpened += () => {
				View.BringSubviewToFront (CloseMenuTapDetectView);
			};

			MenuClosed += () => {
				ContentView.DrawShadow = false;
			};

			View.AddSubviews (
				CloseMenuTapDetectView, 
				new MenuView (View.Frame),
				ContentView
			);
		}

		void Pan (RevealMenuGestureRecognizer gesture)
		{
			var viewToSlide = ContentView;
			var menuShouldOpen = viewToSlide.Frame.X > View.Frame.Width / 4;

			if (gesture.State == UIGestureRecognizerState.Began) {
				ContentView.DrawShadow = true;
				PanOrigin = viewToSlide.Frame.Location;
				View.SendSubviewToBack (CloseMenuTapDetectView);
			}

			var movement = gesture.TranslationInView (View);
			viewToSlide.Frame = viewToSlide.Frame.With (X: PanOrigin.X + movement.X);

			if (gesture.State == UIGestureRecognizerState.Ended) {
				var destination = PointF.Empty;

				if (menuShouldOpen)
					destination = new PointF (View.Frame.Width / 2, 0);

				UIView.Animate (0.3, () => {
					viewToSlide.Frame = viewToSlide.Frame.With (Location: destination);
				}, () => {
					(menuShouldOpen ? MenuOpened : MenuClosed).Raise ();
				});
			}
		}
	}
}
