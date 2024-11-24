namespace PlayerLoopTimer {
    public abstract class CountdownTimerBuilder : TimerBuilder<CountdownTimer> {
        protected CountdownTimerBuilder(CountdownTimer timer) : base(timer) {
        }
    }
}