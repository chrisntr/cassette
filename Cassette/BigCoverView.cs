using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{

	class BigCoverView : UIImageView
	{
		public event Action<BigCoverView> Tapped = delegate {};

		public BigCoverView ()
		{
			UserInteractionEnabled = true;
			AddGestureRecognizer (
				new UITapGestureRecognizer (() => Tapped (this))
			);
		}
	}
	
}
