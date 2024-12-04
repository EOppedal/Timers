namespace PlayerLoopTimer {
    public class CountdownTimer : Timer {
        public CountdownTimer(float duration, TickMethod tickMethod = TickMethod.DeltaTimeScaled) : base(duration, tickMethod) {
            OnComplete += StopTimer;
        }
    }
}