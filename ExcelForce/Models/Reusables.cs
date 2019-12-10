using System.Collections.Generic;

namespace ExcelForce.Models
{
    public class Reusables
    {
        private static Reusables instance;

        public string ConnectionProfile { get; set; }

        public IEnumerable<string> FieldsForSearch { get; set; }

        private Reusables() { }

        public static Reusables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reusables();
                }
                return instance;
            }
        }
    }
}
