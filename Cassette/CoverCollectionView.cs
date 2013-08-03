using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{
	class CoverCell : UICollectionViewCell
	{
		Cover cover;
		readonly UIImageView ImageView;

		public static readonly SizeF DefaultSize;

		static CoverCell ()
		{
			var size = UIScreen.MainScreen.Bounds.Width / 2;
			DefaultSize = new SizeF (size, size);
		}

		public CoverCell (IntPtr handle) : base (handle)
		{
			Frame = new RectangleF (PointF.Empty, DefaultSize);
			ImageView = new UIImageView (Frame);
			ContentView.Add (ImageView);
		}

		public Cover Cover {
			get { return cover; }
			set {
				cover = value;
				ImageView.Image = cover.CoverImage;
			}
		}
	}

	public class Cover
	{
		public readonly string ImagePath;

		Lazy<UIImage> CoverImageLazy;

		public UIImage CoverImage {
			get {
				return CoverImageLazy.Value;
			}
		}
		public Cover (string imagePath)
		{
			ImagePath = imagePath;
			CoverImageLazy = new Lazy<UIImage> (() => UIImage.FromFile (ImagePath));
		}
	}

	public class CoverCollectionView : UICollectionView
	{
		public static readonly NSString CoverCellId = (NSString) "CoverCellId";

		public event Action<Cover, UIView> CoverTapped = delegate {};

		public CoverCollectionView (RectangleF frame) : base (frame, new UICollectionViewFlowLayout ())
		{
			RegisterClassForCell (typeof(CoverCell), CoverCellId);

			DataSource = new CoverDataSource ();
			var @delegate = new CoverLayoutDelegate ();
			Delegate = @delegate;

			@delegate.CoverCellSelected += (cover, view) => CoverTapped (cover, view);
		}

		class CoverLayoutDelegate : UICollectionViewDelegateFlowLayout
		{
			public event Action<Cover, UIView> CoverCellSelected = delegate {};

			public override SizeF GetSizeForItem (UICollectionView view, UICollectionViewLayout layout, NSIndexPath path)
			{
				return CoverCell.DefaultSize;
			}

			public override float GetMinimumInteritemSpacingForSection (UICollectionView view, UICollectionViewLayout layout, int section)
			{
				return 0;
			}

			public override float GetMinimumLineSpacingForSection (UICollectionView view, UICollectionViewLayout layout, int section)
			{
				return 0;
			}

			public override void ItemSelected (UICollectionView view, NSIndexPath path)
			{
				var cell = (CoverCell) view.CellForItem (path);
				CoverCellSelected (cell.Cover, cell);
			}
		}

		class CoverDataSource : UICollectionViewDataSource
		{
			const int TotalCovers = 12;

			List<Cover> Covers =
				Enumerable
					.Range (0, TotalCovers)
					.Select (i => new Cover (string.Format ("covers/{0}.png", i)))
					.ToList ();

			public override int GetItemsCount (UICollectionView collectionView, int section)
			{
				return Covers.Count;
			}

			public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath path)
			{
				var cell = (CoverCell) collectionView.DequeueReusableCell (CoverCellId, path);
				cell.Cover = Covers [path.Row];
				return cell;
			}
		}
	}
	
}
