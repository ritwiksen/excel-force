using Newtonsoft.Json;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfApiChild
    {
        [JsonProperty("childSObject")]
        public string Name { get; set; }

        [JsonProperty("relationshipName")]
        public string RelationshipField { get; set; }
    }
}
