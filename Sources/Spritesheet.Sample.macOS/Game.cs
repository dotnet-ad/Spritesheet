namespace Spritesheet.Sample.macOS
{
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.IO;
	using Microsoft.Xna.Framework.Input;

	public class Game : Microsoft.Xna.Framework.Game
	{
		#region Fields

		private GraphicsDeviceManager graphics;

		private SpriteBatch spriteBatch;

		private Spritesheet sheet;

		private Animation[] animations;

		private Animation anim;

		private PixelFont font;

		private int animIndex = -1;

		#endregion

		public Game()
		{
			this.IsMouseVisible = true;
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
			this.font = new PixelFont();
		}

		private Texture2D LoadTexture(string name)
		{
			using (var fileStream = new FileStream($"Content/{name}.png", FileMode.Open))
			{
				return Texture2D.FromStream(this.graphics.GraphicsDevice, fileStream);
			}
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			this.font.LoadContent(this.GraphicsDevice);

			this.sheet = new Spritesheet(LoadTexture("characters"))
										.WithGrid((32, 32));

			this.animations = new[]
			{
				this.sheet.WithFrameEffect(SpriteEffects.FlipHorizontally).CreateAnimation((0, 1), (1, 1), (2, 1)),
				this.sheet.CreateAnimation((0, 1), (1, 1), (2, 1)),
				this.sheet.WithFrameEffect(SpriteEffects.FlipHorizontally).CreateAnimation((0, 0), (1, 0), (2, 0)),
				this.sheet.CreateAnimation((0, 0), (1, 0), (2, 0)),
			};

			this.NextAnimation();

		}

		private void NextAnimation()
		{
			this.animIndex = (this.animIndex + 1) % this.animations.Length;
			this.anim = this.animations[animIndex];
			this.anim.Start(Repeat.Mode.Loop);
		}

		private MouseState state;

		protected override void Update(GameTime gameTime)
		{
			this.anim.Update(gameTime);

			var newState = Mouse.GetState();

			if(state.LeftButton == ButtonState.Released && newState.LeftButton == ButtonState.Pressed)
			{
				this.NextAnimation();
			}

			state = newState;

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();

			var borders = new Color(1.0f, 1.0f, 1.0f, 0.2f);

			// Full texture
			spriteBatch.Draw(this.sheet.Texture, new Vector2(0, 0));
			spriteBatch.DrawRectangle(this.sheet.Texture.Bounds, borders);

			// Animation
			spriteBatch.Draw(this.anim, new Vector2(64, 64 + this.sheet.Texture.Height));
			this.font.Draw(spriteBatch, new Vector2(12, 12 + this.sheet.Texture.Height), $"{animIndex}", Color.White);

			// Frames in full texture
			for (int ia = 0; ia < anim.Frames.Length; ia++)
			{
				var a = anim.Frames[ia];

				spriteBatch.DrawRectangle(a.Area, borders);
				spriteBatch.DrawCross(a.Area.Location.ToVector2() + a.Origin.ToVector2(), borders);
				this.font.Draw(spriteBatch, a.Area.Location.ToVector2() + new Vector2(4, 4), $"{ia}", borders);
			}

			//Mouse
			const int mouseGrid = 16;
			var mx = (this.state.X / mouseGrid) * mouseGrid;
			var my = (this.state.Y / mouseGrid) * mouseGrid;
			spriteBatch.DrawLine(0, my, this.GraphicsDevice.Viewport.Width, my, Color.Green);
			spriteBatch.DrawLine(mx, 0, mx, this.GraphicsDevice.Viewport.Height, Color.Blue);
			this.font.Draw(spriteBatch, new Vector2(mx, my) + new Vector2(32,32), $"({mx},{my})", borders);

			spriteBatch.End();

			base.Draw(gameTime);
		}

	}
}
