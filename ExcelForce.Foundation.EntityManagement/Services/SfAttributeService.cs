using System.Collections.Generic;
using ExcelForce.Foundation.EntityManagement.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfAttributeService : ISfAttributeService
    {
        public SfAttributeService()
        {

        }

        public ISfField GetSfFieldByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ISfField> GetSfFields()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ISfField> SearchSfFieldByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
