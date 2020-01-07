using ExcelForce.Foundation.EntityManagement.Interfaces;
using System.ComponentModel;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfField : ISfField
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("API Name")]
        public string ApiName { get; set; }

        [DisplayName("Length")]
        public string Length { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [Browsable(false)]
        public bool IsRequired { get; set; }

        [Browsable(false)]
        public bool IsCustom { get; set; }

        public string DisplayName() => GetDisplayName(Name, ApiName);

        public static string GetDisplayName(string name, string apiName) => $"{name} ({apiName})";
    }
}

