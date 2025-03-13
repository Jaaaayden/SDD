using UnityEngine;
using System;

namespace ChatSystem.Utils
{
    public class DebugLogger
    {
        private const string LogPrefix = "[ChatSystem] ";
        private readonly bool isDebugBuild;

        public DebugLogger()
        {
            isDebugBuild = Debug.isDebugBuild;
        }

        public void Log(string message)
        {
            if (!isDebugBuild) return;
            Debug.Log($"{LogPrefix}{message}");
        }

        public void LogWarning(string message)
        {
            if (!isDebugBuild) return;
            Debug.LogWarning($"{LogPrefix}{message}");
        }

        public void LogError(string message)
        {
            Debug.LogError($"{LogPrefix}{message}");
        }

        public void LogException(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
