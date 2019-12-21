using Newtonsoft.Json;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfChild
    {
        [JsonProperty("childSObject")]
        public string Name { get; set; }
    }
}
