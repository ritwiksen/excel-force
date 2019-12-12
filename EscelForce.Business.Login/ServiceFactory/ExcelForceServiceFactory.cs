using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Services.ConfigurationInformation;
using ExcelForce.Business.Services.MapExtraction;
using ExcelForce.Business.Services.UserAuthentication;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.Authentication.Services;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.ProfileManagement;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Services;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;

namespace ExcelForce.Business.ServiceFactory
{
    public class ExcelForceServiceFactory : IExcelForceServiceFactory
    {
        private Lazy<IExcelForceRepository<ConnectionProfile, string>> _excelForceRepository;

        private Lazy<IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>> _authenticationManager;

        private Lazy<IServiceCallWrapper<AuthenticationResponse, ApiError>> _authenticationApiWrapper;

        private Lazy<IServiceCallWrapper<SfObjectApiResponse, ApiError>> _sfObjectApiWrapper;

        private Lazy<IWebApiHttpClient> _webApiHttpClient;

        private Lazy<ISfAttributeService> _sfAttributeService;

        private Lazy<ISfObjectService> _sfObjectService;

        private IExtractMapService _extractMapService;

        private IRibbonBaseService _ribbonBaseService;

        private IConfigurationInformationService _configurationInformationService;

        private IUserAuthenticationService _userAuthenticationService;

        private IPersistenceContainer _persistenceContainer;

        public ExcelForceServiceFactory(IPersistenceContainer persistenceContainer)
        {
            _persistenceContainer = persistenceContainer;

            _excelForceRepository
                = new Lazy<IExcelForceRepository<ConnectionProfile, string>>(() => new ConnectionProfileRepository());

            _webApiHttpClient
                = new Lazy<IWebApiHttpClient>(() => new WebApiHttpClient());

            _authenticationApiWrapper
                = new Lazy<IServiceCallWrapper<AuthenticationResponse, ApiError>>(
                    () => new ServiceCallWrapper<AuthenticationResponse, ApiError>(_webApiHttpClient.Value));

            _sfObjectApiWrapper
                 = new Lazy<IServiceCallWrapper<SfObjectApiResponse, ApiError>>(
                     () => new ServiceCallWrapper<SfObjectApiResponse, ApiError>(_webApiHttpClient.Value));

            _authenticationManager
              = new Lazy<IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>>(
                  () => new SalesforceAuthenticationManager(_authenticationApiWrapper.Value, _persistenceContainer));

            _sfAttributeService
                = new Lazy<ISfAttributeService>(() => new SfAttributeService());

            _sfObjectService
                = new Lazy<ISfObjectService>(() => new SfObjectService(_sfObjectApiWrapper.Value, _persistenceContainer));

        }

        public IConfigurationInformationService GetConnectionProfileService()
        {
            if (_configurationInformationService == null)
                _configurationInformationService = new ConfigurationInformationService(_excelForceRepository.Value);

            return _configurationInformationService;
        }

        public IExtractMapService GetExtractMapService()
        {
            if (_extractMapService == null)
                _extractMapService = new ExtractMapService(
                    _sfAttributeService.Value,
                    _sfObjectService.Value,
                    _persistenceContainer);

            return _extractMapService;
        }

        public IRibbonBaseService GetRibbonBaseService()
        {
            if (_ribbonBaseService == null)
                _ribbonBaseService = new RibbonBaseService(_excelForceRepository.Value);

            return _ribbonBaseService;
        }

        public IUserAuthenticationService GetUserAuthenticationService()
        {
            if (_userAuthenticationService == null)
                _userAuthenticationService = new UserAuthenticationService(
                    _authenticationManager.Value, _excelForceRepository.Value, _persistenceContainer);

            return _userAuthenticationService;
        }
    }
}
