using System;

public interface ITimer {
    public float Duration { get; set; }
    public float ElapsedTime { get; }
    public bool IsRunning { get; }

    void StartTimer();
    void StopTimer();

    public event Action OnBegin;
    public event Action OnUpdate;
    public event Action OnStop;
    public event Action OnComplete;
}