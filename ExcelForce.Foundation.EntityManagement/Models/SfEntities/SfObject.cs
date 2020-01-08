﻿using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfObject
    {
        public string Name { get; set; }
        public string Label { get; set; }

        public IEnumerable<SfField> Fields { get; set; }

        public string FilterExpressions { get; set; }

        public string SortExpressions { get; set; }

        public bool IsPrimary { get; set; }

        public IEnumerable<SfField> GetSystemFields() => Fields?.Where(x => !x.IsCustom);

        public IEnumerable<SfField> GetCustomFields() => Fields?.Where(x => x.IsCustom);
        public string DisplayName() => GetDisplayName(Name, Label);
        public static string GetDisplayName(string apiName, string name) => $"{name} | {apiName}";
    }
}
