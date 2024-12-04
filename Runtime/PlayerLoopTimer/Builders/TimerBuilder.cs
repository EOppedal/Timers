using System;

namespace PlayerLoopTimer {
    public class TimerBuilder<T> : ITimerBuilderWithRequirements<T> where T : Timer {
        private readonly T _timer;

        protected TimerBuilder(T timer) {
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

        private void AddActions(Action[] actions, Action<Action> addHandler) {
            if (actions == null) throw new ArgumentNullException(nameof(actions));

            lock (_timer) {
                foreach (var action in actions) {
                    addHandler(action);
                }
            }
        }

        #region ***---OptionalActionSetters---***
        public ITimerBuilderWithRequirements<T> WithOnBegin(params Action[] actions) {
            AddActions(actions, action => _timer.OnBegin += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnUpdate(params Action[] actions) {
            AddActions(actions, action => _timer.OnUpdate += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnStop(params Action[] actions) {
            AddActions(actions, action => _timer.OnStop += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnComplete(params Action[] actions) {
            AddActions(actions, action => _timer.OnComplete += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnRestart(params Action[] actions) {
            AddActions(actions, action => _timer.OnRestart += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnPause(params Action[] actions) {
            AddActions(actions, action => _timer.OnPause += action);
            return this;
        }

        public ITimerBuilderWithRequirements<T> WithOnResume(params Action[] actions) {
            AddActions(actions, action => _timer.OnResume += action);
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