namespace Spritesheet
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public static class SpriteBatchExtensions
	{
		public static void Draw(this SpriteBatch batch, Frame frame, Vector2 position, Color? color = null, float rotation = 0, Vector2? scale = null, float layerDepth = 0)
		{
			batch.Draw(texture: frame.Texture,
					   position: position,
					   sourceRectangle: frame.Area,
					   color: color ?? Color.White,
					   rotation: rotation,
					   origin: frame.Origin.ToVector2(),
					   scale: scale ?? Vector2.One,
					   effects: frame.Effects,
					   layerDepth: layerDepth);
		}

		public static void Draw(this SpriteBatch batch, Animation animation, Vector2 position, Color? color = null, float rotation = 0, Vector2? scale = null, float layerDepth = 0)
		{
			batch.Draw(animation.CurrentFrame, position, color, rotation, scale, layerDepth);
		}
	}
}
