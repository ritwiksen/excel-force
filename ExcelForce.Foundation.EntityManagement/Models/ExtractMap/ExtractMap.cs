using ExcelForce.Foundation.CoreServices.Models.Interfaces;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    public class ExtractMap : IExcelForceModel
    {
        public string Name { get; set; }

        public string Query { get; set; }
    }
}
