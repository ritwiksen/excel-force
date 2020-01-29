using ExcelForce.Business.Constants;
using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Business.ServiceFactory;
using ExcelForce.Foundation.Authentication.Models;
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

        private readonly IReadableExtractMapService _readableExtractMapService;

        private readonly ISfObjectService _sfObjectService;

        public CreateExtractionMapService(IPersistenceContainer container,
            ILoggerManager loggerManager,
            IExtractMapService extractMapService,
            IExcelForceRepository<ExtractMap, string> extractMapRepository,
            IReadableExtractMapService readableExtractMapService,
            ISfObjectService sfObjectService)
        {
            _persistenceContainer = container;

            _loggerManager = loggerManager;

            _extractMapService = extractMapService;

            _extractMapRepository = extractMapRepository;

            _readableExtractMapService = readableExtractMapService;

            _sfObjectService = sfObjectService;
        }

        public ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var query = queryObject ?? new SfQuery
                {
                    Objects = new List<SfObject>()
                };

                string selectedObjectName = SfObject.GetApiNameFromDisplayName(objectName);

                if (!query.Objects.Any(x => x.Name .Equals(selectedObjectName)))
                {
                    var isPrimary = (query.Objects?.Count(x => x.IsPrimary) ?? 0) == 0;

                    query.Objects.Add(new SfObject
                    {
                        IsPrimary = isPrimary,
                        ApiName = selectedObjectName,
                        Name= SfObject.GetObjectNameFromDisplayName(objectName)
                    });
                }

                _persistenceContainer.Set(BusinessConstants.CurrentObject, selectedObjectName);

                _persistenceContainer.Set(BusinessConstants.CreateMapKey, query);

                result = true;
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
            try
            {
                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var sfQuery = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var response = ServiceResponseModelFactory.GetReferenceTypeModel<FieldSelectionModel>();

                response.Model = new FieldSelectionModel
                {
                    SfFields = _extractMapService.GetFieldsByName(currentObject)?.ToList(),
                    AvailableFields = sfQuery.Objects?.FirstOrDefault(x => x.Name == currentObject).Fields?.ToList() ?? null,
                    ObjectName = currentObject
                };

                return response;
            }
            catch (Exception ex)
            {
                List<string> errorList = new List<string>();

                LogException(ex, "An error occurred while fetching field details", errorList);

                return ServiceResponseModelFactory.GetNullModelForReferenceType<FieldSelectionModel>(errorList?.ToArray());
            }
        }

        public ServiceResponseModel<bool> CancelCreateExtractionMap()
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                _persistenceContainer.Set<SfQuery>(BusinessConstants.CreateMapKey, null);

                _persistenceContainer.Set<string>(BusinessConstants.CurrentObject, null);
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

        public ServiceResponseModel<bool> SubmitFieldSelection(string objectName, IList<SfField> fields)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var relationShipFieldName = _persistenceContainer.Get<string>("SelectedChildRelationshipField");

                var sfObject = queryObject?.Objects?.First(x => x.Name == objectName);

                sfObject.Fields = fields;

                sfObject.RelationshipName = relationShipFieldName;

                _persistenceContainer.Set(BusinessConstants.CreateMapKey, queryObject);

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
                var contextObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var objects = _extractMapService.GetChildRelationships(contextObject);

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
                    ChildList = objects?.Model.Where(x => !queryObject.Objects?.Select(y => y.Name)?.Contains(x.ObjectName) ?? false)?.Select(s=>s)?.ToList()
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
            SearchSortExtractionModel model)
        {
            try
            {
                var contextObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var objectDetails = queryObject?.Objects?.First(x => x.Name == contextObject);

                objectDetails.FilterExpressions = model?.SearchExpression;

                objectDetails.SortExpressions = model?.SortExpression;

                queryObject.Name = model?.MapName;

                var query = _readableExtractMapService.GetContentFromQuery(queryObject);

                var addRecordResult = _extractMapRepository.AddRecord(new ExtractMap
                {
                    Query = query,
                    Name = model.MapName
                });

                _persistenceContainer.Set<string>(BusinessConstants.CurrentObject, null);

                _persistenceContainer.Set<SfQuery>(BusinessConstants.CreateMapKey, null);

                return ServiceResponseModelFactory.GetModel(true, null);
            }
            catch (Exception ex)
            {
                List<string> errorList = null;

                LogException(ex, "An error occurred while saving object data", errorList);

                return ServiceResponseModelFactory.GetNullModelForValueType<bool>(errorList?.ToArray());
            }
        }

        public ServiceResponseModel<ObjectSelectionFormModel> LoadObjectSelectionScreen()
        {
            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var response = _extractMapService.GetObjects();

                var objects = response.IsValid()
                    ? response.Model?.ToList()
                    : null;

                var existingObjectNames = queryObject?.Objects.Select(x => x.Name);

                return ServiceResponseModelFactory.GetModel(
                    new ObjectSelectionFormModel
                    {
                        ObjectNames = objects.Where(x => !(existingObjectNames?.Contains(x.Name) ?? false)).Select(s=>s.DisplayName()),
                        selectedObjectName = currentObject != null
                            ? queryObject?.Objects?.Last()?.DisplayName() ?? string.Empty
                            : string.Empty
                    });
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();

                LogException(ex, "An error occurred while saving object data", errorList);

                return ServiceResponseModelFactory.GetNullModelForReferenceType<ObjectSelectionFormModel>(
                    errorList?.ToArray());
            }
        }

        public ServiceResponseModel<SearchSortExtractionModel> LoadSearchSortScreen()
        {
            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var authResponse = _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var currentObjectData = queryObject?.Objects?.FirstOrDefault(x => x.Name == currentObject);

                var children = _sfObjectService.GetChildrenForObject(
                    authResponse?.InstanceUrl,
                    authResponse?.AccessToken,
                    queryObject?.GetParentObject()?.Name);

                var excludedNames = queryObject?.Objects?.Select(x => x.Name)?.ToList();

                children = queryObject.GetChildren() != null
                    ? children?.Where(x => !excludedNames.Contains(x.Name))
                              ?.OrderBy(x => x.Name)
                    : children;

                var objects = _extractMapService.GetChildRelationships(queryObject?.GetParentObject()?.Name);
                if (objects.Messages?.Count > 0)
                {
                    return ServiceResponseModelFactory.GetNullModelForReferenceType<SearchSortExtractionModel>(
                        objects.Messages?.ToArray());
                }

                var objectDetails = queryObject?.Objects
                    ?.First(x => x.Name == currentObject);

                return ServiceResponseModelFactory.GetModel(
                  new SearchSortExtractionModel
                  {
                      SearchExpression = currentObjectData.FilterExpressions,
                      SortExpression = currentObjectData.SortExpressions,
                      ShowAddChildSection = queryObject.GetChildren()?.Count < 2,
                      Children = children?.ToList(),
                      ShowMapNameSection = ShowMapSectionOnStart(),
                      ChildRelationships = objects?.Model.Where(x => !queryObject.Objects?.Select(y => y.Name)?.Contains(x.ObjectName) ?? false)?.Select(s => s)?.ToList()
                  });
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();

                LogException(ex, "An error occurred while getting children details", errorList);

                return ServiceResponseModelFactory.GetNullModelForReferenceType<SearchSortExtractionModel>(
                    errorList?.ToArray());
            }
        }

        public ServiceResponseModel<FieldSelectionModel> SubmitForNewChild(SearchSortExtractionModel model)
        {
            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var sfObject = queryObject?.Objects.FirstOrDefault(x => x.Name == currentObject);

                sfObject.FilterExpressions = model.SearchExpression;

                sfObject.SortExpressions = model.SortExpression;

                _persistenceContainer.Set(BusinessConstants.CreateMapKey, queryObject);

                _persistenceContainer.Set(BusinessConstants.CurrentObject, model.SelectedChild);

                _persistenceContainer.Set("SelectedChildRelationshipField", model?.SelectedChildRelationshipName);

                var response = SubmitOnObjectSelection(model.SelectedChild);

                var loadActionResponse = LoadActionsOnFieldList();

                return ServiceResponseModelFactory.GetModel(
                    loadActionResponse?.Model,
                    loadActionResponse.Messages?.ToArray());
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();

                LogException(ex, "An error occurred while saving object data", errorList);

                return ServiceResponseModelFactory.GetNullModelForReferenceType<FieldSelectionModel>(
                    errorList?.ToArray());
            }
        }

        public ServiceResponseModel<bool> AreChildrenAvailable()
        {
            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                var evaluation = queryObject?.GetParentObject() != null
                    && (queryObject?.GetChildren()?.Count ?? 0) > 0;

                return ServiceResponseModelFactory.GetModel<bool>(evaluation, null);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();

                LogException(ex, "An error occurred while checking if primary object was added", errorList);

                return ServiceResponseModelFactory.GetNullModelForValueType<bool>(
                    errorList?.ToArray());
            }
        }

        private bool ShowMapSectionOnStart()
        {
            var children = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey)?.GetChildren();

            return (children?.Count ?? 0) == 2;
        }

        public ServiceResponseModel<bool> SubmitPreviousFieldSelection()
        {
            {
                try
                {
                    var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.CreateMapKey);

                    var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                    queryObject.Objects = queryObject?.Objects.Where(x => x.Name != currentObject)?.ToList();

                    currentObject = queryObject.Objects?.LastOrDefault()?.Name;

                    _persistenceContainer.Set(BusinessConstants.CurrentObject, currentObject);

                    _persistenceContainer.Set(BusinessConstants.CreateMapKey, queryObject);

                    return ServiceResponseModelFactory.GetModel(true);
                }
                catch (Exception ex)
                {
                    var errorList = new List<string>();

                    LogException(ex, "An error occurred while transitioning back", errorList);

                    return ServiceResponseModelFactory.GetNullModelForValueType<bool>(
                        errorList?.ToArray());
                }
            }
        }
    }
}
