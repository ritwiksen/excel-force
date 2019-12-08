using ExcelForce.Foundation.EntityManagement.Interfaces;

namespace ExcelForce.Foundation.EntityManagement.Models.SortEntities
{
    public class SortByModel
    {
        public ISfField Field { get; set; }

        public bool OrderAscending { get; set; }
    }
}
