using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfQueryService
    {
        string GetStringifiedQuery(ExtractMap query);

        SfQuery MapStringifiedQuery(string query);  

        bool IsValidQuery(SfQuery query);

        SfExtractDataWrapper ExtractData(string query, string AccessToken, string InstanceUrl);
    }
}
