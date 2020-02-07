﻿using ExcelForce.Business.Constants;
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
    public class UpdateExtractionMapService : IUpdateExtractionMapService
    {
        private readonly IPersistenceContainer _persistenceContainer;

        private readonly IExcelForceRepository<ExtractMap, string> _updateMapRepository;

        private readonly ILoggerManager _loggerManager;

        private readonly IUpdateMapService _updateMapService;

        private readonly ISfQueryService _sfQueryService;

        private readonly ISfObjectService _sfObjectService;

        private readonly IReadableExtractMapService _readableExtractMapService;
        public UpdateExtractionMapService(IPersistenceContainer container,
            ILoggerManager loggerManager,
            IUpdateMapService updateMapService,
            IExcelForceRepository<ExtractMap, string> extractMapRepository,
            ISfQueryService sfQueryService,
            ISfObjectService sfObjectService,
            IReadableExtractMapService readableExtractMapService)
        {
            _persistenceContainer = container;

            _loggerManager = loggerManager;

            _updateMapService = updateMapService;

            _updateMapRepository = extractMapRepository;

            _sfQueryService = sfQueryService;

            _sfObjectService = sfObjectService;

            _readableExtractMapService = readableExtractMapService;
        }

        public ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var query = queryObject ?? new SfQuery
                {
                    Objects = new List<SfObject>()
                };

                string selectedObjectName = SfObject.GetApiNameFromDisplayName(objectName);

                if (!query.Objects.Any(x => x.Name.Equals(selectedObjectName)))
                {
                    var isPrimary = (query.Objects?.Count(x => x.IsPrimary) ?? 0) == 0;

                    query.Objects.Add(new SfObject
                    {
                        IsPrimary = isPrimary,
                        ApiName = selectedObjectName,
                        Name = SfObject.GetObjectNameFromDisplayName(objectName),
                        FilterExpressions=_updateMapService.GetObjectNameByMapName(query.Name)?.Model.FilterExpressions,
                        SortExpressions = _updateMapService.GetObjectNameByMapName(query.Name)?.Model.SortExpressions,
                        RelationshipName= _persistenceContainer.Get<string>(BusinessConstants.SelectedChildRelationshipField)
                    });
                }

                _persistenceContainer.Set(BusinessConstants.CurrentObject, selectedObjectName);

                _persistenceContainer.Set(BusinessConstants.UpdateMapKey, query);

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

        public ServiceResponseModel<SfQuery> SubmitOnMapSelection(string mapName)
        {
           try
            {
                var updateMapObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var updateObject = updateMapObject ?? new SfQuery
                {
                    Name = string.Empty,
                    Objects = new List<SfObject>(),
                    ParentObject=new SfObject()
                };

                if (!string.IsNullOrEmpty(mapName) && !string.Equals(updateObject.Name.Trim(),mapName.Trim(),StringComparison.InvariantCultureIgnoreCase))
                {
                    var parentObjectResponse = _updateMapService.GetObjectNameByMapName(mapName);
                    var childObjectResponse = _updateMapService.GetChildrenssByName(mapName);

                    updateObject.Name = mapName;
                    updateObject.ParentObject = parentObjectResponse.IsValid()? parentObjectResponse.Model:null;                                      
                    updateObject.Objects = childObjectResponse.IsValid()
                    ? childObjectResponse.Model?.ToList()
                    : null;
                }

                var finResponse = ServiceResponseModelFactory.GetReferenceTypeModel<SfQuery>();

                finResponse.Model = updateObject;

                if (string.IsNullOrEmpty(mapName))
                {
                    _persistenceContainer.Set(BusinessConstants.CurrentMapName, updateObject?.Name);
                }
                else
                {
                    _persistenceContainer.Set(BusinessConstants.CurrentMapName, mapName);
                }
                

                _persistenceContainer.Set(BusinessConstants.UpdateMapKey, updateObject);

                

                return finResponse;
            }catch (Exception ex)
            {
                List<string> errorList = new List<string>();

                LogException(ex, "An error occurred while fetching field details", errorList);

                return ServiceResponseModelFactory.GetNullModelForReferenceType<SfQuery>(errorList?.ToArray());
            }
        }
        
        public ServiceResponseModel<FieldSelectionModel> LoadActionsOnFieldList()
        {
            try
            {
                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var sfQuery = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var response = ServiceResponseModelFactory.GetReferenceTypeModel<FieldSelectionModel>();

                var isPrimary= sfQuery?.Objects.FirstOrDefault(x => x.Name == currentObject)?.IsPrimary;

               
                var availableFields = isPrimary != null && (bool)isPrimary ? _updateMapService.GetFieldsByMapParentObjectName(sfQuery?.Name)?.ToList() : _updateMapService.GetChildrenssByName(sfQuery.Name)?.Model.ToList().FirstOrDefault(x => string.Equals(x.ApiName,currentObject,StringComparison.InvariantCultureIgnoreCase))?.Fields.ToList();
                response.Model = new FieldSelectionModel
                {
                    SfFields = _updateMapService.GetFieldsByName(currentObject)?.ToList(),
                    AvailableFields = availableFields,
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

        public ServiceResponseModel<bool> CancelUpdateExtractionMap()
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                _persistenceContainer.Set<SfQuery>(BusinessConstants.UpdateMapKey, null);

                _persistenceContainer.Set<string>(BusinessConstants.CurrentMapName, null);
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
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);
                var relationShipFieldName = _persistenceContainer.Get<string>(BusinessConstants.SelectedChildRelationshipField);
                var sfObject = queryObject?.Objects?.First(x => x.Name == objectName);

                sfObject.Fields = fields;

                _persistenceContainer.Set(BusinessConstants.UpdateMapKey, queryObject);

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

                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var objects = _updateMapService.GetChildRelationships(contextObject);

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
                    ChildList = objects?.Model.Where(x => !queryObject.Objects?.Select(y => y.Name)?.Contains(x.ObjectName) ?? false)?.Select(s => s)?.ToList()
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

                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var objectDetails = queryObject?.Objects?.First(x => x.Name == contextObject);

                objectDetails.FilterExpressions = model?.SearchExpression;

                objectDetails.SortExpressions = model?.SortExpression;
               

                queryObject.Name = model?.MapName;

                if (string.IsNullOrEmpty(model.SelectedChild))
                {
                    objectDetails.RelationshipName = _persistenceContainer.Get<string>(BusinessConstants.SelectedChildRelationshipField);
                    var query = _readableExtractMapService.GetContentFromQuery(queryObject);

                    var addRecordResult = _updateMapRepository.UpdateRecord(model.MapName, new ExtractMap
                    {
                        Query = query,
                        Name = model.MapName
                    });
                }
                else
                {
                    objectDetails.RelationshipName = model?.SelectedChildRelationshipName;
                    var extractMap = _updateMapService.GetExtractMapByName(model.MapName);

                    var childrenList = extractMap?.Query?.Children;
                    var matchRecord = childrenList?.Where(x => string.Equals(x.ApiName, model.SelectedChild, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    var recordIndex = childrenList.IndexOf(matchRecord);
                    if (recordIndex < 0)
                        return null;

                    var updateChild = new ReadableObject
                    {
                        ApiName = objectDetails.ApiName,

                        Label = objectDetails.Name,

                        SearchFilter = objectDetails.FilterExpressions,

                        SortFilter = objectDetails.SortExpressions,

                        Fields = objectDetails.Fields?.ToList(),

                        RelationshipName = objectDetails.RelationshipName
                    };

                    childrenList[recordIndex] = updateChild;

                    extractMap.Query.Children = childrenList;

                    var addRecordResult = _updateMapRepository.UpdateRecord(model.MapName, new ExtractMap
                    {
                        Query = extractMap.Query,
                        Name = model.MapName
                    });
                }
               

               

                return ServiceResponseModelFactory.GetModel(true, null);
            }
            catch (Exception ex)
            {
                List<string> errorList = null;

                LogException(ex, "An error occurred while saving object data", errorList);

                return ServiceResponseModelFactory.GetNullModelForValueType<bool>(errorList?.ToArray());
            }
        }

        public ServiceResponseModel<ObjectSelectionFormModel> LoadMapSelectionScreen()
        {
            try
            {
                var updateObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentMapName);

                var response = _updateMapService.GetMapNames();

                var objects = response.IsValid()
                    ? response.Model?.ToList()
                    : null;

                var existingObjectName = updateObject != null ? updateObject.Name:string.Empty;

                return ServiceResponseModelFactory.GetModel(
                    new ObjectSelectionFormModel
                    {
                        ObjectNames = objects.Where(x => !(existingObjectName?.Contains(x) ?? false)),
                        selectedObjectName = currentObject != null
                            ? updateObject?.Name ?? string.Empty
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
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

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

                var objects = _updateMapService.GetChildRelationships(queryObject?.GetParentObject()?.Name);
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
                      ChildRelationships = objects?.Model.Where(x => !queryObject.Objects?.Select(y => y.Name)?.Contains(x.ObjectName) ?? false)?.Select(s => s)?.ToList(),
                      MapName = queryObject.Name
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

        public ServiceResponseModel<SearchSortExtractionModel> LoadChildSearchSortScreen(String child)
        {
            try
            {
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                var authResponse = _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var currentObjectData = queryObject?.Objects?.FirstOrDefault(x => x.Name == child);

                var objects = _updateMapService.GetChildRelationships(queryObject?.ParentObject?.ApiName);
                if (objects.Messages?.Count > 0)
                {
                    return ServiceResponseModelFactory.GetNullModelForReferenceType<SearchSortExtractionModel>(
                        objects.Messages?.ToArray());
                }


                return ServiceResponseModelFactory.GetModel(
                  new SearchSortExtractionModel
                  {
                      SearchExpression = currentObjectData.FilterExpressions,
                      SortExpression = currentObjectData.SortExpressions,
                      SelectedChild=SfObject.GetApiNameFromDisplayName(child),
                      SelectedChildRelationshipName=currentObjectData.RelationshipName,
                      ChildRelationships = objects?.Model.Select(s => s)?.ToList(),
                      MapName =queryObject.Name
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
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);
               

                _persistenceContainer.Set(BusinessConstants.UpdateMapKey, queryObject);

                _persistenceContainer.Set(BusinessConstants.CurrentObject, model.SelectedChild);

                _persistenceContainer.Set(BusinessConstants.SelectedChildRelationshipField, model?.SelectedChildRelationshipName);

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
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

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
            var children = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey)?.GetChildren();

            return (children?.Count ?? 0) == 2;
        }

        public ServiceResponseModel<bool> SubmitPreviousFieldSelection()
        {
            {
                try
                {
                    var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);

                    var currentObject = _persistenceContainer.Get<string>(BusinessConstants.CurrentObject);

                    queryObject.Objects = queryObject?.Objects.Where(x => x.Name != currentObject)?.ToList();

                    currentObject = queryObject.Objects?.LastOrDefault()?.Name;

                    _persistenceContainer.Set(BusinessConstants.CurrentObject, currentObject);

                    _persistenceContainer.Set(BusinessConstants.UpdateMapKey, queryObject);

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

        public ServiceResponseModel<bool> DeleteSelectedChild(
           string childObjectName)
        {

            try
            {
               
                var queryObject = _persistenceContainer.Get<SfQuery>(BusinessConstants.UpdateMapKey);                

                var addRecordResult = _updateMapRepository.DeleteRecordByMapNameAndKey(queryObject.Name,childObjectName);

                queryObject.Objects = _updateMapService.GetChildrenssByName(queryObject.Name).Model?.ToList();
                _persistenceContainer.Set<SfQuery>(BusinessConstants.UpdateMapKey, queryObject);

                return ServiceResponseModelFactory.GetModel(true, null);
            }
            catch (Exception ex)
            {
                List<string> errorList = null;

                LogException(ex, "An error occurred while saving object data", errorList);

                return ServiceResponseModelFactory.GetNullModelForValueType<bool>(errorList?.ToArray());
            }
        }

        public ServiceResponseModel<bool> clear()
        {
            _persistenceContainer.Set<string>(BusinessConstants.CurrentObject, null);

            _persistenceContainer.Set<SfQuery>(BusinessConstants.UpdateMapKey, null);

            return ServiceResponseModelFactory.GetModel(true, null);
        }
    }
}
