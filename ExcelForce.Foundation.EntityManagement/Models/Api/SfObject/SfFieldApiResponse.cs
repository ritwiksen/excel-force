using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfFieldApiResponse
    {
        [JsonProperty("fields", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SfApiField> Fields { get; set; }

        [JsonProperty("childRelationships", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SfApiChild> Children { get; set; }
    }
}
