using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    public class ReadableMapExtract
    {
        [JsonProperty("parent")]
        public ReadableObject Parent { get; set; }

        [JsonProperty("children")]
        public IList<ReadableObject> Children { get; set; }
    }
}
