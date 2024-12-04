using System.Runtime.InteropServices;
using static PlayerLoopTimer.Timer;

namespace PlayerLoopTimer {
    public class IntervalTimerBuilder : TimerBuilder<IntervalTimer> {
        private IntervalTimerBuilder(IntervalTimer timer) : base(timer) {
        }

        public static IntervalTimerBuilder Start(float duration, float intervalTime, [Optional] TickMethod tickMethod) {
            return new IntervalTimerBuilder(new IntervalTimer(duration, intervalTime, tickMethod));
        }

        public static IntervalTimerBuilder Start(int repetitions, float intervalTime, [Optional] TickMethod tickMethod) {
            return new IntervalTimerBuilder(new IntervalTimer(repetitions, intervalTime, tickMethod));
        }
    }
}