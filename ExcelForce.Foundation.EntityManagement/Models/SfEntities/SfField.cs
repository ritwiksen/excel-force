using ExcelForce.Foundation.EntityManagement.Interfaces;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfField<T>: ISfField
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }
    }
}
