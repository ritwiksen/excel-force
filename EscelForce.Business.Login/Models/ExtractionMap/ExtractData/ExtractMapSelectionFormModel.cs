using System.Collections.Generic;

namespace ExcelForce.Business.Models.ExtractionMap.ExtractData
{
    public class ExtractMapSelectionFormModel
    {
        public IList<string> ExtractMapNames { get; set; }

        public string SelectedExtractMap { get; set; }
    }
}
