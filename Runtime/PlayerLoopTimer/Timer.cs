using System;
using UnityEngine;

namespace PlayerLoopTimer {
    public abstract class Timer {
        public float Duration;
        public float ElapsedTime;

        protected bool Paused;

        protected readonly Action TimerTickIncrementMethod;

        public event Action OnBegin = delegate { };
        public event Action OnUpdate = delegate { };
        public event Action OnStop = delegate { };
        public event Action OnComplete = delegate { };
        public event Action OnRestart = delegate { };
        public event Action OnPause = delegate { };
        public event Action OnResume = delegate { };

        protected Timer(float duration, Action timerTickIncrementMethod = null) {
            Duration = duration;
            TimerTickIncrementMethod =
                timerTickIncrementMethod ?? (() => ElapsedTime += Time.deltaTime * Time.timeScale);
        }

        public virtual void StartTimer() {
            OnBegin.Invoke();

            TimerController.AddTimer(this);
        }

        public void UpdateTimer() {
            if (Paused) return;

            TimerTickIncrementMethod();

            OnUpdate.Invoke();

            if (ElapsedTime >= Duration) {
                OnComplete.Invoke();
            }
        }

        public virtual void StopTimer() {
            OnStop.Invoke();

            TimerController.RemoveTimer(this);
        }

        public virtual void RestartTimer() {
            OnRestart.Invoke();

            ElapsedTime = 0;
            OnBegin.Invoke();
        }

        public virtual void PauseTimer() {
            OnPause.Invoke();

            Paused = true;
        }

        public virtual void ResumeTimer() {
            OnResume.Invoke();

            Paused = false;
        }
    }
}