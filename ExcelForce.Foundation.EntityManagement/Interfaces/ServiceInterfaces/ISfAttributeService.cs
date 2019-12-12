using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfAttributeService
    {
        IEnumerable<ISfField> GetSfFields();

        ISfField GetSfFieldByName(string name);

        IEnumerable<ISfField> SearchSfFieldByName(string name);
    }
}
