using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Data.Tests
{
    [TestClass()]
    class TestsInit
    {
        [AssemblyInitialize()]
        public static void AllTestsInitializer(TestContext context)
        {
            new ApplicationDBContext().UpdateDatabase();
        }
    }
}
