using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces
{
    public interface ISfQueryService
    {
        string GetStringifiedQuery(SfQuery query);

        SfQuery MapStringifiedQuery(string query);  

        bool IsValidQuery(SfQuery query);
    }
}
