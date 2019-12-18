using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Business.Interfaces
{
    public interface ICreateExtractionMapService
    {
        ServiceResponseModel<bool> SubmitOnObjectSelection(string objectName, bool isPrimar);

        ServiceResponseModel<FieldSelectionModel> ActionsOnFieldListLoad();
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
