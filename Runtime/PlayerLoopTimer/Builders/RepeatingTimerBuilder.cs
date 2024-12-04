using System.Runtime.InteropServices;
using static PlayerLoopTimer.Timer;

namespace PlayerLoopTimer {
    public class RepeatingTimerBuilder : TimerBuilder<RepeatingTimer> {
        private RepeatingTimerBuilder(RepeatingTimer timer) : base(timer) {
        }

        public static RepeatingTimerBuilder Start(float duration, [Optional] int repeatCount, [Optional] TickMethod tickMethod) {
            return new RepeatingTimerBuilder(new RepeatingTimer(duration, repeatCount, tickMethod));
        }
    }
}