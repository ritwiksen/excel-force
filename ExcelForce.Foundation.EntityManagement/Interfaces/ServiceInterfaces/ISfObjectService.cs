using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfObjectService
    {
        IEnumerable<string> GetObjectNames();
    }
}
