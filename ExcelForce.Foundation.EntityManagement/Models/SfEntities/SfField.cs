using ExcelForce.Foundation.EntityManagement.Interfaces;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfField : ISfField
    {
        public string Name { get; set; }

        public string ApiName { get; set; }

        public bool IsRequired { get; set; }

        public bool IsCustom { get; set; }

        public string DisplayName() => $"{Name} ({ApiName})";
    }
}

