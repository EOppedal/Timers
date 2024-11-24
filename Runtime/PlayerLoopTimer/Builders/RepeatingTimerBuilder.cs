namespace PlayerLoopTimer {
    public abstract class RepeatingTimerBuilder : TimerBuilder<RepeatingTimer> {
        protected RepeatingTimerBuilder(RepeatingTimer timer) : base(timer) {
        }
    }
}