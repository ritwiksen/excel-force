using System.Collections.Generic;
using System.Data;

namespace ExcelForce.Foundation.EntityManagement.Models.ExtractMap
{
    public class SfExtractDataModel
    {
        public SfExtractDataModel()
        {
            Data = new Dictionary<string, object>();
        }

        public string Url { get; set; }

        public string Id { get; set; }

        public Dictionary<string, object> Data { get; set; }
    }


    public static class ExcelDataHelpers
    {
        public static Dictionary<string, DataTable> GetObjects(this SfExtractDataWrapper obj)
        {
            var dataTableCollection = new Dictionary<string, DataTable>();

            return AddData(dataTableCollection, obj);
        }

        private static Dictionary<string, DataTable> AddData(
            Dictionary<string, DataTable> dataTableCollection,
            SfExtractDataWrapper obj
            , bool isChild = false
            , string parentId = null
            , string parentname = null)
        {
            if (obj != null)
            {
                if (!dataTableCollection.ContainsKey(obj.ObjectName))
                    dataTableCollection.Add(obj.ObjectName, new DataTable());

                var dataTable = dataTableCollection[obj.ObjectName];              

                foreach (var item in obj.Data)
                {
                    dataTable.TableName = obj.ObjectName;

                    if (!dataTable.Columns.Contains("Id"))
                        dataTable.Columns.Add("Id");

                    DataRow dataRow = dataTable.NewRow();

                    dataRow["Id"] = item.Id;

                    foreach (var objValue in item.Data)
                    {
                        if (objValue.Value is SfExtractDataWrapper)
                        {
                            dataTableCollection = AddData(
                                dataTableCollection,
                                (SfExtractDataWrapper)objValue.Value,
                                true,
                                item.Id,
                                obj.ObjectName);
                        }
                        else
                        {
                            if (!dataTable.Columns.Contains(objValue.Key))
                                dataTable.Columns.Add(objValue.Key);

                            dataRow[objValue.Key] = objValue.Value;
                        }                     
                    }
                    if (isChild)
                    {
                        if (!dataTable.Columns.Contains($"{parentname}Id"))
                            dataTable.Columns.Add($"{parentname}Id");

                        dataRow[$"{parentname}Id"] = parentId;
                    }

                    dataTable.Rows.Add(dataRow);
                }               
            }
            return dataTableCollection;
        }
    }
}
