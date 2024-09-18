using System;
using System.Threading;
using UnityEngine;

public class Timer {
    public event Action OnComplete = delegate { };
    public event Action OnUpdate = delegate { };
    public event Action OnBegin = delegate { };
    public event Action OnCancel = delegate { };

    public float TotalTime;
    public float ProgressTime => TotalTime * CurrentProgress;
    public float TimeRemaining => TotalTime - ProgressTime;

    public float CurrentProgress {
        get => _currentProgress;
        protected set {
            _currentProgress = Mathf.Clamp01(value);
            OnUpdate.Invoke();
        }
    }

    private float _currentProgress;
    protected CancellationTokenSource CancellationTokenSource;
    public bool IsRunning {get; protected set;}

    public Timer(float time, bool startImmediately = true) {
        TotalTime = time;
        if (startImmediately) _ = StartTimer();
    }

    public virtual async Awaitable StartTimer() {
        if (IsRunning) return;

        CancellationTokenSource = new CancellationTokenSource();
        IsRunning = true;
        CurrentProgress = 0;
        OnBegin.Invoke();

        try {
            while (CurrentProgress < 1) {
                await Awaitable.NextFrameAsync(CancellationTokenSource.Token);
                CurrentProgress += Time.deltaTime / TotalTime * Time.timeScale;
            }

            OnComplete.Invoke();
        }
        catch (OperationCanceledException) {
            OnCancel.Invoke();
        }
        finally {
            IsRunning = false;
            CancellationTokenSource.Dispose();
        }
    }

    public void CancelTimer() {
        if (!IsRunning) return;

        CancellationTokenSource?.Cancel();
    }

    public void RestartTimer() {
        if (IsRunning) {
            CancelTimer();
        }
        
        _ = StartTimer();
    }

    protected void CallBeginEvent() => OnBegin.Invoke();

    protected void CallUpdateEvent() => OnUpdate.Invoke();

    protected void CallCompleteEvent() => OnComplete.Invoke();

    protected void CallCancelEvent() => OnCancel.Invoke();
}