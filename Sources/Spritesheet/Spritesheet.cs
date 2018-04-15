namespace Spritesheet {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Linq;

    public class SpriteSheet {
        public SpriteSheet(Texture2D texture) : this(texture, null, null, null) { }

        private SpriteSheet(Texture2D texture, Point? cellSize = null, Point? cellOffset = null, Point? cellOrigin = null, double frameDuration = 200.0f, SpriteEffects frameEffects = SpriteEffects.None) {
            Texture = texture;
            CellSize = cellSize ?? new Point(32, 32);
            CellOffset = cellOffset ?? new Point(0, 0);
            CellOrigin = cellOrigin ?? CellSize / new Point(2, 2);
            FrameDefaultDuration = frameDuration;
            FrameDefaultEffects = frameEffects;
        }

        #region Properties

        public Texture2D Texture { get; }

        public Point CellSize { get; }

        public Point CellOffset { get; }

        public Point CellOrigin { get; }

        public double FrameDefaultDuration { get; }

        public SpriteEffects FrameDefaultEffects { get; }

        #endregion

        #region Grid

        public SpriteSheet WithGrid((int w, int h) cell, (int x, int y) offset, (int x, int y) cellOrigin) {
            return new SpriteSheet(this.Texture, new Point(cell.w, cell.h), new Point(offset.x, offset.y), new Point(cellOrigin.x, cellOrigin.y));
        }

        public SpriteSheet WithGrid((int w, int h) cell, (int x, int y) offset) {
            return this.WithGrid(cell, offset, (cell.w / 2, cell.h / 2));
        }

        public SpriteSheet WithGrid((int w, int h) cell) {
            return this.WithGrid(cell, (0, 0), (cell.w / 2, cell.h / 2));
        }

        #endregion

        #region Frame default settings

        public SpriteSheet WithFrameDuration(double durationInMs) {
            return new SpriteSheet(this.Texture, this.CellSize, this.CellOffset, this.CellOrigin, durationInMs, this.FrameDefaultEffects);
        }

        public SpriteSheet WithFrameEffect(SpriteEffects effects) {
            return new SpriteSheet(this.Texture, this.CellSize, this.CellOffset, this.CellOrigin, this.FrameDefaultDuration, effects);
        }

        #endregion

        public Frame CreateFrame(int x, int y, double duration = 0.0, SpriteEffects effects = SpriteEffects.None) {
            x = x * CellSize.X + CellOffset.X;
            y = y * CellSize.Y + CellOffset.Y;
            var area = new Rectangle(x, y, CellSize.X, CellSize.Y);
            return new Frame(Texture, area, CellOrigin, duration, effects);
        }

        public Animation CreateAnimation(params (int x, int y, double duration, SpriteEffects effects)[] frames) {
            return new Animation(frames.Select(f => CreateFrame(f.x, f.y, f.duration, f.effects)).ToArray());
        }

        public Animation CreateAnimation(params (int x, int y, double duration)[] frames) {
            return this.CreateAnimation(frames.Select(x => (x.x, x.y, x.duration, FrameDefaultEffects)).ToArray());
        }

        public Animation CreateAnimation(params (int x, int y)[] frames) {
            return this.CreateAnimation(frames.Select(x => (x.x, x.y, FrameDefaultDuration, FrameDefaultEffects)).ToArray());
        }
    }
}
