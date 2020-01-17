using ExcelForce.Business.Models.ExtractionMap.ExtractData;
using ExcelForce.Foundation.CoreServices.Models;

namespace ExcelForce.Business.Interfaces
{
    public interface IExtractDataService
    {
        ServiceResponseModel<ExtractMapSelectionFormModel> GetExtractMapSelectionFormModel();
    }
}
