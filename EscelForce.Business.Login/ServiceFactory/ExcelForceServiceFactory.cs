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

        private readonly IExtractMapService _extractMapService;

        public ExcelForceServiceFactory(
            IConfigurationInformationService configurationInfoService,
            ICreateExtractionMapService createExtractionMapService,
            IRibbonBaseService ribbonBaseService,
            IUserAuthenticationService userAuthenticationService,
            IExtractMapService extractMapService)
        {
            _configurationInformationService = configurationInfoService;

            _createExtractionMapService = createExtractionMapService;

            _ribbonBaseService = ribbonBaseService;

            _userAuthenticationService = userAuthenticationService;

            _extractMapService = extractMapService;
        }

        public IConfigurationInformationService GetConnectionProfileService() => _configurationInformationService;

        public ICreateExtractionMapService GetCreateExtractMapService() => _createExtractionMapService;

        public IRibbonBaseService GetRibbonBaseService() => _ribbonBaseService;

        public IUserAuthenticationService GetUserAuthenticationService() => _userAuthenticationService;

        public IExtractMapService GetExtractMapService() => _extractMapService;
    }
}
