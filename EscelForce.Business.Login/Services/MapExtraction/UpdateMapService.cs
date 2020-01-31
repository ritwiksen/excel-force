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
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
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

        public IEnumerable<SfField> GetFieldsByMapParentObjectName(string name)
        {
            try
            {
                return _extractMapRepository.GetRecords().FirstOrDefault(s => s.Name.Equals(name)).Query?.Parent?.Fields;

            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());
                
            }
            return null;
        }
        public ServiceResponseModel<IEnumerable<SfObject>> GetChildrenssByName(string name)
        {
            try
            {
                var persistentMapNames =
                      _persistenceContainer.Get<IEnumerable<SfObject>>(BusinessConstants.ChildList);

                //if (persistentMapNames != null)
                  //  return ServiceResponseModelFactory.GetModel(persistentMapNames);


                var childObjectNames = _extractMapRepository.GetRecords().FirstOrDefault(s => s.Name.Equals(name))?.Query?.Children?.Select(x=>new SfObject {ApiName= x.ApiName, Name=x.Label,FilterExpressions=x.SearchFilter,SortExpressions=x.SortFilter,Fields=x.Fields,RelationshipName=x.RelationshipName});
                
                _persistenceContainer?.Set(
                    BusinessConstants.ChildList, childObjectNames);

                return ServiceResponseModelFactory.GetModel(childObjectNames);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<IEnumerable<SfObject>>("An error occurred while fetching child object names");
            }
        }

        public ServiceResponseModel<IEnumerable<string>> GetMapNames()
        {
            try
            {
                var MapNames = _extractMapRepository.GetRecords().Select(c => c.Name);

                _persistenceContainer?.Set(
                    BusinessConstants.MapList, MapNames);

                return ServiceResponseModelFactory.GetModel(MapNames);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<IEnumerable<string>>("An error occurred while fetching Map names");
            }
        }

        public ServiceResponseModel<SfObject> GetObjectNameByMapName(string mapName)
        {
            try
            {
                

                var response = _extractMapRepository.GetRecords().FirstOrDefault(s => s.Name.Equals(mapName));
                var updatedObject = new SfObject
                {
                    ApiName = response.Query?.Parent?.ApiName,
                    Name= response.Query?.Parent?.Label,
                    IsPrimary=true,
                    FilterExpressions= response.Query?.Parent?.SearchFilter,
                    SortExpressions= response.Query?.Parent?.SortFilter
                };

                _persistenceContainer?.Set(
                    BusinessConstants.UpdatedObject, updatedObject);

                return ServiceResponseModelFactory.GetModel(updatedObject);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                    .GetNullModelForReferenceType<SfObject>("An error occurred while fetching parent object");
            }
        }

        public ServiceResponseModel<IEnumerable<SfChildRelationship>> GetChildRelationships(string objectName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(objectName))
                    throw new ArgumentNullException(nameof(objectName));

                var authResponse =
                  _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var values =
                    _attributeService.GetChildRelationships(objectName, authResponse?.AccessToken, authResponse?.InstanceUrl);

                return ServiceResponseModelFactory.GetModel(
                    values?.OrderBy(x => x.ObjectName).AsEnumerable());
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

                return ServiceResponseModelFactory
                     .GetNullModelForReferenceType<IEnumerable<SfChildRelationship>>("An error occurred while fetching object names");
            }
        }
        public ServiceResponseModel<IEnumerable<string>> GetObjectNames()
        {
            throw new NotImplementedException();
        }

        public ExtractMap GetExtractMapByName(string name)
        {
            try
            {
                return _extractMapRepository.GetRecords().FirstOrDefault(s => s.Name.Equals(name));

            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.GetExceptionLog());

            }
            return null;
        }
    }
}
