using Newtonsoft.Json;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{

    public class SfApiField
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public string Length { get; set; }
    }
}
