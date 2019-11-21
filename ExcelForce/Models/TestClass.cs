using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelForce.Models
{
    public class TestClass : ITestClass
    {
        public string TestMethod()
        {
            return "1";
        }
    }

    public interface ITestClass
    {
        string TestMethod();
    }
}
