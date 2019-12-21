using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface ICreateExtractionMapService
    {
        ServiceResponseModel<ObjectSelectionFormModel> LoadObjectSelectionScreen();

        ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName);

        ServiceResponseModel<FieldSelectionModel> LoadActionsOnFieldList();

        ServiceResponseModel<bool> CancelCreateExtractionMap();

        ServiceResponseModel<bool> SubmitFieldSelection(string objectName,IList<SfField> fields);

        ServiceResponseModel<ParameterSelectionModel> LoadParameterSelectionScreen();

        ServiceResponseModel<bool> SubmitParameterSelectionScreen(string sortText, string filterText, string childName, string mapName);

        ServiceResponseModel<SearchSortExtractionModel> LoadSearchSortScreen();
    }
}
