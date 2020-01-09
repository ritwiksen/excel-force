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

        public IEnumerable<SfField> GetFieldsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var authResponse =
                _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

            var values =
                _attributeService.GetSfFields(name, authResponse?.AccessToken, authResponse?.InstanceUrl);

            return values
                ?.OrderBy(x => x.DisplayName());
        }

        public ServiceResponseModel<IEnumerable<SfObject>> GetObjects()
        {
            try
            {
                var persistentObjectNames =
                      _persistenceContainer.Get<IEnumerable<SfObject>>(BusinessConstants.ObjectList);

                if (persistentObjectNames != null)
                    return ServiceResponseModelFactory.GetModel(persistentObjectNames);

                var authResponse = _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var objectNames = _objectService.GetObjects(authResponse?.InstanceUrl, authResponse?.AccessToken);

                _persistenceContainer?.Set(
                    BusinessConstants.ObjectList, objectNames);

                return ServiceResponseModelFactory.GetModel(objectNames);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<IEnumerable<SfObject>>("An error occurred while fetching object names");
            }
        }
    }
}
