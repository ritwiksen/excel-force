using System;
using System.Collections.Generic;
using System.Linq;
using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.CoreServices.Persitence;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class ExtractMapService : IExtractMapService
    {
        private readonly ISfAttributeService _attributeService;

        private readonly IPersistenceManager<IEnumerable<string>> _objectPersistenceManager;

        private readonly IPersistenceManager<IEnumerable<string>> _fieldPersistenceManager;

        private readonly ISfObjectService _objectService;

        public ExtractMapService(ISfAttributeService attributeService,
            ISfObjectService objectService,
            IPersistenceManager<IEnumerable<string>> objectPersistenceManager,
            IPersistenceManager<IEnumerable<string>> fieldPersistenceManager)
        {
            _attributeService = attributeService;

            _objectPersistenceManager = objectPersistenceManager;

            _fieldPersistenceManager = fieldPersistenceManager;
        }

        public IEnumerable<string> GetObjectsByName(string name)
        {
            var objectNames = GetObjectNames();

            objectNames = objectNames
                ?.Where(x => !string.IsNullOrWhiteSpace(name)
                    ? string.Equals(x, name, StringComparison.InvariantCultureIgnoreCase)
                    : true);         

            return objectNames;
        }

        public IEnumerable<string> GetAttributesByName(string name, int pageSize, int pageNumber)
        {
            if (pageSize < 0)
                throw new InvalidOperationException("Page Size cannot be negative");

            if (pageSize <= 0)
                throw new InvalidOperationException("Page number cannot be negative or 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var attributes = _fieldPersistenceManager.Get();

            if (attributes == null)
            {
                var values = GetObjectsByName(name);

                attributes = values;
            }

            return attributes?.Skip((pageNumber * (pageSize - 1)))?.Take(pageSize);
        }

        public SfField GetAttributeData(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetObjectNames()
        {
            var objectNames = _objectService.GetObjectNames();

            objectNames = objectNames ?? _objectService.GetObjectNames();

            _objectPersistenceManager.Set(objectNames);

            return objectNames;
        }
    }
}
