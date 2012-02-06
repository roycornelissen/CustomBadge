using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace CustomBadge
{
	public class CustomBadgeView : UIView
	{
		private string text;
		private UIColor textColor;
		private UIColor badgeColor;
		private UIColor borderColor;
		private bool border;
		private bool shining;
		private bool autoResize;
		private float cornerRoundness;
		private float scaleFactor;
		
		public CustomBadgeView (string text)
			: this(text, true) { }
		
		public CustomBadgeView (string text, bool autoResize)
			: base(new RectangleF(0, 0, 25, 25))
		{
			this.ContentScaleFactor = UIScreen.MainScreen.Scale;
			this.BackgroundColor = UIColor.Clear;
			this.text = text;
			this.textColor = UIColor.White;
			this.border = true;
			this.borderColor = UIColor.White;
			this.badgeColor = UIColor.Red;
			this.cornerRoundness = 0.4f;
			this.scaleFactor = 1.0f;
			this.shining = true;
			this.autoResize = autoResize;
			if (this.autoResize)
				this.AutoResizeBadge ();
			base.UserInteractionEnabled = false;
		}
		
		public string Text {
			get {
				return text;
			}
			set {
				if (text != value) {
					text = value;
					Redraw ();
				}
			}
		}
		
		public UIColor TextColor {
			get {
				return textColor;
			}
			set {
				if (textColor != value) {
					textColor = value;
					Redraw (false);
				}
			}
		}
		
		public UIColor BadgeColor {
			get {
				return badgeColor;
			}
			set {
				if (badgeColor != value) {
					badgeColor = value;
					Redraw (false);
				}
			}
		}
		
		public UIColor BorderColor {
			get {
				return borderColor;
			}
			set {
				if (borderColor != value) {
					borderColor = value;
					Redraw (false);
				}
			}
		}
		
		public bool Border {
			get {
				return border;
			}
			set {
				if (border != value) {
					border = value;
					Redraw (false);
				}
			}
		}
		
		public bool Shining {
			get {
				return shining;
			}
			set {
				if (shining != value) {
					shining = value;
					Redraw (false);
				}
			}
		}
		
		public bool AutoResize {
			get {
				return autoResize;
			}
			set {
				autoResize = value;
			}
		}
		
		public float CornerRoundness {
			get {
				return cornerRoundness;
			}
			set {
				if (cornerRoundness != value) {
					cornerRoundness = value;
					Redraw ();
				}
			}
		}
		
		public float ScaleFactor {
			get {
				return scaleFactor;
			}
			set {
				if (scaleFactor != value) {
					scaleFactor = value;
					Redraw ();
				}
			}
		}
		
		private void Redraw (bool autoResize = true)
		{
			if (autoResize && this.autoResize) {
				AutoResizeBadge ();
				return; // AutoResize calls redraw
			}
			this.SetNeedsDisplay (); 
		}
		
		public void AutoResizeBadge ()
		{
			var stringSize = new NSString (this.text).StringSize (UIFont.BoldSystemFontOfSize (12));
			float flexSpace, rectWidth, rectHeight;
			var frame = this.Frame;
			if (this.text.Length >= 2) {
				flexSpace = this.text.Length;
				rectWidth = 25 + (stringSize.Width + flexSpace);
				rectHeight = 25;
				frame.Width = rectWidth * this.scaleFactor;
				frame.Height = rectHeight * this.scaleFactor;
			} else {
				frame.Width = 25 * this.scaleFactor;
				frame.Height = 25 * this.scaleFactor;
			}
			this.Frame = frame;
			this.Redraw (false);
		}
		
		public override void Draw (RectangleF rect)
		{
			var context = UIGraphics.GetCurrentContext ();
			this.DrawRoundedRect (context, rect);
			
			if (this.shining)
				DrawShine (context, rect);
			
			if (this.border)
				DrawBorder (context, rect);
			
			if (this.text.Length > 0) {
				textColor.SetColor ();
				var sizeOfFont = 13.5f * scaleFactor;
				if (this.text.Length < 2) {
					sizeOfFont += sizeOfFont * 0.20f;
				}
				var font = UIFont.BoldSystemFontOfSize (sizeOfFont);
				var text = new NSString (this.text);
				var textSize = text.StringSize (font);
				var textPos = new PointF (rect.Width / 2 - textSize.Width / 2, rect.Height / 2 - textSize.Height / 2);
				if (this.text.Length < 2)
					textPos.X += 0.5f;
				text.DrawString (textPos, font);
			}
		}
		
		private float MakePath (CGContext context, RectangleF rect)
		{
			var radius = rect.Bottom * this.cornerRoundness;
			var puffer = rect.Bottom * 0.12f;
			var maxX = rect.Right - (puffer * 2f);
			var maxY = rect.Bottom - puffer;
			var minX = rect.Left + (puffer * 2f);
			var minY = rect.Top + puffer;
			if (maxX - minX < 20f) {
				maxX = rect.Right - puffer;
				minX = rect.Left + puffer;
			}
			
			context.AddArc (maxX - radius, minY + radius, radius, (float)(Math.PI + (Math.PI / 2)), 0f, false);
			context.AddArc (maxX - radius, maxY - radius, radius, 0, (float)(Math.PI / 2), false);
			context.AddArc (minX + radius, maxY - radius, radius, (float)(Math.PI / 2), (float)Math.PI, false);
			context.AddArc (minX + radius, minY + radius, radius, (float)Math.PI, (float)(Math.PI + Math.PI / 2), false);
			
			return maxY;
		}
		
		private void DrawRoundedRect (CGContext context, RectangleF rect)
		{
			context.SaveState ();
			
			context.BeginPath ();
			context.SetFillColor (badgeColor.CGColor);
			MakePath (context, rect);
			context.SetShadowWithColor (new SizeF (1.0f, 1.0f), 3, UIColor.Black.CGColor);
			context.FillPath ();
			
			context.RestoreState ();
		}
		
		private void DrawShine (CGContext context, RectangleF rect)
		{
			context.SaveState ();
			
			context.BeginPath ();
			var maxY = MakePath (context, rect);
			context.Clip ();
			
			var locations = new float[] { 0.0f, 0.5f };
			var components = new float[] { 0.92f, 0.92f, 0.92f, 1.0f, 0.82f, 0.82f, 0.82f, 0.4f };
			
			var darkLoc = new float[] { 0.5f, 1.0f };
			var darkComp = new float[] { 0.08f, 0.08f, 0.08f, 0.6f, 0.18f, 0.18f, 0.18f, 0.2f };
			
			using (var cspace = CGColorSpace.CreateDeviceRGB ())
			using (var darkGrad = new CGGradient (cspace, darkComp, darkLoc))
			using (var gradient = new CGGradient (cspace, components, locations)) {
		
				PointF sPoint = new PointF (0f, 0f),
					ePoint = new PointF (0f, maxY);
				context.DrawLinearGradient (gradient, sPoint, ePoint, 0);
			}
			
			context.RestoreState ();
		}
		
		private void DrawBorder (CGContext context, RectangleF rect)
		{
			context.SaveState ();
			
			context.BeginPath ();
			float lineSize = 2f;
			if (this.scaleFactor > 1) {
				lineSize += this.scaleFactor * .25f;
			}
			context.SetLineWidth (lineSize);
			context.SetStrokeColor (this.borderColor.CGColor);
			MakePath (context, rect);
			context.ClosePath ();
			context.StrokePath ();
			
			context.RestoreState ();
		}
	}
}

