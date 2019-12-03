using ExcelForce.Foundation.ProfileManagement.Models;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IConfigurationInformationService
    {
        bool PerformConnectionSubmitActions(ConnectionProfile profile);

        IEnumerable<ConnectionProfile> GetSavedConnectionProfiles();
    }
}
