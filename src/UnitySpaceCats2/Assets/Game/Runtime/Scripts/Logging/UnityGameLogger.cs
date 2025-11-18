/*
using Game399.Shared.Diagnostics;

namespace Game.Runtime
{
    public class UnityGameLogger : IGameLog
    {
        public void Info(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void Warn(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}
*/

using System;
using UnityEngine;

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