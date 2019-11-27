using ExcelForce.Foundation.CoreServices.Models.Interfaces;
using Newtonsoft.Json;
using System;

namespace ExcelForce.UserProfile.Models
{
    [Serializable]
    public class ConnectionProfile : IExcelForceModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("consumerKey")]
        public string ConsumerKey { get; set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }

        [JsonProperty("isProduction")]
        public bool IsProduction { get; set; }
    }
}
