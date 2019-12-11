using ExcelForce.Foundation.CoreServices.FileManagement;
using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using System;

namespace ExcelForce.Foundation.CoreServices.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private readonly IContentStreamManager _contentStreamManager;
        private const string _filePath = "C:\\DD\\ExcelForce\\Log.txt";

        public LoggerManager()
        {
            _contentStreamManager = new FileContentManager();
        }

        public void LogInfo(string message)
        {
            string modifiedMessage = "INFO : " + DateTime.Now + " : " + message;
            _contentStreamManager.CreateFileIfAbsent(_filePath);

             _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }
        public void LogError(string message)
        {
            string modifiedMessage = "ERROR : " + DateTime.Now + " : " + message;
            _contentStreamManager.CreateFileIfAbsent(_filePath);

             _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }
        public void LogWarn(string message)
        {
            string modifiedMessage = "WARN : " + DateTime.Now + " : " + message;
            _contentStreamManager.CreateFileIfAbsent(_filePath);

             _contentStreamManager.WriteContent(_filePath, modifiedMessage);
        }

    }
}
