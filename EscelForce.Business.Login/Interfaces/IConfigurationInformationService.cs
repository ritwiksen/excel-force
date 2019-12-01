using ExcelForce.Foundation.ProfileManagement.Models;

namespace ExcelForce.Business.Interfaces
{
    public interface IConfigurationInformationService
    {
        bool PerformConnectionSubmitActions(ConnectionProfile profile);
    }
}
