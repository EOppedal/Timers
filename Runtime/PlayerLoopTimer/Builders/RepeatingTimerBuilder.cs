using System.Runtime.InteropServices;

namespace PlayerLoopTimer {
    public class RepeatingTimerBuilder : TimerBuilder<RepeatingTimer> {
        public RepeatingTimerBuilder(RepeatingTimer timer) : base(timer) {
        }

        public static RepeatingTimerBuilder Start(float duration, [Optional] int repeatCount,
                [Optional] Timer.TimerTickMethod timerTickMethod) {
            return new RepeatingTimerBuilder(new RepeatingTimer(duration, repeatCount, timerTickMethod));
        }
    }
}