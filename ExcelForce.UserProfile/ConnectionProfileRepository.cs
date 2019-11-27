using ExcelForce.Foundation.CoreServices.FileManagement;
using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.Serialization;
using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using ExcelForce.UserProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.UserProfile
{
    public class ConnectionProfileRepository : IExcelForceRepository<ConnectionProfile, string>
    {
        private readonly IContentSerializationManager _contentSerializationManager;

        private readonly IContentStreamManager _contentStreamManager;

        private string _fileName;

        public ConnectionProfileRepository()
        {
            _contentSerializationManager = new JsonSerializer();

            _contentStreamManager = new FileContentManager();
        }

        public bool AddRecord(ConnectionProfile model)
        {
            var records = GetRecords()?.ToList();

            records?.Add(model);

            return WriteContent(records);
        }

        public bool DeleteRecord(string key)
        {
            var records = GetRecords()?.ToList();

            records.RemoveAll(
                x => string.Equals(x.Name, key, StringComparison.InvariantCultureIgnoreCase));

            return WriteContent(records);
        }

        public IEnumerable<ConnectionProfile> GetRecords()
        {
            var fileContent =
                _contentStreamManager.ReadContent(_fileName);

            return
                _contentSerializationManager.Deserialize<List<ConnectionProfile>>(fileContent);
        }

        public bool UpdateRecord(string key, ConnectionProfile model)
        {
            var records = GetRecords();

            var matchRecord = records
                ?.Where(x => string.Equals(x.Name, key, StringComparison.InvariantCultureIgnoreCase))
                ?.FirstOrDefault();

            matchRecord = model;

            return WriteContent(records);
        }

        private bool WriteContent(IEnumerable<ConnectionProfile> records)
        {
            var serializedContent = _contentSerializationManager.Serialize(records);

            return _contentStreamManager.WriteContent(serializedContent, _fileName);
        }
    }
}
