using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfFieldApiResponse
    {
        [JsonProperty("fields")]
        public IList<SfApiField> Fields { get; set; }
    }
}
