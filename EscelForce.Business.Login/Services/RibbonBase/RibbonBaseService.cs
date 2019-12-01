using ExcelForce.Business;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.ProfileManagement.Models;
using System.Linq;

namespace ExcelForce.Business
{
    public class RibbonBaseService : IRibbonBaseService
    {
        private IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        public RibbonBaseService(
            IExcelForceRepository<ConnectionProfile, string> connectionProfileRepository)
        {
            _connectionProfileRepository = connectionProfileRepository;
        }

        public bool LoadConnectionProfilePopup()
        {
            var existingConnectionProfiles = _connectionProfileRepository.GetRecords();

            return (!existingConnectionProfiles?.Any()) ?? true;
        }
    }
}
