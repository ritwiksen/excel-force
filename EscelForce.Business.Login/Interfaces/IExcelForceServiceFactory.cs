namespace ExcelForce.Business.Interfaces
{
    public interface IExcelForceServiceFactory
    {
        IRibbonBaseService GetRibbonBaseService();

        IConfigurationInformationService GetConnectionProfileService();
    }
}
