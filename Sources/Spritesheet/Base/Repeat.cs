namespace Spritesheet {
    using System;

    public static class Repeat {
        /// <summary>
        /// A repeat mode for any kind of animation.
        /// </summary>
        public enum Mode {
            Once,
            OnceWithReverse,
            Loop,
            LoopWithReverse,
            Reverse,
        }
        /// <summary>
        /// Calculate the current value for a time between zero and one, and a curve mode.
        /// </summary>
        public static double Calculate(Mode mode, double time) {
            if (mode == Mode.Once)
                time = Math.Min(Math.Max(time, 0.0f), 1.0f);
            else if (mode == Mode.Reverse)
                time = 1.0f - Math.Min(Math.Max(time, 0.0f), 1.0f);
            else if (mode == Mode.Loop)
                time %= 1.0f;
            else if (mode == Mode.LoopWithReverse)
                time %= 2.0f;
            if ((mode == Mode.OnceWithReverse || mode == Mode.LoopWithReverse) && time > 1.0f) {
                time = 2.0f - time;
            }
            return time;
        }

        /// <summary>
        /// Indicates whether the animation is finished.
        /// </summary>
        public static bool IsFinished(Mode mode, double time) => (((mode == Repeat.Mode.Once || mode == Repeat.Mode.Reverse) && time >= 1.0f) ||
                                                                (mode == Repeat.Mode.OnceWithReverse && time >= 2.0f));
    }
}
