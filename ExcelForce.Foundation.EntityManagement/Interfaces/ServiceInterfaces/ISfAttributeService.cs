using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfAttributeService
    {
        IEnumerable<SfField> GetSfFields(string objectName, string bearerToken, string instanceUrl);

        IEnumerable<SfChildRelationship> GetChildRelationships(string objectName, string bearerToken, string instanceUrl);
    }
}
