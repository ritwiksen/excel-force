using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfObjectService
    {
        IEnumerable<SfObject> GetObjects(string instanceUrl,string bearerToken);

        IEnumerable<SfObject> GetChildrenForObject(string instanceUrl, string bearerToken, string parentObjectName);
    }
}
