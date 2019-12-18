using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using System;

namespace ExcelForce.Foundation.CoreServices.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private readonly IContentStreamManager _contentStreamManager;

        private const string _filePath = "C:\\DD\\ExcelForce\\Log.txt";

        public LoggerManager(IContentStreamManager contentStreamManager)
        {
            _contentStreamManager = contentStreamManager;
        }

        public void LogInfo(string message)
        {
            string modifiedMessage = CreateModifiedMessage(message);

            _contentStreamManager.CreateContentIfAbsent(_filePath);

            _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }
        public void LogError(string message)
        {
            string modifiedMessage = CreateModifiedMessage(message);

            _contentStreamManager.CreateContentIfAbsent(_filePath);

            _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }
        public void LogWarn(string message)
        {
            string modifiedMessage = CreateModifiedMessage(message);

            _contentStreamManager.CreateContentIfAbsent(_filePath);

            _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }

        private string CreateModifiedMessage(string message) => $"WARN : {DateTime.Now} : {message}";
    }
}
