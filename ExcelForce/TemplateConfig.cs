using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelForce
{
    public class TemplateConfig
    {
        public List<Template> templates { get; set; }
    }

    public class Template
    {
        public string name { get; set; }
        public List<Option> options { get; set; }
    }

    public class Option
    {
        public string displayName { get; set; }
        public List<string> option { get; set; }
    }
}
