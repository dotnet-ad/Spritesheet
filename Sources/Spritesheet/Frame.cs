namespace Spritesheet
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class Frame
	{
		public Frame(Texture2D texture, Rectangle area, Point origin, double duration, SpriteEffects effects)
		{
			this.Texture = texture;
			this.Area = area;
			this.Origin = origin;
			this.Duration = duration;
			this.Effects = effects;
		}

		public Texture2D Texture { get; }

		public Rectangle Area { get; }

		public Point Origin { get; }

		public double Duration { get; }

		public SpriteEffects Effects { get; }

		#region Cloning

		public Frame FlipX()
		{
			return new Frame(this.Texture, this.Area, this.Origin, this.Duration, SpriteEffects.FlipHorizontally);
		}

		public Frame FlipY()
		{
			return new Frame(this.Texture, this.Area, this.Origin, this.Duration, SpriteEffects.FlipVertically);
		}

		public Frame WithDuration(double duration)
		{
			return new Frame(this.Texture, this.Area, this.Origin, duration, this.Effects);
		}

		public Frame WithOrigin(int x, int y)
		{
			return new Frame(this.Texture, this.Area, new Point(x, y), this.Duration, this.Effects);
		}

		public Frame WithArea(Rectangle area)
		{
			return new Frame(this.Texture, area, this.Origin, this.Duration, this.Effects);
		}

		#endregion
	}
}
