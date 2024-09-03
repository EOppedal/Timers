using System;
using System.Threading;
using UnityEngine;

/// <inheritdoc />
public class TimerRepeating : Timer {
    public int TimesRepeated;
    public readonly int TimesToRepeat;
    public TimerRepeating(float time, int timesToRepeat = -1, bool startImmediately = true) : base(time, startImmediately) {
        TimesToRepeat = timesToRepeat;
        TimesRepeated = 0;
    }

    public override async Awaitable StartTimer() {
        if (_isRunning) return;

        _cancellationTokenSource = new CancellationTokenSource();
        _isRunning = true;
        OnBegin.Invoke();

        try {
            while (TimesToRepeat == -1 || TimesRepeated <= TimesToRepeat ) {
                CurrentProgress = 0;
                TimesRepeated++;
                
                while (CurrentProgress < 1) {
                    await Awaitable.NextFrameAsync(_cancellationTokenSource.Token);
                    CurrentProgress += Time.deltaTime / TotalTime * Time.timeScale;
                }

                OnComplete.Invoke();
            }
        }
        catch (OperationCanceledException) {
            OnCancel.Invoke();
        }
        finally {
            _isRunning = false;
            _cancellationTokenSource.Dispose();
        }
    }
}