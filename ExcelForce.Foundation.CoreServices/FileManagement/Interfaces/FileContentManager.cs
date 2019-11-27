using System;
using System.IO;

namespace ExcelForce.Foundation.CoreServices.FileManagement.Interfaces
{
    public class FileContentManager : IContentStreamManager
    {
        public string ReadContent(string resourceName)
        {
            using (StreamReader reader = new StreamReader(resourceName))
            {
                return reader.ReadToEnd();
            }
        }

        public bool WriteContent(string resourceName, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(resourceName))
                {
                    writer.WriteLine(content);
                }

                return true;
            }
            catch (Exception ex)
            {
                //TODO:(Ritwik):: Add logging here
                return false;
            }
        }
    }
}
