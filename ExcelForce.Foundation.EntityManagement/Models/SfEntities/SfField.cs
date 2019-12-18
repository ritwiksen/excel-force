using ExcelForce.Foundation.EntityManagement.Interfaces;
using System;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfField : ISfField
    {
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public bool IsCustom { get; set; }

        public Type Type { get; set; }
    }
}
