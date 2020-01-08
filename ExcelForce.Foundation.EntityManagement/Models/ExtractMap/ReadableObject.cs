using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    public class ReadableObject
    {

        [JsonProperty("objectLabel")]
        public string Label { get; set; }

        [JsonProperty("apiName")]
        public string ApiName { get; set; }

        [JsonProperty("fields")]
        public IList<SfField> Fields { get; set; }

        [JsonProperty("searchFilter")]
        public string SearchFilter { get; set; }

        [JsonProperty("sortFilter")]
        public string SortFilter { get; set; }
    }
}
