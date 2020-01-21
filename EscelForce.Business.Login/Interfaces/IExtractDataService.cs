using ExcelForce.Business.Models.ExtractionMap.ExtractData;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;

namespace ExcelForce.Business.Interfaces
{
    public interface IExtractDataService
    {
        ServiceResponseModel<ExtractMapSelectionFormModel> GetExtractMapSelectionFormModel();

        ServiceResponseModel<bool> SubmitExtractMapSelection(string extractMap);

        ServiceResponseModel<ReadableMapExtract> GetEtxractMapViewerFormModel();

        void getDataFromExtractMap();


    }
}
