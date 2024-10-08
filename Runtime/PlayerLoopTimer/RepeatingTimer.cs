using System;

namespace PlayerLoopTimer {
    public class RepeatingTimer : Timer {
        public int RepeatCount;
        public int CurrentRepeatCount;

        public RepeatingTimer(float duration, int repeatCount = -1, Action timerTickIncrementMethod = null) : base(duration,
            timerTickIncrementMethod) {
            OnComplete += () => ElapsedTime = 0;

            if (repeatCount > 0) {
                RepeatCount = repeatCount;
                OnComplete += () => {
                    CurrentRepeatCount++;

                    if (CurrentRepeatCount >= RepeatCount) {
                        StopTimer();
                    }
                };
            }
        }

        public override void RestartTimer() {
            CurrentRepeatCount = 0;

            base.RestartTimer();
        }
    }
}