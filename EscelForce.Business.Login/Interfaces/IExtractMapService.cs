using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IExtractMapService
    {
        IEnumerable<string> GetObjectNames();

        IEnumerable<string> GetObjectsByName(string name);

        IEnumerable<string> GetAttributesByName(string name, int pageSize, int pageNumber);

        SfField GetAttributeData(string name);
    }
}
