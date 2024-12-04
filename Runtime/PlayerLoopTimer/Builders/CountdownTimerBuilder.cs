using System.Runtime.InteropServices;
using static PlayerLoopTimer.Timer;

namespace PlayerLoopTimer {
    public class CountdownTimerBuilder : TimerBuilder<CountdownTimer> {
        private CountdownTimerBuilder(CountdownTimer timer) : base(timer) {
        }

        public static CountdownTimerBuilder Start(float duration, [Optional] TickMethod tickMethod) {
            return new CountdownTimerBuilder(new CountdownTimer(duration, tickMethod));
        }
    }
}