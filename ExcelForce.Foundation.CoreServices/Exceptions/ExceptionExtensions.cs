using System;

namespace ExcelForce.Foundation.CoreServices.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionLog(this Exception ex)
        {
            return $"{ex.Message} :: {ex.StackTrace}";
        }
    }
}
