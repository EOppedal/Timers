using System;

namespace PlayerLoopTimer {
    /// <summary>
    /// Interval timer can be called with a duration or an interval repetitions count
    /// </summary>
    public class IntervalTimer : Timer {
        public event Action OnInterval;

        private float _intervalTime;
        private int _currentIntervalIndex;

        public IntervalTimer(float duration, float intervalTime, TickMethod tickMethod = TickMethod.DeltaTimeScaled) 
            : base(duration, tickMethod) {
            InitializeTimer(intervalTime);
        }

        public IntervalTimer(int repetitions, float intervalTime, TickMethod tickMethod = TickMethod.DeltaTimeScaled)
            : base(repetitions * intervalTime, tickMethod) {
            InitializeTimer(intervalTime);
        }

        private void InitializeTimer(float intervalTime) {
            _intervalTime = intervalTime;

            OnComplete += StopTimer;
            OnUpdate += CheckForInterval;
        }

        private void CheckForInterval() {
            var intervalsPassed = (int)(ElapsedTime / _intervalTime);

            while (_currentIntervalIndex < intervalsPassed) {
                _currentIntervalIndex++;
                OnInterval?.Invoke();
            }
        }

        public override void RestartTimer() {
            _currentIntervalIndex = 0;

            base.RestartTimer();
        }
    }
}