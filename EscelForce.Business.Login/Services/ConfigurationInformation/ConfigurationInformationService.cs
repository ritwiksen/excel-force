using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ConnectionProfile> GetSavedConnectionProfiles()
        {
            var records = _connectionProfileRepository.GetRecords()?.ToList();

            records?.RemoveAll(x => string.IsNullOrWhiteSpace(x.ClientSecret)
             || string.IsNullOrWhiteSpace(x.ConsumerKey)
             || string.IsNullOrWhiteSpace(x.Name));

            return records;
        }

        public bool PerformConnectionSubmitActions(ConnectionProfile profile)
        {
            return _connectionProfileRepository.AddRecord(profile);
        }

        public bool ShowLoginFormFromConnectionInformation()
        {
            var existingProfiles = _connectionProfileRepository.GetRecords();

            return (existingProfiles?.Any() ?? false)
                ? false
                : true;
        }
    }
}
