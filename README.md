# Timer System for Unity
A flexible timer system for Unity that allows you to run code on each frame/tick, as well as on completion. It supports pause, restart, and other controls, making it easy to manage time-based events.

## Features
Run code each frame/tick and upon completion.
Supports pause, resume, restart, and stop functionality.
Easy integration with Unity's lifecycle.
## Example Usage: 
```csharp
private void Start() {
    var repeatingTimer = new RepeatingTimer(2, 2);

    repeatingTimer.OnBegin += () => Debug.Log($"Begin | time: {Time.time}");
    repeatingTimer.OnUpdate += () => Debug.Log($"Update | ElapsedTime: {repeatingTimer.ElapsedTime} | Duration: {repeatingTimer.Duration}");
    repeatingTimer.OnComplete += () => Debug.Log($"Complete | CurrentRepeatCount: {repeatingTimer.CurrentRepeatCount} | time: {Time.time}");
    repeatingTimer.OnStop += () => Debug.Log("Stop: " + repeatingTimer.RepeatCount);

    repeatingTimer.StartTimer();
}
```

### In this example:

- The timer starts and triggers OnBegin when it starts.
- It logs OnUpdate on each frame/tick with the current elapsed time.
- Once the timer completes, it triggers OnComplete with the repeat count.
- You can stop the timer manually, which triggers OnStop.

## Installation
Clone or download this repository.
Add it to your Unity project under the Packages folder or reference it via Git.
