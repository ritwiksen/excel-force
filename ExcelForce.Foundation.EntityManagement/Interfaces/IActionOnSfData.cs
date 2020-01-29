using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Interfaces
{
    public interface IActionOnSfData
    {
        bool ActionOnSfExtractData(SfExtractDataWrapper extractData);
    }
}
