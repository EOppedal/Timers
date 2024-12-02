using System.Runtime.InteropServices;

namespace PlayerLoopTimer {
    public class IntervalTimerBuilder : TimerBuilder<IntervalTimer> {
        protected IntervalTimerBuilder(IntervalTimer timer) : base(timer) {
        }

        public static IntervalTimerBuilder Start(float duration, float intervalTime,
                [Optional] Timer.TimerTickMethod timerTickMethod) {
            return new IntervalTimerBuilder(new IntervalTimer(duration, intervalTime, timerTickMethod));
        }

        public static IntervalTimerBuilder Start(int intervalRepetitions, float intervalTime,
                [Optional] Timer.TimerTickMethod timerTickMethod) {
            return new IntervalTimerBuilder(new IntervalTimer(intervalRepetitions, intervalTime, timerTickMethod));
        }
    }
}