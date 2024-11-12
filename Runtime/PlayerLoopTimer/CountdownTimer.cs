using System;

namespace PlayerLoopTimer {
    public class CountdownTimer : Timer {
        public CountdownTimer(float duration, TimerTickMethod timerTickMethod = TimerTickMethod.DeltaTimeScaled) : base(duration, timerTickMethod) {
            OnComplete += StopTimer;
        }
    }
}