using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{
	public class ContentController : UIViewController
	{
		readonly CoverCollectionView CoverCollection;

		UIView OriginalView;
		RectangleF OriginalFrame;
		BigCoverView CoverView;

		public ContentController (RectangleF frame)
		{
			View = new UIView (frame);
			View.AddSubview (CoverCollection = new CoverCollectionView (frame));
			CoverCollection.CoverTapped += CoverCollectionCoverTapped;
		}

		void TransitionFromBigCoverBackToCoverCollection ()
		{
			UIView.Animate (0.2, () => {
				CoverView.Frame = OriginalFrame;
				CoverCollection.Alpha = 1;
			}, () => {
				CoverView.RemoveFromSuperview ();
				CoverView = null;
				OriginalView.Alpha = 1;
			});
		}

		void CoverCollectionCoverTapped (Cover cover, UIView view)
		{
			OriginalView = view;
			OriginalFrame = CoverCollection.ConvertRectToView (view.Frame, View);

			CoverView = new BigCoverView { Frame = OriginalFrame, Image = cover.CoverImage };
			CoverView.Tapped += _ => TransitionFromBigCoverBackToCoverCollection ();
		
			View.AddSubview (CoverView);

			OriginalView.Alpha = 0;
			UIView.Animate (0.2, () => {
				CoverCollection.Alpha = 0;
				CoverView.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Width);
			});
		}
	}
	
}
