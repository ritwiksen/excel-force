using ExcelForce.Business.Constants;
using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Models.ExtractionMap.ExtractData;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.Persistence.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Business.Services.MapExtraction
{
    public class ExtractDataService : IExtractDataService
    {
        private readonly IExcelForceRepository<ExtractMap, string> _excelForceRepository;

        private readonly IPersistenceContainer _persistenceContainer;

        private readonly ILoggerManager _loggerManager;

        private readonly ISfQueryService _sfQueryService;

        public ExtractDataService(IExcelForceRepository<ExtractMap, string> excelForceRepository,
            IPersistenceContainer persistenceContainer,
            ILoggerManager loggerManager, ISfQueryService sfQueryService)
        {
            _excelForceRepository = excelForceRepository;

            _loggerManager = loggerManager;

            _persistenceContainer = persistenceContainer;

            _sfQueryService = sfQueryService;
        }

        public ServiceResponseModel<ReadableMapExtract> GetEtxractMapViewerFormModel()
        {
            List<string> errorList = null;

            ReadableMapExtract readableMapExtract = null;

            try
            {
                var savedMapName = _persistenceContainer.Get<string>(BusinessConstants.ExtractDataKey);

                var extractMaps = _excelForceRepository.GetRecords();

                var matchingMap = extractMaps?.FirstOrDefault(x => string.Equals(savedMapName, x.Name));

                if (matchingMap == null)
                {
                    throw new InvalidOperationException("No matching map records found");
                }

                readableMapExtract = matchingMap?.Query;
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while loading the Map viewer form ", errorList);
            }

            return new ServiceResponseModel<ReadableMapExtract>
            {
                Messages = errorList,
                Model = readableMapExtract
            };
        }

        public ServiceResponseModel<ExtractMapSelectionFormModel> GetExtractMapSelectionFormModel()
        {
            List<string> errorList = null;

            ExtractMapSelectionFormModel extractMaps = null;

            try
            {
                _persistenceContainer.Set<string>(BusinessConstants.ExtractDataKey, null);

                extractMaps = new ExtractMapSelectionFormModel
                {
                    ExtractMapNames = _excelForceRepository.GetRecords()?.Select(x => x.Name)?.ToList()
                };
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while Loading the Map selection form ", errorList);
            }

            return new ServiceResponseModel<ExtractMapSelectionFormModel>
            {
                Messages = errorList,
                Model = extractMaps
            };
        }

        public ServiceResponseModel<bool> SubmitExtractMapSelection(string extractMap)
        {
            bool response = false;

            List<string> errorList = null;

            try
            {
                _persistenceContainer.Set(BusinessConstants.ExtractDataKey, extractMap);
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while Submitting the Map selection form ", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = response
            };
        }
        public void GetDataFromExtractMap() {

            List<string> errorList = null;
            
            try
            {
                var savedMapName = _persistenceContainer.Get<string>(BusinessConstants.ExtractDataKey);
                var loginResponse = _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);

                var extractMaps = _excelForceRepository.GetRecords();

                var matchingMap = extractMaps?.FirstOrDefault(x => string.Equals(savedMapName, x.Name));

                if (matchingMap == null)
                {
                    throw new InvalidOperationException("No matching map records found");
                }

                 var query = _sfQueryService.GetStringifiedQuery(matchingMap);

                var extractDataResponse = _sfQueryService.ExtractData(query, loginResponse?.AccessToken, loginResponse?.InstanceUrl);
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while loading the Map viewer form ", errorList);
            }

           
        }
        private void LogException(Exception ex, string errorMessage, IList<string> errorList)
        {
            errorList.Add("An error occurred while fetching field details");

            _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");
        }
    }
}
