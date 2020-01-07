using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.ComponentModel;

namespace ExcelForce.Models
{
    public class SfFieldDataGrid : SfField
    {
        [DisplayName("Selected")]
        public bool IsSelected { get; set; }
    }
}
