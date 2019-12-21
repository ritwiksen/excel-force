using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfAttributeService
    {
        IEnumerable<ISfField> GetSfFields(string objectName, string bearerToken, string instanceUrl);
    }
}
