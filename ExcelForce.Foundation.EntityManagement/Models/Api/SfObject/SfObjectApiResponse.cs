using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfObjectApiResponse
    {
        [JsonProperty("sobjects")]
        public IList<SfApiField> SalesforceObjects { get; set; }
    }
}
