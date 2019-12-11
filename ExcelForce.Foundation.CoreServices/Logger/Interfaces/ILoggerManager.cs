using System;

namespace ExcelForce.Foundation.CoreServices.Logger.Interfaces
{
    public interface ILoggerManager
    {
        void LogError(string message);

        void LogInfo(string message);

        void LogWarn(string message);
    }
}
