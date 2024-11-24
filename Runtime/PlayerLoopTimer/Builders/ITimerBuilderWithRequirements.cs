using System;

namespace PlayerLoopTimer {
    public interface ITimerBuilderWithRequirements<out T> where T : Timer {
        T Finish();

        T FinishAndStartTimer();

        ITimerBuilderWithRequirements<T> WithOnBegin(Action action);
        ITimerBuilderWithRequirements<T> WithOnUpdate(Action action);
        ITimerBuilderWithRequirements<T> WithOnStop(Action action);
        ITimerBuilderWithRequirements<T> WithOnComplete(Action action);
        ITimerBuilderWithRequirements<T> WithOnRestart(Action action);
        ITimerBuilderWithRequirements<T> WithOnPause(Action action);
        ITimerBuilderWithRequirements<T> WithOnResume(Action action);
        ITimerBuilderWithRequirements<T> WithDuration(float duration);
    }
}