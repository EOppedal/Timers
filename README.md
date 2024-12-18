# Timer System for Unity
A flexible timer system for Unity that allows you to run code on each frame/tick, as well as on completion. It supports pause, restart, and other controls, making it easy to manage time-based events.

## Features
Run code each frame and upon events such as OnBegin, OnUpdate, OnStop, OnComplete, OnRestart, OnPause, OnResume.

## Example Usage: 
```csharp
private void Start() {
    var repeatingTimer = new RepeatingTimer(duration: 2, repeatCount: 2);
    repeatingTimer.OnBegin += () => Debug.Log($"Begin | time: {Time.time}");
    repeatingTimer.OnUpdate += () => Debug.Log($"Update | ElapsedTime: {repeatingTimer.ElapsedTime} | Duration: {repeatingTimer.Duration}");
    repeatingTimer.OnComplete += () => Debug.Log($"Complete | CurrentRepeatCount: {repeatingTimer.CurrentRepeatCount} | time: {Time.time}");
    repeatingTimer.OnStop += () => Debug.Log("Stop: " + repeatingTimer.RepeatCount);
    repeatingTimer.StartTimer();

    _rTimer = RepeatingTimerBuilder.Start(duration: 2, repeatCount: 2)
        .WithOnBegin(() => Debug.Log($"Begin | time: {Time.time}"))
        .WithOnUpdate(() => Debug.Log($"Update | ElapsedTime: {Time.time}"))
        .WithOnComplete(() => Debug.Log($"Complete | ElapsedTime: {Time.time}"))
        .WithOnStop(() => Debug.Log($"Stop | ElapsedTime: {Time.time}"))
        .FinishAndStartTimer();

    var countdownTimer = CountdownTimerBuilder.Start(new CountdownTimer(duration: 2))
        .WithOnComplete(() => Debug.Log($"Complete | time: {Time.time}"))
        .Finish();
}
```

### In this example:
A new RepeatingTimer is created and anonymous functions are run by different events invoked by the timer at different times. 

## Installation
'Install package from Git URL' in the 'Package Manager'.
