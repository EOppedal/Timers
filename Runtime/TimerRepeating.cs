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
        if (IsRunning) return;

        CancellationTokenSource = new CancellationTokenSource();
        IsRunning = true;
        CallBeginEvent();

        try {
            while (TimesToRepeat == -1 || TimesRepeated <= TimesToRepeat ) {
                CurrentProgress = 0;
                TimesRepeated++;
                
                while (CurrentProgress < 1) {
                    await Awaitable.NextFrameAsync(CancellationTokenSource.Token);
                    CurrentProgress += Time.deltaTime / TotalTime * Time.timeScale;
                }

                CallCompleteEvent();
            }
        }
        catch (OperationCanceledException) {
            CallCancelEvent();
        }
        finally {
            IsRunning = false;
            CancellationTokenSource.Dispose();
        }
    }
}