using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class ProjectModelsTest
    {
        [TestMethod]
        public void TestFindCountyTaxForAllReciepts()
        {
            Project project = TestProjectsData.GetInstance().Projects[2];

            int countyIndex = County.DURHAM;
            var reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex);

            Assert.AreEqual(121.03, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);
        }        
    }
}
