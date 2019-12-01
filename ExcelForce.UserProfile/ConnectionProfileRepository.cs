﻿using ExcelForce.Foundation.CoreServices.FileManagement;
using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.Serialization;
using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.ProfileManagement
{
    public class ConnectionProfileRepository : IExcelForceRepository<ConnectionProfile, string>
    {
        private readonly IContentSerializationManager _contentSerializationManager;

        private readonly IContentStreamManager _contentStreamManager;

        private const string _filePath = "C:\\Users\\risen\\Documents\\Data\\ExcelForce\\ExcelForce.txt";

        public ConnectionProfileRepository()
        {
            _contentSerializationManager = new JsonSerializer();

            _contentStreamManager = new FileContentManager();
        }

        public bool AddRecord(ConnectionProfile model)
        {
            var records = GetRecords()?.ToList()
                ?? new List<ConnectionProfile>();

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

        //TODO:(Ritwik):: Modify this to fetch only distinct records
        public IEnumerable<ConnectionProfile> GetRecords()
        {
            CreateFileIfAbsent();

            var fileContent =
                _contentStreamManager.ReadContent(_filePath);

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

            CreateFileIfAbsent();

            return _contentStreamManager.WriteContent(_filePath, serializedContent);
        }

        private bool CreateFileIfAbsent()
        {
            var fileExists = _contentStreamManager.ContentLocationExists(_filePath);

            if (!fileExists)
                _contentStreamManager.CreateContentLocation(_filePath);

            return true;
        }
    }
}