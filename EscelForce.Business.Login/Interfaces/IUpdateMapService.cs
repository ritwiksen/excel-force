using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IUpdateMapService
    {
        ServiceResponseModel<IEnumerable<string>> GetMapNames();

        ServiceResponseModel<IEnumerable<string>> GetObjectNames();
        IEnumerable<SfField> GetFieldsByName(string name);
    }
}
