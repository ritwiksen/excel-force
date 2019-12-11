using System;
using System.Collections.Generic;
using System.Linq;
using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.Persitence;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class ExtractMapService : IExtractMapService
    {
        private readonly ISfAttributeService _attributeService;

        private readonly IPersistenceContainer _persistenceContainer;

        private readonly ISfObjectService _objectService;

        public ExtractMapService(ISfAttributeService attributeService,
            ISfObjectService objectService,
            IPersistenceContainer persistenceContainer)
        {
            _attributeService = attributeService;

            _persistenceContainer = persistenceContainer;

            _objectService = objectService;
        }

        public IEnumerable<string> GetObjectsByName(string name, string bearerToken)
        {
            var objectNames = GetObjectNames(bearerToken);

            objectNames = objectNames
                ?.Where(x => !string.IsNullOrWhiteSpace(name)
                    ? string.Equals(x, name, StringComparison.InvariantCultureIgnoreCase)
                    : true);

            return objectNames;
        }

        public IEnumerable<string> GetAttributesByName(string name, int pageSize, int pageNumber)
        {
            return null;
            //if (pageSize < 0)
            //    throw new InvalidOperationException("Page Size cannot be negative");

            //if (pageSize <= 0)
            //    throw new InvalidOperationException("Page number cannot be negative or 0");

            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentNullException(nameof(name));

            //var attributes = _fieldPersistenceManager.Get();

            //if (attributes == null)
            //{
            //    var values = GetObjectsByName(name);

            //    attributes = values;
            //}

            //return attributes?.Skip((pageNumber * (pageSize - 1)))?.Take(pageSize);
        }

        public SfField GetAttributeData(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetObjectNames(string bearerToken)
        {
            var objectNames = _objectService.GetObjectNames(bearerToken);

            objectNames = objectNames ?? _objectService.GetObjectNames(bearerToken);

            _persistenceContainer?.SfObjectsManager.Set(objectNames);

            return objectNames;
        }
    }
}
