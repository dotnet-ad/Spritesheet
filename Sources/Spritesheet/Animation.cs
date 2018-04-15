namespace Spritesheet {
    using System;
    using Microsoft.Xna.Framework;
    using System.Linq;

    public class Animation {
        public Animation(Frame[] frames) {
            Frames = frames;
            TotalDuration = frames.Sum(x => x.Duration);
            CurrentFrame = Frames[0];
        }

        #region Cloning
        public Animation Clone() => new Animation(Frames);
        public Animation FlipX() => new Animation(Frames.Select(x => x.FlipX()).ToArray());
        public Animation FlipY() => new Animation(Frames.Select(x => x.FlipY()).ToArray());
        #endregion

        private Repeat.Mode repeat;

        public bool IsStarted { get; private set; }

        public Frame CurrentFrame { get; private set; }

        public double Time { get; private set; }

        public Frame[] Frames { get; }

        public double TotalDuration { get; }

        private Frame GetFrame(double amount) {
            var time = amount * TotalDuration;
            var current = 0.0;

            for (int i = 0; i < Frames.Length; i++) {
                var frame = Frames[i];
                current += frame.Duration;
                if (time <= current) {
                    return frame;
                }
            }

            return null;
        }

        public void Start(Repeat.Mode _repeat) {
            Time = 0;
            repeat = _repeat;
            IsStarted = true;
        }

        public void Pause() => IsStarted = false;

        public void Resume() => IsStarted = true;

        public void Stop() => Pause();

        public bool Update(GameTime time) {
            if (IsStarted) {
                Time += time.ElapsedGameTime.TotalMilliseconds;

                var amount = UpdateCurrentFrame();

                if (Repeat.IsFinished(repeat, amount)) {
                    Stop();
                    return true;
                }
            }
            return false;
        }

        private double UpdateCurrentFrame() {
            // Updating current frame
            var interval = CurrentFrame.Duration;
            var amount = (float)(Time / TotalDuration);
            var value = Repeat.Calculate(repeat, amount);
            var i = Math.Max(0, Math.Min(Frames.Length - 1, (int)(value * Frames.Length)));
            CurrentFrame = Frames[i];
            return amount;
        }

        public void Reset() {
            Time = 0;
            repeat = Repeat.Mode.Once;
            UpdateCurrentFrame();
        }
    }
}
