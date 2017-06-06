namespace Spritesheet
{
	using System;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System.Linq;

	public class Animation
	{
		public Animation(Frame[] frames)
		{
			this.Frames = frames;
			this.TotalDuration = frames.Sum(x => x.Duration);
			this.CurrentFrame = this.Frames[0];
		}

		#region Cloning

		public Animation Clone() => new Animation(this.Frames);

		public Animation FlipX() => new Animation(this.Frames.Select(x => x.FlipX()).ToArray());

		public Animation FlipY() => new Animation(this.Frames.Select(x => x.FlipY()).ToArray());

		#endregion 

		private Repeat.Mode repeat;

		public bool IsStarted { get; private set; }

		public Frame CurrentFrame { get; private set; }

		public double Time { get; private set; }

		public Frame[] Frames { get; }

		public double TotalDuration { get; }

		private Frame GetFrame(double amount)
		{
			var time = amount * this.TotalDuration;
			var current = 0.0;

			for (int i = 0; i < Frames.Length; i++)
			{
				var frame = Frames[i];
				current += frame.Duration;
				if (time <= current)
				{
					return frame;
				}
			}

			return null;
		}

		public void Start(Repeat.Mode repeat)
		{
			this.Time = 0;
			this.repeat = repeat;
			this.IsStarted = true;
		}

		public void Pause() => this.IsStarted = false;

		public void Resume() => this.IsStarted = true;

		public void Stop() => this.Pause();

		public bool Update(GameTime time)
		{
			if (this.IsStarted)
			{
				this.Time += time.ElapsedGameTime.TotalMilliseconds;

				var amount = this.UpdateCurrentFrame();

				if (Repeat.IsFinished(repeat, amount))
				{
					this.Stop();
					return true;
				}
			}
			return false;
		}

		private double UpdateCurrentFrame()
		{
			// Updating current frame
			var interval = this.CurrentFrame.Duration;
			var amount = (float)(this.Time / this.TotalDuration);
			var value = Repeat.Calculate(this.repeat, amount);
			var i = Math.Max(0, Math.Min(Frames.Length - 1, (int)(value * Frames.Length)));
			this.CurrentFrame = this.Frames[i];
			return amount;
		}

		public void Reset()
		{
			this.Time = 0;
			this.repeat = Repeat.Mode.Once;
			this.UpdateCurrentFrame();
		}
	}
}
