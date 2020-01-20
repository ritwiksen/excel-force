namespace ExcelForce.Business.Interfaces
{
    public interface IExcelForceServiceFactory
    {
        IRibbonBaseService GetRibbonBaseService();

        IConfigurationInformationService GetConnectionProfileService();

        IUserAuthenticationService GetUserAuthenticationService();

        ICreateExtractionMapService GetCreateExtractMapService();

        IExtractMapService GetExtractMapService();

        IExtractDataService GetExtractDataService();
    }
}
