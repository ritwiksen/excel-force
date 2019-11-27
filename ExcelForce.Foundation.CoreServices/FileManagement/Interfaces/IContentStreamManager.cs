namespace ExcelForce.Foundation.CoreServices.FileManagement.Interfaces
{
    public interface IContentStreamManager
    {
        /// <summary>
        /// This method reads content from an external resource
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        string ReadContent(string resourceName);

        /// <summary>
        /// This method Writes String content to an external resource
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        bool WriteContent(string resourceName, string content);
    }
}
