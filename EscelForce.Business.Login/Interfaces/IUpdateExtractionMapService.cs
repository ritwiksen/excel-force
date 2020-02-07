using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IUpdateExtractionMapService
    {
        ServiceResponseModel<ObjectSelectionFormModel> LoadMapSelectionScreen();

        ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName);

        ServiceResponseModel<SfQuery> SubmitOnMapSelection(string mapName);

        ServiceResponseModel<FieldSelectionModel> LoadActionsOnFieldList();

        ServiceResponseModel<bool> CancelUpdateExtractionMap();

        ServiceResponseModel<bool> SubmitFieldSelection(string objectName,IList<SfField> fields);

        ServiceResponseModel<ParameterSelectionModel> LoadParameterSelectionScreen();

        ServiceResponseModel<bool> SubmitParameterSelectionScreen(SearchSortExtractionModel model);

        ServiceResponseModel<SearchSortExtractionModel> LoadSearchSortScreen();

        ServiceResponseModel<SearchSortExtractionModel> LoadChildSearchSortScreen(string child);

        ServiceResponseModel<FieldSelectionModel> SubmitForNewChild(SearchSortExtractionModel model);

        ServiceResponseModel<bool> AreChildrenAvailable();

        ServiceResponseModel<bool> SubmitPreviousFieldSelection();

        ServiceResponseModel<bool> DeleteSelectedChild(string childName);
        ServiceResponseModel<bool> clear();
    }
}
