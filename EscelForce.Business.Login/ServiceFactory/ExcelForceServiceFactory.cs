using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.ProfileManagement;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;

namespace ExcelForce.Business.ServiceFactory
{
    public class ExcelForceServiceFactory : IExcelForceServiceFactory
    {
        private Lazy<IExcelForceRepository<ConnectionProfile, string>> _excelForceRepository;

        private IRibbonBaseService _ribbonBaseService;

        public ExcelForceServiceFactory()
        {
            _excelForceRepository 
                = new Lazy<IExcelForceRepository<ConnectionProfile, string>>(() => new ConnectionProfileRepository());
        }

        public IRibbonBaseService GetRibbonBaseService()
        {
            if (_ribbonBaseService == null)
                _ribbonBaseService = new RibbonBaseService(_excelForceRepository.Value);

            return _ribbonBaseService;
        }
    }
}
