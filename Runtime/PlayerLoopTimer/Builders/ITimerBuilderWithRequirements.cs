using System;
using System.Collections.Generic;

namespace PlayerLoopTimer {
    public interface ITimerBuilderWithRequirements<out T> where T : Timer {
        T Finish();

        T FinishAndStartTimer();

        ITimerBuilderWithRequirements<T> WithOnBegin(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnUpdate(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnStop(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnComplete(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnRestart(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnPause(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithOnResume(params Action[] actions);
        ITimerBuilderWithRequirements<T> WithDuration(float duration);
    }
}