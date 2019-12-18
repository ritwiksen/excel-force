using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
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

        private readonly ILoggerManager _loggerManager;

        private readonly IExtractMapService _extractMapService;

        private const string _createMapKey = "createMap";

        private const string _currentObject = "objectName";

        public CreateExtractionMapService(IPersistenceContainer container,
            ILoggerManager loggerManager,
            IExtractMapService extractMapService)
        {
            _persistenceContainer = container;

            _loggerManager = loggerManager;

            _extractMapService = extractMapService;
        }

        public ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName, bool isPrimary)
        {
            bool result = false;

            List<string> errorList = null;

            try
            {
                SfQuery query = null;

                query = new SfQuery();

                query.Objects.Add(new SfObject
                {
                    IsPrimary = isPrimary,
                    Name = objectName
                });

                _persistenceContainer.SetPersistence(_currentObject, objectName);

                _persistenceContainer.SetPersistence(_createMapKey, query);
            }
            catch (Exception ex)
            {
                errorList.Add("An error occurred while submitting object selection for Object");

                _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");
            }

            return new ServiceResponseModel<bool>
            {
                Model = result,
                Messages = errorList
            };
        }

        public ServiceResponseModel<FieldSelectionModel> ActionsOnFieldListLoad()
        {
            FieldSelectionModel result = null;

            List<string> errorList = null;

            try
            {
                var cufrrentObject = _persistenceContainer.GetPersistence<string>(_currentObject);

                var listOfFields = new List<SfField>();

                result.SfFields = _extractMapService.GetFieldsByName(cufrrentObject, 10, 1)?.ToList();
            }
            catch (Exception ex)
            {
                errorList.Add("An error occurred while fetching field details");

                _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");
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
                _persistenceContainer.SetPersistence<SfQuery>(_createMapKey, null);

                _persistenceContainer.SetPersistence<string>(_currentObject, null);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");

                errorList.Add("An error occurred while cancelling the Create extraction map process");
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = result
            };
        }
    }
}
