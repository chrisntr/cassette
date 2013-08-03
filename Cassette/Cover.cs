using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{

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
	
}
