using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface IReadableExtractMapService
    {
        ReadableMapExtract GetContentFromQuery(SfQuery query);
    }
}
