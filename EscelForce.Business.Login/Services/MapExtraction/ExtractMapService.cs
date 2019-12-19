using System;
using System.Collections.Generic;
using System.Linq;
using ExcelForce.Business.Constants;
using ExcelForce.Business.Interfaces;
using ExcelForce.Business.ServiceFactory;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Exceptions;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.Persistence.Persitence;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class ExtractMapService : IExtractMapService
    {
        private readonly ISfAttributeService _attributeService;

        private readonly IPersistenceContainer _persistenceContainer;

        private readonly ISfObjectService _objectService;

        private readonly ISfQueryService _sfQueryService;

        private readonly ILoggerManager _loggerManager;

        public ExtractMapService(ISfAttributeService attributeService,
            ISfObjectService objectService,
            IPersistenceContainer persistenceContainer,
            ISfQueryService queryService,
            ILoggerManager loggerManager)
        {
            _attributeService = attributeService;

            _persistenceContainer = persistenceContainer;

            _objectService = objectService;

            _sfQueryService = queryService;

            _loggerManager = loggerManager;
        }

        public IEnumerable<string> GetObjectsByName(string name, string bearerToken)
        {
            //var objectNames = GetObjectNames(bearerToken);

            //objectNames = objectNames
            //    ?.Where(x => !string.IsNullOrWhiteSpace(name)
            //        ? string.Equals(x, name, StringComparison.InvariantCultureIgnoreCase)
            //        : true);

            //return objectNames;

            return null;
        }

        public IEnumerable<SfField> GetFieldsByName(string name, int pageSize, int pageNumber)
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

        public ServiceResponseModel<IEnumerable<string>> GetObjectNames()
        {
            try
            {
                var persistentObjectNames =
                      _persistenceContainer.GetPersistence<IEnumerable<string>>(BusinessConstants.ObjectList);

                if (persistentObjectNames != null)
                    return ServiceResponseModelFactory.GetModel(persistentObjectNames);

                var authResponse = _persistenceContainer.GetPersistence<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var objectNames = _objectService.GetObjectNames(authResponse?.InstanceUrl,authResponse?.AccessToken);

                _persistenceContainer?.SetPersistence(
                    BusinessConstants.ObjectList, objectNames);

                return ServiceResponseModelFactory.GetModel(objectNames);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<IEnumerable<string>>("An error occurred while fetching object names");
            }
        }
    }
}
