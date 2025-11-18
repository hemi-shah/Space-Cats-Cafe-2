using System;

public enum LogLevel { Debug, Info, Warning, Error}

public interface IGameLogger
{
    void Log(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogException(Exception exception);
    void LogFormat(string format, params object[] args);
}