using ExcelForce.Business.Interfaces;

namespace ExcelForce.Business.ServiceFactory
{
    //TODO:(Ritwik):: Should this Factory reside here or in the presentation layer
    public class ExcelForceServiceFactory : IExcelForceServiceFactory
    {
        private readonly ICreateExtractionMapService _createExtractionMapService;

        private readonly IRibbonBaseService _ribbonBaseService;

        private readonly IConfigurationInformationService _configurationInformationService;

        private readonly IUserAuthenticationService _userAuthenticationService;

        public ExcelForceServiceFactory(
            IConfigurationInformationService configurationInfoService,
            ICreateExtractionMapService createExtractionMapService,
            IRibbonBaseService ribbonBaseService,
            IUserAuthenticationService userAuthenticationService)
        {
            _configurationInformationService = configurationInfoService;

            _createExtractionMapService = createExtractionMapService;

            _ribbonBaseService = ribbonBaseService;

            _userAuthenticationService = userAuthenticationService;
        }

        public IConfigurationInformationService GetConnectionProfileService() => _configurationInformationService;

        public ICreateExtractionMapService GetCreateExtractMapService() => _createExtractionMapService;

        public IRibbonBaseService GetRibbonBaseService() => _ribbonBaseService;

        public IUserAuthenticationService GetUserAuthenticationService() => _userAuthenticationService;
    }
}
