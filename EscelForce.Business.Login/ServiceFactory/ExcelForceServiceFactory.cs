using ExcelForce.Business.Interfaces;
using ExcelForce.Business.Services.ConfigurationInformation;
using ExcelForce.Business.Services.MapExtraction;
using ExcelForce.Business.Services.UserAuthentication;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.Authentication.Services;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.Persitence;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.ProfileManagement;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Collections.Generic;

namespace ExcelForce.Business.ServiceFactory
{
    public class ExcelForceServiceFactory : IExcelForceServiceFactory
    {
        private Lazy<IExcelForceRepository<ConnectionProfile, string>> _excelForceRepository;

        private Lazy<IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>> _authenticationManager;

        private Lazy<IServiceCallWrapper<AuthenticationApiRequest, ErrorModel>> _authenticationApiWrapper;

        private Lazy<IWebApiHttpClient> _webApiHttpClient;

        private IExtractMapService _extractMapService;

        private IRibbonBaseService _ribbonBaseService;

        private IConfigurationInformationService _configurationInformationService;

        private IUserAuthenticationService _userAuthenticationService;

        public ExcelForceServiceFactory(IPersistenceManager<IEnumerable<SfField>> attributeManager)
        {
            _excelForceRepository
                = new Lazy<IExcelForceRepository<ConnectionProfile, string>>(() => new ConnectionProfileRepository());

            _webApiHttpClient
                = new Lazy<IWebApiHttpClient>(() => new WebApiHttpClient());

            _authenticationApiWrapper
                = new Lazy<IServiceCallWrapper<AuthenticationApiRequest, ErrorModel>>(
                    () => new ServiceCallWrapper<AuthenticationApiRequest, ErrorModel>(_webApiHttpClient.Value));

            _authenticationManager
              = new Lazy<IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>>(
                  () => new SalesforceAuthenticationManager(_authenticationApiWrapper.Value));
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
                _extractMapService = new ExtractMapService();

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
                    _authenticationManager.Value, _excelForceRepository.Value);

            return _userAuthenticationService;
        }
    }
}
