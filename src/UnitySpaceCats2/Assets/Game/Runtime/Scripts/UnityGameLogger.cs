using Game399.Shared.Diagnostics;
using System;
using UnityEngine;

namespace Game.Runtime
{
    public class UnityGameLogger : IGameLogger
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        public void LogFormat(string format, params object[] args)
        {
            Debug.LogFormat(format, args);
        }
    }
}