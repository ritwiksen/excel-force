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
using ExcelForce.Foundation.EntityManagement.Repository;
using ExcelForce.Foundation.Persistence.Persitence;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class UpdateMapService : IUpdateMapService
    {
        private readonly ISfAttributeService _attributeService;

        private readonly IPersistenceContainer _persistenceContainer;

        private readonly ISfObjectService _objectService;

        private readonly ISfQueryService _sfQueryService;

        private readonly ExtractMapRepository _extractMapRepository;

        private readonly ILoggerManager _loggerManager;

        public UpdateMapService(ISfAttributeService attributeService,
            ISfObjectService objectService,
            IPersistenceContainer persistenceContainer,
            ISfQueryService queryService,
            ILoggerManager loggerManager,
            ExtractMapRepository extractMapRepository)
        {
            _attributeService = attributeService;

            _persistenceContainer = persistenceContainer;

            _objectService = objectService;

            _sfQueryService = queryService;

            _loggerManager = loggerManager;

            _extractMapRepository = extractMapRepository;
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

        public ServiceResponseModel<IEnumerable<string>> GetMapNames()
        {
            try
            {
                var persistentMapNames =
                      _persistenceContainer.Get<IEnumerable<string>>(BusinessConstants.MapList);

                if (persistentMapNames != null)
                    return ServiceResponseModelFactory.GetModel(persistentMapNames);


                var MapNames = _extractMapRepository.GetRecords().Select(c => c.Name);

                _persistenceContainer?.Set(
                    BusinessConstants.MapList, MapNames);

                return ServiceResponseModelFactory.GetModel(MapNames);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<IEnumerable<string>>("An error occurred while fetching object names");
            }
        }

        public ServiceResponseModel<IEnumerable<string>> GetObjectNames()
        {
            throw new NotImplementedException();
        }
    }
}
