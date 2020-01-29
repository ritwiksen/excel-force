using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Interfaces
{
    public interface IUpdateMapService
    {
        ServiceResponseModel<IEnumerable<string>> GetMapNames();

        ServiceResponseModel<IEnumerable<string>> GetObjectNames();

        ServiceResponseModel<SfObject> GetObjectNameByMapName(string mapName);

        ServiceResponseModel<IEnumerable<SfObject>> GetChildrenssByName(string name);

        IEnumerable<SfField> GetFieldsByName(string name);
        IEnumerable<SfField> GetFieldsByMapParentObjectName(string name);

        ServiceResponseModel<IEnumerable<SfChildRelationship>> GetChildRelationships(string objectName);


    }
}
