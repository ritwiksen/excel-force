using System;
using System.Collections.Generic;
using System.Linq;
using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;

namespace ExcelForce.Foundation.EntityManagement.Repository
{
    public class ExtractMapRepository : IExcelForceRepository<ExtractMap, string>
    {
        private readonly IContentSerializationManager _contentSerializationManager;

        private readonly IContentStreamManager _contentStreamManager;

        private const string _filePath = "C:\\Users\\risen\\Documents\\Data\\ExcelForce\\ExtractMaps.txt";


        public ExtractMapRepository(IContentSerializationManager contentSerializationManager,
            IContentStreamManager contentStreamManager)
        {
            _contentSerializationManager = contentSerializationManager;

            _contentStreamManager = contentStreamManager;
        }

        public bool AddRecord(ExtractMap model)
        {
            var records = GetRecords()?.ToList()
              ?? new List<ExtractMap>();

            records?.Add(model);

            return WriteContent(records);
        }

        public bool DeleteRecord(string key)
        {
            var records = GetRecords()?.ToList();

            records?.RemoveAll(
                x => string.Equals(x.Name, key, StringComparison.InvariantCultureIgnoreCase));

            return WriteContent(records);
        }

        public IEnumerable<ExtractMap> GetRecords()
        {
            _contentStreamManager.CreateContentIfAbsent(_filePath);

            var fileContent =
                _contentStreamManager.ReadContent(_filePath);

            return
                _contentSerializationManager.Deserialize<List<ExtractMap>>(fileContent);
        }

        public bool UpdateRecord(string key, ExtractMap model)
        {
            var records = GetRecords();

            var matchRecord = records
                ?.Where(x => string.Equals(x.Name, key, StringComparison.InvariantCultureIgnoreCase))
                ?.FirstOrDefault();

            matchRecord = model;

            return WriteContent(records);
        }

        private bool WriteContent(IEnumerable<ExtractMap> records)
        {
            var serializedContent = _contentSerializationManager.Serialize(records);

            _contentStreamManager.CreateContentIfAbsent(_filePath);

            return _contentStreamManager.WriteContent(_filePath, serializedContent);
        }
    }
}
