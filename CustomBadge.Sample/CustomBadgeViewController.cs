using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace CustomBadge.Sample
{
	public class CustomBadgeViewController : UIViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.LightGray;
			
			CustomBadgeView c1 = new CustomBadgeView ("2")
			{
				TextColor = UIColor.White,
				BadgeColor = UIColor.Red,
				Border = true,
				BorderColor = UIColor.White,
				ScaleFactor = 1f,
				Shining = true
			};
			
			CustomBadgeView c2 = new CustomBadgeView ("CustomBadge")
			{
				TextColor = UIColor.Black,
				BadgeColor = UIColor.Green,
				BorderColor = UIColor.Yellow,
				ScaleFactor = 1.5f
			};
			
			CustomBadgeView c3 = new CustomBadgeView ("Now Retina Ready!")
			{
				BadgeColor = UIColor.Blue,
				ScaleFactor = 1.5f
			};
			
			CustomBadgeView c4 = new CustomBadgeView ("... and scalable")
			{
				BadgeColor = UIColor.Purple,
				BorderColor = UIColor.Black,
				ScaleFactor = 2.0f
			};
			
			CustomBadgeView c5 = new CustomBadgeView ("... with Shining")
			{
				TextColor = UIColor.Black,
				BadgeColor = UIColor.Orange,
				BorderColor = UIColor.Black
			};
			
			CustomBadgeView c6 = new CustomBadgeView ("... without Shining")
			{
				BadgeColor = UIColor.Brown,
				BorderColor = UIColor.Black,
				Shining = false
			};
			
			CustomBadgeView c7 = new CustomBadgeView ("Still Open & Free")
			{
				BadgeColor = UIColor.Black,
				BorderColor = UIColor.Yellow,
				ScaleFactor = 1.25f
			};
			
			CustomBadgeView c8 = new CustomBadgeView ("And small too!")
			{
				ScaleFactor = 0.7f
			};
			
			CustomBadgeView c9 = new CustomBadgeView ("9")
			{
				ScaleFactor = 0.5f,
				BadgeColor = UIColor.Orange
			};
			
			// Positions
			c1.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c1.Frame.Width / 2 + c2.Frame.Width / 2 - 10, 105), c1.Frame.Size);
			c2.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c2.Frame.Width / 2, 110), c2.Frame.Size);
			c3.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c3.Frame.Width / 2, 150), c3.Frame.Size);
			c4.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c4.Frame.Width / 2, 185), c4.Frame.Size);
			c5.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c5.Frame.Width / 2, 235), c5.Frame.Size);
			c6.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c6.Frame.Width / 2, 260), c6.Frame.Size);
			c7.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c7.Frame.Width / 2, 310), c7.Frame.Size);
			c8.Frame = new RectangleF (new PointF (this.View.Frame.Width / 2 - c8.Frame.Width / 2, 350), c8.Frame.Size);
			c9.Frame = new RectangleF (new PointF (c8.Frame.Right - 15, c8.Frame.Top - 5), c9.Frame.Size);
			
			
			// Adding to view
			this.View.AddSubview (c2);
			this.View.AddSubview (c1);
			this.View.AddSubview (c3);
			this.View.AddSubview (c4);
			this.View.AddSubview (c5);
			this.View.AddSubview (c6);
			this.View.AddSubview (c7);
			this.View.AddSubview (c8);
			this.View.AddSubview (c9);
		}
	}
}

