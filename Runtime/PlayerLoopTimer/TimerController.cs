using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PlayerLoopTimer {
    public static class TimerController {
        private static readonly List<Timer> Timers = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize() {
            AddToPlayerLoop();

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        private static void AddToPlayerLoop() {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            var newSystem = new PlayerLoopSystem {
                type = typeof(TimerController),
                updateDelegate = UpdateTimers
            };

            var newSubSystemList = new List<PlayerLoopSystem>(playerLoop.subSystemList) { newSystem };
            playerLoop.subSystemList = newSubSystemList.ToArray();

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        public static void AddTimer(Timer timer) {
            if (!Timers.Contains(timer)) {
                Timers.Add(timer);
            }
            else {
                Debug.LogWarning("Timer is already added in the TimerController");
            }
        }

        public static void RemoveTimer(Timer timer) {
            if (Timers.Contains(timer)) {
                Timers.Remove(timer);
            }
            else {
                Debug.LogWarning("Timer not found and could not be removed");
            }
        }

        private static void UpdateTimers() {
            foreach (var timer in Timers.ToArray()) {
                timer.UpdateTimer();
            }
        }

#if UNITY_EDITOR
        private static void OnPlayModeStateChanged(PlayModeStateChange state) {
            if (state == PlayModeStateChange.ExitingPlayMode) {
                Timers.Clear();
                ResetPlayerLoop();
            }
        }

        private static void ResetPlayerLoop() => PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
#endif
    }
}
