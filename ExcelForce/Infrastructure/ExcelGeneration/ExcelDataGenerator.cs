using ExcelForce.Foundation.EntityManagement.Interfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using System;
using System.Collections.Generic;

namespace ExcelForce.Infrastructure.ExcelGeneration
{
    public sealed class ExcelDataGenerator : IActionOnSfData
    {
        public bool ActionOnSfExtractData(SfExtractDataWrapper extractData)
        {
            throw new NotImplementedException();
        }

        private static void GenerateExcelSheet(Dictionary<string, object> keyValues,string sheetName)
        {

        }
    }
}
