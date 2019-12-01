using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.ProfileManagement.Models;

namespace ExcelForce.Business.Services.ConfigurationInformation
{
    public class ConfigurationInformationService : IConfigurationInformationService
    {
        private IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        public ConfigurationInformationService(IExcelForceRepository<ConnectionProfile, string> connectionProfileRepository)
        {
            _connectionProfileRepository = connectionProfileRepository;
        }

        public bool PerformConnectionSubmitActions(ConnectionProfile profile)
        {
            return _connectionProfileRepository.AddRecord(profile);
        }
    }
}
