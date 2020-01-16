using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    public class SfExtractDataModel
    {
        public SfExtractDataModel()
        {
            Data = new Dictionary<string, object>();
        }

        public string Url { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }
}
