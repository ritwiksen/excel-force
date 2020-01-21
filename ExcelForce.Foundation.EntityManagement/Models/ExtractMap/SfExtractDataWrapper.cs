using ExcelForce.Foundation.EntityManagement.Infrastructure.CustomSerializers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    [JsonConverter(typeof(SfDisplayDataSerializer))]
    public class SfExtractDataWrapper
    {
        public SfExtractDataWrapper()
        {
            Data = new List<SfExtractDataModel>();
        }

        public string ObjectName { get; set; }

        public IList<SfExtractDataModel> Data { get; set; }
    }
}
