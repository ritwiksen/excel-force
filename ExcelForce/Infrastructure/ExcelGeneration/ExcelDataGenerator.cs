﻿using ExcelForce.Foundation.EntityManagement.Interfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using Microsoft.Office.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelForce.Infrastructure.ExcelGeneration
{
    public sealed class ExcelDataGenerator : IActionOnSfData
    {
        public bool ActionOnSfExtractData(SfExtractDataWrapper extractData, IList<ReadableObject> children)
        {
            try
            {
                var primaryObject = extractData.ObjectName;

                var objectList = extractData?.GetObjects();

                if (!(objectList?.Any() ?? false))
                    return false;

                if (children?.Any() ?? false)
                {
                    foreach (var child in children)
                    {
                        if (!objectList.Any(x => x.Key == child.Label))
                            objectList.Add(child.Label, new DataTable());

                        if (objectList[primaryObject].Columns.Contains(child.RelationshipName))
                            objectList[primaryObject].Columns.Remove(child.RelationshipName);

                    }
                }

                if (objectList.Keys?.Any(x => x != primaryObject) ?? false)
                {
                    foreach (var key in objectList.Keys?.Where(x => x != primaryObject))
                    {
                        PerformTasksOnIndividualSheet(key, objectList[key]);
                    }
                }

                PerformTasksOnIndividualSheet(primaryObject, objectList[primaryObject]);

                return true;
            }
            catch (Exception ex)
            {
                //TODO:(RItwik):: Add logging here
                return false;
            }
        }

        private bool PerformTasksOnIndividualSheet(string sheetName, DataTable data)
        {
            var sheet = GenerateExcelSheet(sheetName);

            return PopulateWorkSheetWithData(sheetName, data);
        }

        private static Worksheet GenerateExcelSheet(string sheetName)
        {
            Worksheet worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.Worksheets.Add());

            worksheet.Name = sheetName;

            return worksheet;
        }

        private static bool PopulateWorkSheetWithData(string sheetName, DataTable data)
        {
            var sheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.Worksheets.Item[sheetName]);

            return PopulateWorksheet(sheet, data);
        }

        private static bool PopulateWorksheet(Worksheet worksheet, DataTable data)
        {
            for (int i = 0; i < data.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = data.Columns[i].ColumnName;
            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (var j = 0; j < data.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = Convert.ToString(data.Rows[i][j]);
                }
            }

            return true;
        }
    }
}
