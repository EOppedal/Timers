﻿using System;
using UnityEngine;

namespace PlayerLoopTimer {
    public abstract class Timer : ITimer {
        #region ---Fields---
        public float Duration { get; set; }
        public float ElapsedTime { get; protected set; }
        public bool IsRunning { get; protected set; }
        public bool IsPaused { get; protected set; }

        public float CurrentProgress => ElapsedTime / Duration;

        protected readonly Action TimerTick;

        #region ***---Events---***
        public event Action OnBegin = delegate { };
        public event Action OnUpdate = delegate { };
        public event Action OnStop = delegate { };
        public event Action OnComplete = delegate { };
        public event Action OnRestart = delegate { };
        public event Action OnPause = delegate { };
        public event Action OnResume = delegate { };
        #endregion
        #endregion

        public enum TickMethod { DeltaTimeScaled, DeltaTimeUnscaled }

        protected Timer(float duration, TickMethod tickMethod = TickMethod.DeltaTimeScaled) {
            Duration = duration;
            TimerTick = tickMethod switch {
                TickMethod.DeltaTimeUnscaled => () => ElapsedTime += Time.unscaledDeltaTime,
                _ => () => ElapsedTime += Time.deltaTime
            };
        }

        public virtual void StartTimer() {
            if (IsRunning) return;

            ElapsedTime = 0;
            IsRunning = true;
            OnBegin.Invoke();

            TimerController.AddTimer(this);
        }

        /// <summary>
        /// Early returns if 'IsPaused == true'
        /// </summary>
        public virtual void UpdateTimer() {
            if (IsPaused) return;

            TimerTick();

            OnUpdate.Invoke();

            if (ElapsedTime >= Duration) {
                OnComplete.Invoke();
            }
        }

        public virtual void StopTimer() {
            if (!IsRunning) return;
            OnStop.Invoke();

            TimerController.RemoveTimer(this);

            IsRunning = false;
        }

        public virtual void RestartTimer() {
            OnRestart.Invoke();

            if (IsRunning) {
                OnStop.Invoke();
                IsRunning = false;
            }
            else {
                TimerController.AddTimer(this);
            }

            ElapsedTime = 0;
            IsRunning = true;
            OnBegin.Invoke();
        }

        public virtual void PauseTimer() {
            if (IsPaused) return;
            
            OnPause.Invoke();
            IsPaused = true;
        }

        public virtual void ResumeTimer() {
            if (!IsPaused) return;
            
            OnResume.Invoke();
            IsPaused = false;
        }

        #region ---ProtectedInvocation---
        protected void InvokeOnBegin() => OnBegin.Invoke();

        protected void InvokeOnUpdate() => OnUpdate.Invoke();

        protected void InvokeOnStop() => OnStop.Invoke();

        protected void InvokeOnComplete() => OnComplete.Invoke();

        protected void InvokeOnRestart() => OnRestart.Invoke();

        protected void InvokeOnPause() => OnPause.Invoke();

        protected void InvokeOnResume() => OnResume.Invoke();
        #endregion
    }
}