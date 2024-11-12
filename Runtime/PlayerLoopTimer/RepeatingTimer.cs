namespace PlayerLoopTimer {
    public class RepeatingTimer : Timer {
        public int RepeatCount;
        public int CurrentRepeatCount;

        public RepeatingTimer(float duration, int repeatCount = -1, 
                TimerTickMethod timerTickMethod = TimerTickMethod.DeltaTimeScaled) : base(duration, timerTickMethod) {
            OnComplete += () => ElapsedTime = 0;
            OnComplete += InvokeOnBegin;

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