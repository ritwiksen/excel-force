using System;
using System.Collections;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfDisplayData
    {
        public string ItemUrl { get; set; }

        public string ObjectName { get; set; }

        private Hashtable DisplayData { get; set; }

        public void SetData<T>(string key, T value)
        {
            DisplayData = DisplayData ?? new Hashtable();

            if (DisplayData.ContainsKey(key))
            {
                DisplayData[key] = value;
            }
            else
            {
                DisplayData.Add(key, value);
            }
        }

        public T GetData<T>(string key)
        {
            return DisplayData.ContainsKey(key)
                ? (T)DisplayData[key]
                : default(T);
        }

        public IEnumerable<string> GetPropertyNameList()
        {
            if (DisplayData == null)
                yield return null;

            foreach (var key in DisplayData?.Keys)
            {
                yield return Convert.ToString(key);
            }
        }
    }
}
