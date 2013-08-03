using System;
using System.Drawing;

namespace Xamarin.Juice
{
	public static class RectangleFExtensions
	{

		static T Id<T> (T t) { return t; }

		public static RectangleF With (this RectangleF self,
		                               PointF? Location = null,
		                               SizeF? Size = null,
		                               float? X = null, float? Y = null,
		                               float? Width = null, float? Height = null)
		{
			if (Location != null) {
				X = Location.Value.X;
				Y = Location.Value.Y;
			}

			if (Size != null) {
				Width = Size.Value.Width;
				Height = Size.Value.Height;
			}

			return new RectangleF (X ?? self.X, Y ?? self.Y, Width ?? self.Width, Height ?? self.Height);
		}
		
		public static RectangleF With (this RectangleF self,
		                               Func<float,float> X = null, Func<float,float> Y = null,
		                               Func<float,float> Width = null, Func<float,float> Height = null)
		{
			return new RectangleF ((X ?? Id) (self.X), (Y ?? Id) (self.Y),
			                       (Width ?? Id) (self.Width), (Height ?? Id) (self.Height));
		}

		public static PointF Center (this RectangleF self)
		{
			return new PointF (self.X + self.Width / 2, self.Y + self.Height / 2);
		}

		public static PointF DistanceTo (this PointF self, PointF other)
		{
			return new PointF (self.X - other.X, self.Y - other.Y);
		}

		public static PointF With (this PointF self,
	                               float? X = null, float? Y = null)
		{
			return new PointF (X ?? self.X, Y ?? self.Y);
		}

		public static PointF With (this PointF self,
	                               Func<float,float> X = null, Func<float,float> Y = null)
		{
			return new PointF ((X ?? Id) (self.X), (Y ?? Id) (self.Y));
		}
	}
}

