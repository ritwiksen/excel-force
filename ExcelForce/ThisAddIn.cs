using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExcelForce
{
    public partial class ThisAddIn
    {
        ListObject list1;
        Worksheet worksheet;
        internal void BindDatatoExcel(JArray trgArray)
        {

            DataTable tester = JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
            worksheet = Globals.Factory.GetVstoObject(
                   this.Application.ActiveWorkbook.ActiveSheet);

            worksheet.Controls.Remove("list1");
            //Excel.Range selection = Globals.ThisAddIn.Application.Selection as Excel.Range;
            list1 = worksheet.Controls.AddListObject(worksheet.Range["A1"], "list1");

            // Bind the list object to the table.

            list1.AutoSetDataBoundColumnHeaders = true;
            list1.SetDataBinding(tester);

        }

        public JArray AddingRowsToArray(JArray trgArray, string json)
        {
            var jsonLinq = JObject.Parse(json);
            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            //var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }

                trgArray.Add(cleanRow);
            }
            return trgArray;

        }
        /*public static DataTable Tabulate(string json)
        {
            var jsonLinq = JObject.Parse(json);
             // Find the first array using Linq
                var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
                var trgArray = new JArray();
             foreach (JObject row in srcArray.Children<JObject>())
                {
                    var cleanRow = new JObject();
                    foreach (JProperty column in row.Properties())
                    {
                        // Only include JValue types
                        if (column.Value is JValue)
                        {
                            cleanRow.Add(column.Name, column.Value);
                        }
                    }

                    trgArray.Add(cleanRow);
                }
            

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }*/
        public Excel.Range GetValuesRange()
        {

            int UsedRows = 0;
            int RowCounter = 1;
            int RowsToCount = 10;
            int UsedColumns = 0;
            int ColumnCounter = 1;
            int ColumnsToCount = 5;
            Worksheet workSheet = Globals.Factory.GetVstoObject(
                   this.Application.ActiveWorkbook.ActiveSheet);
            // Excel.Range localRange = worksheet.UsedRange;
            // var totalcolumn = localRange.Columns.Count;
            // var totalrow = localRange.Rows.Count;



            do
            {
                do
                {
                    var cellValue = workSheet.Cells[RowCounter, ColumnCounter].Value2;

                    if (!String.IsNullOrWhiteSpace(cellValue))
                    {
                        UsedRows = RowCounter;
                        if (ColumnCounter > UsedColumns)
                        {
                            UsedColumns = ColumnCounter;
                        }
                        if (UsedRows == RowsToCount)
                        {
                            RowsToCount = RowsToCount + 100;
                        }
                        if (UsedColumns == ColumnsToCount)
                        {
                            ColumnsToCount = ColumnsToCount + 50;
                        }
                    }
                    ColumnCounter = ColumnCounter + 1;
                } while (ColumnCounter <= ColumnsToCount);
                RowCounter = RowCounter + 1;
                ColumnCounter = 1;

            } while (RowCounter <= RowsToCount);
            var workSheetRange = workSheet.Range["a1", workSheet.Cells[UsedRows, UsedColumns].Address];
            return workSheetRange;
        }
        public String ToInsertJSON(Excel.Range RangeToParse, String ObjectName)
        {
            int RowCounter;
            int ColumnCounter;
            String RecordsString = "{\"" + "records" + "\"" + ":";
            String AttributeString = "\"" + "attributes" + "\"" +
                                     ":{" + "\"" + "type" + "\"" + ":" + "\"" + ObjectName + "\"" + "," +
                                     "\"" + "referenceId" + "\"" + ":" + "\"" + "ref";
            String ParsedData = RecordsString + "[";
            String temp;

            for (RowCounter = 2; RowCounter <= RangeToParse.Rows.Count; RowCounter++)
            {
                temp = "";
                for (ColumnCounter = 1; ColumnCounter <= RangeToParse.Columns.Count; ColumnCounter++)
                {
                    temp = temp + "\"" + RangeToParse.Cells[1, ColumnCounter].Value2 + "\"" + ":" + "\"" + RangeToParse.Cells[RowCounter, ColumnCounter].Value2 + "\"" + ",";
                }
                temp = "{" + AttributeString + RowCounter + "\"" + "}," + temp.Substring(0, temp.Length - 1) + "},";
                ParsedData = ParsedData + temp;
                //ExcelForce.columnName;
            }
            ParsedData = ParsedData.Substring(0, ParsedData.Length - 1) + "]}";
            return ParsedData;
        }
        public String ToUpdateJSON(Excel.Range RangeToParse, String ObjectName)
        {
            int RowCounter;
            int ColumnCounter;
            String batchRequestsString = "{\"" + "batchRequests" + "\"" + ":";
            String methodString = "\"" + "method" + "\"" + ":" + "\"" + "PATCH" + "\"" + ",";
            String urlString = "\"" + "url" + "\"" + ":" + "\"" + "v45.0/sobjects/" + ObjectName + "/";
            String richInputString = "\"" + "richInput" + "\"" + ":" + "{";
            String ParsedData = batchRequestsString + "[";
            String temp;
            String tempUrlString;

            for (RowCounter = 2; RowCounter <= RangeToParse.Rows.Count; RowCounter++)
            {
                temp = "";
                tempUrlString = urlString;
                for (ColumnCounter = 1; ColumnCounter <= RangeToParse.Columns.Count; ColumnCounter++)
                {
                    if (RangeToParse.Cells[1, ColumnCounter].Value2 == "Id" || RangeToParse.Cells[1, ColumnCounter].Value2 == "ID")
                    {
                        tempUrlString = tempUrlString + RangeToParse.Cells[RowCounter, ColumnCounter].Value2 + "\"" + ",";
                    }
                    else
                    {
                        temp = temp + "\"" + RangeToParse.Cells[1, ColumnCounter].Value2 + "\"" + ":" + "\"" + RangeToParse.Cells[RowCounter, ColumnCounter].Value2 + "\"" + ",";
                    }
                }
                temp = "{" + methodString + tempUrlString + richInputString + temp.Substring(0, temp.Length - 1) + "}},";
                ParsedData = ParsedData + temp;
            }
            ParsedData = ParsedData.Substring(0, ParsedData.Length - 1) + "]}";
            return ParsedData;
        }
        public String ToDeleteJson(Excel.Range RangeToParse)
        {
            int RowCounter;
            int ColumnCounter;
            String temp = "";

            for (RowCounter = 2; RowCounter <= RangeToParse.Rows.Count; RowCounter++)
            {
                for (ColumnCounter = 1; ColumnCounter <= RangeToParse.Columns.Count; ColumnCounter++)
                {
                    if (RangeToParse.Cells[1, ColumnCounter].Value2 == "Id" || RangeToParse.Cells[1, ColumnCounter].Value2 == "ID")
                    {
                        temp = temp + RangeToParse.Cells[RowCounter, ColumnCounter].Value2 + ",";
                    }
                }

            }
            temp = temp.Substring(0, temp.Length - 1) + "&allOrNone=false";
            return temp;
        }
        void Application_WorkbookBeforeSave(Microsoft.Office.Interop.Excel.Workbook workbook,
          bool SaveAsUI, ref bool Cancel)
        {
            /*Excel.Worksheet worksheet =
                workbook.Worksheets[1] as Excel.Worksheet;

            if (Globals.Factory.HasVstoObject(worksheet) &&
                Globals.Factory.GetVstoObject(worksheet).Controls.Count > 0)
            {
                Worksheet vstoWorksheet = Globals.Factory.GetVstoObject(worksheet);



                while (vstoWorksheet.Controls.Count > 0)
                {
                    object vstoControl = vstoWorksheet.Controls[1];
                    vstoWorksheet.Controls.Remove(vstoControl);
                }

            }*/
        }
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.WorkbookBeforeSave +=
                new Excel.AppEvents_WorkbookBeforeSaveEventHandler
                    (Application_WorkbookBeforeSave);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void SplitButton_OnLoad()
        {
         

        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }


    }

    #endregion
}

