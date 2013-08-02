using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{
	class CoverView : UIImageView
	{
		public event Action<CoverView> Tapped;

		public CoverView (UIImage image) : base (image)
		{
			var tap = new UITapGestureRecognizer (() => Tapped.Raise (this));
			UserInteractionEnabled = true;
			AddGestureRecognizer (tap);
		}
	}

	public class CoverListView : UIScrollView
	{
		const int CoverOverlap = 56;
		const int TotalCovers = 12;

		List<CoverView> Covers = new List<CoverView> ();
		bool CoversCollapsed;

		public CoverListView (RectangleF frame) : base (frame)
		{
			Enumerable
				.Range (0, TotalCovers)
				.Select (i => string.Format ("covers/{0}.png", i))
				.Select (CreateCoverView)
				.ForEach (AddCover);
		}

		CoverView CreateCoverView (string path)
		{
			var image = UIImage.FromFile (path);
			var cover = new CoverView (image);

			cover.Tapped += CoverTapped;

			return cover;
		}

		void CoverTapped (CoverView cover)
		{
			UIView.Animate (0.3, 0, UIViewAnimationOptions.CurveEaseInOut, () => {
				foreach (var otherCover in Covers.WithIndexes ()) {
					if (cover == otherCover.Value) continue;

					int signEven = otherCover.Index % 2 == 0 ? -1 : 1;
					int signCollapsed = CoversCollapsed ? -1 : 1;

					otherCover.Value.Frame = otherCover.Value.Frame.With (
						X: x => x + (signCollapsed * signEven * Frame.Width / 2)
					);
				}
			}, () => {
				CoversCollapsed = !CoversCollapsed;
			});
		}

		void AddCover (CoverView cover)
		{
			Covers.Add (cover);

			var index = Covers.Count - 1;
			var rows = (int) Math.Ceiling (Covers.Count / 2.0);
			var coverSize = Frame.Width / 2;

			cover.Frame = new RectangleF (
				(index % 2) * coverSize,
			    (index / 2) * (coverSize - CoverOverlap),
				coverSize,
				coverSize
			);

			ContentSize = new SizeF (Frame.Width, rows * (coverSize - CoverOverlap) + CoverOverlap);
			AddSubview (cover);
		}
	}
	
}
