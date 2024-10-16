using System;
using UnityEngine;

namespace PlayerLoopTimer {
    public abstract class Timer {
        public float Duration;
        public float ElapsedTime;
        public float CurrentProgress => ElapsedTime / Duration;

        protected bool Paused;

        protected readonly Action TimerTick;

        public bool TimerRunning { private set; get; }

        public event Action OnBegin = delegate { };
        public event Action OnUpdate = delegate { };
        public event Action OnStop = delegate { };
        public event Action OnComplete = delegate { };
        public event Action OnRestart = delegate { };
        public event Action OnPause = delegate { };
        public event Action OnResume = delegate { };

        protected Timer(float duration, Action timerTickIncrementMethod = null) {
            Duration = duration;
            TimerTick = timerTickIncrementMethod ?? (() => ElapsedTime += Time.deltaTime * Time.timeScale);
        }

        public virtual void StartTimer() {
            if (TimerRunning) return;
            
            ElapsedTime = 0;
            TimerRunning = true;
            OnBegin.Invoke();

            TimerController.AddTimer(this);
        }

        public void UpdateTimer() {
            if (Paused) return;

            TimerTick();

            OnUpdate.Invoke();

            if (ElapsedTime >= Duration) {
                OnComplete.Invoke();
            }
        }

        public virtual void StopTimer() {
            if (!TimerRunning) return;
            OnStop.Invoke();

            TimerController.RemoveTimer(this);
            
            TimerRunning = false;
        }

        public virtual void RestartTimer() {
            OnRestart.Invoke();

            StopTimer();
            StartTimer();
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