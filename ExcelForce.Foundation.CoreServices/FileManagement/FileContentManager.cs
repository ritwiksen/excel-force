using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using System;
using System.IO;

namespace ExcelForce.Foundation.CoreServices.FileManagement
{
    public class FileContentManager : IContentStreamManager
    {
        public bool ContentLocationExists(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException(nameof(resourceName));

            return File.Exists(resourceName);
        }

        public bool CreateContentLocation(string resourceName)
        {
            try
            {
                Path.Combine(resourceName);
            }
            catch
            {
                throw new ArgumentException($"{nameof(resourceName)} is not a valid file name");
            }

            var directoryInfo = Directory.CreateDirectory(
                Path.GetDirectoryName(resourceName));

            return WriteContent(resourceName, string.Empty);
        }

        public string ReadContent(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException(nameof(resourceName));

            using (StreamReader reader = new StreamReader(resourceName))
            {
                return reader.ReadToEnd();
            }
        }

        public bool WriteContent(string resourceName, string content)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException(nameof(resourceName));

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
