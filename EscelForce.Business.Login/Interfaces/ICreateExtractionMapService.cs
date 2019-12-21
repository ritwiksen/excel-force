using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface ICreateExtractionMapService
    {
        ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName);

        ServiceResponseModel<FieldSelectionModel> LoadActionsOnFieldList();

        ServiceResponseModel<bool> CancelCreateExtractionMap();

        ServiceResponseModel<bool> SubmitFieldSelection(string objectName,IList<SfField> fields);

        ServiceResponseModel<ParameterSelectionModel> LoadParameterSelectionScreen();

        ServiceResponseModel<bool> SubmitParameterSelectionScreen(string sortText, string filterText, string childName, string mapName);
    }

    //public class UpdateExtraction
    //{
    //    private SfQuery _sfQuery;

    //    public SfQuery Query
    //    {
    //        get { return _sfQuery; }
    //        set
    //        {
    //            _sfQuery = value;
    //        }
    //    }

    //    public SfQueryMemento UpdateQuery()
    //    {
    //        return new SfQueryMemento(_sfQuery);
    //    }

    //    public void RestoreMemento(SfQuery memento)
    //    {
    //        Query = memento;
    //    }
    //}

    //public class SfQueryMemento
    //{
    //    public SfQuery Query { get; set; }

    //    public SfQueryMemento(SfQuery query)
    //    {
    //        Query = query;
    //    }
    //}

    //public class SfQueryMemory
    //{
    //    private SfQueryMemento _memento;

    //    public SfQueryMemento Memento
    //    {
    //        set { _memento = value; }
    //        get { return _memento; }
    //    }
    //}
}
