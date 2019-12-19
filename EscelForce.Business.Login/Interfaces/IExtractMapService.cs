using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IExtractMapService
    {
        ServiceResponseModel<IEnumerable<string>> GetObjectNames();

        IEnumerable<string> GetObjectsByName(string name, string bearerToken);

        IEnumerable<SfField> GetFieldsByName(string name, int pageSize, int pageNumber);

        SfField GetAttributeData(string name);
    }
}
