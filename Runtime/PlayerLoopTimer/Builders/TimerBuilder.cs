using System;

namespace PlayerLoopTimer {
    public class TimerBuilder<T> : ITimerBuilderWithRequirements<T> where T : Timer {
        private readonly T _timer;

        public TimerBuilder(T timer) {
            _timer = timer;
        }
        
        public static TimerBuilder<T> Start(T timer) {
            return new TimerBuilder<T>(timer);
        }

        #region ---ITimerBuilderWithDuration<T>---
        #region ***---ReturnsTimer---***
        public T Finish() {
            lock (_timer) {
                return _timer;
            }
        }

        public T FinishAndStartTimer() {
            lock (_timer) {
                _timer.StartTimer();
                return _timer;
            }
        }
        #endregion

        #region ***---OptionalActionSetters---***
        public ITimerBuilderWithRequirements<T> WithOnBegin(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            lock (_timer) {
                _timer.OnBegin += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnUpdate(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnUpdate += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnStop(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnStop += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnComplete(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnComplete += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnRestart(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnRestart += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnPause(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnPause += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnResume(Action action) {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_timer) {
                _timer.OnResume += action;
            }

            return this;
        }

        public ITimerBuilderWithRequirements<T> WithDuration(float duration) {
            lock (_timer) {
                _timer.Duration = duration;
            }
            
            return this;
        }
        #endregion
        #endregion
    }
}