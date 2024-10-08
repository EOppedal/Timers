using System;

namespace PlayerLoopTimer {
    public class CountdownTimer : Timer {
        public CountdownTimer(float duration, Action timerTickIncrementMethod = null) : base(duration, timerTickIncrementMethod) {
            OnComplete += StopTimer;
        }
    }
}