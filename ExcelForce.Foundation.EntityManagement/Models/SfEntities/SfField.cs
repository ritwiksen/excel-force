using ExcelForce.Foundation.EntityManagement.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfField : ISfField
    {
        [DisplayName("Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DisplayName("API Name")]
        [JsonProperty("apiName")]
        public string ApiName { get; set; }

        [DisplayName("Length")]
        [JsonProperty("length")]
        public string Length { get; set; }

        [DisplayName("Type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        public bool IsMandatory { get; set; }

        public string DisplayName() => GetDisplayName(Name, ApiName);

        public static string GetDisplayName(string name, string apiName) => $"{name} ({apiName})";
    }
}

