namespace Spritesheet {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Frame {
        public Frame(Texture2D texture, Rectangle area, Point origin, double duration, SpriteEffects effects) {
            Texture = texture;
            Area = area;
            Origin = origin;
            Duration = duration;
            Effects = effects;
        }

        public Texture2D Texture { get; }

        public Rectangle Area { get; }

        public Point Origin { get; }

        public double Duration { get; }

        public SpriteEffects Effects { get; }

        #region Cloning

        public Frame FlipX() {
            return new Frame(Texture, Area, Origin, Duration, SpriteEffects.FlipHorizontally);
        }

        public Frame FlipY() {
            return new Frame(Texture, Area, Origin, Duration, SpriteEffects.FlipVertically);
        }

        public Frame WithDuration(double duration) {
            return new Frame(Texture, Area, Origin, duration, Effects);
        }

        public Frame WithOrigin(int x, int y) {
            return new Frame(Texture, Area, new Point(x, y), Duration, Effects);
        }

        public Frame WithArea(Rectangle area) {
            return new Frame(Texture, area, Origin, Duration, Effects);
        }

        #endregion
    }
}
