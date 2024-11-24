namespace PlayerLoopTimer {
    public abstract class IntervalTimerBuilder : TimerBuilder<IntervalTimer> {
        protected IntervalTimerBuilder(IntervalTimer timer) : base(timer) {
        }
    }
}