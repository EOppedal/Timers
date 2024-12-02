using System.Runtime.InteropServices;

namespace PlayerLoopTimer {
    public class CountdownTimerBuilder : TimerBuilder<CountdownTimer> {
        protected CountdownTimerBuilder(CountdownTimer timer) : base(timer) {
        }

        public static CountdownTimerBuilder Start(float duration, [Optional] Timer.TimerTickMethod timerTickMethod) {
            return new CountdownTimerBuilder(new CountdownTimer(duration, timerTickMethod));
        }
    }
}