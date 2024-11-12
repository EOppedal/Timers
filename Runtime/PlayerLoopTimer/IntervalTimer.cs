using System;

namespace PlayerLoopTimer {
    /// <summary>
    /// Interval timer can be called with a duration or an interval repetitions count
    /// </summary>
    public class IntervalTimer : Timer {
        public event Action OnInterval;

        private float _intervalTime;
        private int _currentIntervalIndex;

        public IntervalTimer(float duration, float intervalTime, TimerTickMethod timerTickMethod = TimerTickMethod.DeltaTimeScaled) : base(duration, timerTickMethod) {
            InitializeTimer(intervalTime);
        }

        public IntervalTimer(int intervalRepetitions, float intervalTime, TimerTickMethod timerTickMethod = TimerTickMethod.DeltaTimeScaled)
            : base(intervalRepetitions * intervalTime, timerTickMethod) {
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