using System;
using System.Threading;
using UnityEngine;

public class AwaitableTimer {
    public event Action OnComplete = delegate { };
    public event Action OnUpdate = delegate { };
    public event Action OnBegin = delegate { };
    public event Action OnCancel = delegate { };

    public float TotalTime;
    public float ProgressTime => TotalTime * CurrentProgress;
    public float TimeRemaining => TotalTime - ProgressTime;

    public float CurrentProgress {
        get => _currentProgress;
        private set {
            _currentProgress = Mathf.Clamp01(value);
            OnUpdate.Invoke();
        }
    }

    private float _currentProgress;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isRunning;

    public AwaitableTimer(float time, bool startImmediately = true) {
        TotalTime = time;
        if (startImmediately) _ = StartTimer();
    }

    public async Awaitable StartTimer() {
        if (_isRunning) return;

        _cancellationTokenSource = new CancellationTokenSource();
        _isRunning = true;
        OnBegin.Invoke();

        try {
            while (CurrentProgress < 1) {
                await Awaitable.NextFrameAsync(_cancellationTokenSource.Token);
                CurrentProgress += Time.deltaTime / TotalTime * Time.timeScale;
            }

            OnComplete.Invoke();
        }
        catch (OperationCanceledException) {
            OnCancel.Invoke();
        }
        finally {
            _isRunning = false;
            _cancellationTokenSource.Dispose();
        }
    }

    public void CancelTimer() {
        if (!_isRunning) return;

        _cancellationTokenSource?.Cancel();
    }
}