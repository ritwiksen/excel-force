using ExcelForce.Business.Constants;
using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Business.ServiceFactory;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.Persistence.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class CreateExtractionMapService : ICreateExtractionMapService
    {
        private readonly IPersistenceContainer _persistenceContainer;

        private readonly IExcelForceRepository<ExtractMap, string> _extractMapRepository;

        private readonly ILoggerManager _loggerManager;

        private readonly IExtractMapService _extractMapService;

        private readonly ISfQueryService _sfQueryService;

        public CreateExtractionMapService(IPersistenceContainer container,
            ILoggerManager loggerManager,
            IExtractMapService extractMapService,
            IExcelForceRepository<ExtractMap, string> extractMapRepository,
            ISfQueryService sfQueryService)
        {
            _persistenceContainer = container;

            _loggerManager = loggerManager;

            _extractMapService = extractMapService;

            _extractMapRepository = extractMapRepository;

            _sfQueryService = sfQueryService;
        }

        public ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName, bool isPrimary)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var query = new SfQuery
                {
                    Objects = new List<SfObject>()
                };

                query.Objects.Add(new SfObject
                {
                    IsPrimary = isPrimary,
                    Name = objectName
                });

                _persistenceContainer.SetPersistence(BusinessConstants.CurrentObject, objectName);

                _persistenceContainer.SetPersistence(BusinessConstants.CreateMapKey, query);
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while submitting object selection for Object", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Model = result,
                Messages = errorList
            };
        }

        public ServiceResponseModel<FieldSelectionModel> LoadActionsOnFieldList()
        {
            FieldSelectionModel result = null;

            List<string> errorList = null;

            try
            {
                var cufrrentObject = _persistenceContainer.GetPersistence<string>(BusinessConstants.CurrentObject);

                var listOfFields = new List<SfField>();

                result.SfFields = _extractMapService.GetFieldsByName(cufrrentObject, 10, 1)?.ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while fetching field details", errorList);
            }

            return new ServiceResponseModel<FieldSelectionModel>
            {
                Messages = errorList,
                Model = result
            };
        }

        public ServiceResponseModel<bool> CancelCreateExtractionMap()
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                _persistenceContainer.SetPersistence<SfQuery>(BusinessConstants.CreateMapKey, null);

                _persistenceContainer.SetPersistence<string>(BusinessConstants.CurrentObject, null);
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while cancelling the Create extraction map process", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = result
            };
        }

        public ServiceResponseModel<bool> SubmitFieldSelection(string objectName, IList<string> fields)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var queryObject = _persistenceContainer.GetPersistence<SfQuery>(BusinessConstants.CreateMapKey);

                var sfObject = queryObject?.Objects?.First(x => x.Name == objectName);

                sfObject.Fields = fields.Select(x => new SfField
                {
                    Name = x
                });

                _persistenceContainer.SetPersistence(BusinessConstants.CreateMapKey, sfObject);

                result = true;
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while saving field details", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = result
            };
        }

        public ServiceResponseModel<ParameterSelectionModel> LoadParameterSelectionScreen()
        {
            ParameterSelectionModel response = null;

            List<string> errorList = null;

            try
            {
                var contextObject = _persistenceContainer.GetPersistence<string>(BusinessConstants.CurrentObject);

                var queryObject = _persistenceContainer.GetPersistence<SfQuery>(BusinessConstants.CreateMapKey);

                var objects = _extractMapService.GetObjectNames();

                if (objects.Messages?.Count > 0)
                {
                    return ServiceResponseModelFactory.GetNullModelForReferenceType<ParameterSelectionModel>(
                        objects.Messages?.ToArray());
                }

                var objectDetails = queryObject?.Objects
                    ?.First(x => x.Name == contextObject);

                response = new ParameterSelectionModel
                {
                    SortExpression = objectDetails?.SortExpressions,
                    SearchExpression = objectDetails.FilterExpressions,
                    IsPrimary = objectDetails?.IsPrimary ?? false,
                    ChildList = objects?.Model.Where(x => !queryObject.Objects?.Select(y => y.Name)?.Contains(x) ?? false)?.ToList()
                };
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while fetching details for setting query parameters", errorList);
            }

            return new ServiceResponseModel<ParameterSelectionModel>
            {
                Model = response,
                Messages = errorList
            };
        }

        private void LogException(Exception ex, string errorMessage, IList<string> errorList)
        {
            errorList.Add("An error occurred while fetching field details");

            _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");
        }

        public ServiceResponseModel<bool> SubmitParameterSelectionScreen(
            string sortText, string filterText, string childName, string mapName)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var contextObject = _persistenceContainer.GetPersistence<string>(BusinessConstants.CurrentObject);

                var queryObject = _persistenceContainer.GetPersistence<SfQuery>(BusinessConstants.CreateMapKey);

                var objectDetails = queryObject?.Objects?.First(x => x.Name == contextObject);

                objectDetails.FilterExpressions = filterText;

                objectDetails.SortExpressions = sortText;

                queryObject.Objects.Add(new SfObject
                {
                    Name = childName
                });

                queryObject.Name = mapName;

                result = _persistenceContainer.SetPersistence(BusinessConstants.CreateMapKey, queryObject);

                if (!string.IsNullOrWhiteSpace(mapName))
                {
                    var query = _sfQueryService.GetStringifiedQuery(queryObject);

                    var addRecordResult = _extractMapRepository.AddRecord(new ExtractMap
                    {
                        Query = query,
                        Name = mapName
                    });
                }

                result = true;
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while saving object data", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = result
            };
        }
    }
}
