using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Juice;

namespace Cassette
{

	public class CoverController : UIViewController
	{
		public CoverController ()
		{
			View = new CoverListView (UIScreen.MainScreen.Bounds);
		}
	}
	
}
