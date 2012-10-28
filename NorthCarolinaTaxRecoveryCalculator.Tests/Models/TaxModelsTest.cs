using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Controllers
{
    [TestClass]
    public class TaxModelsTest
    {
        [TestMethod]
        public void TestTransitTaxRate()
        {
            Assert.AreEqual(TaxContext.CountyTransitTaxRate(County.MECKLENBURG), .5);
            Assert.AreNotEqual(TaxContext.CountyTransitTaxRate(County.ALLEGHANY), .5);
        }

        [TestMethod]
        public void TestCountyTaxRate()
        {
            //On Oct 28, 2012, Ashe tax rate = 2.0
            Assert.AreEqual(TaxContext.CountyTaxRate(County.ASHE, new DateTime(2012, 10, 28)), 2.0);
            //On Feb 2, 2012, Halifax tax rate = 2.25
            Assert.AreEqual(TaxContext.CountyTaxRate(County.HALIFAX, new DateTime(2012, 2, 19)), 2.25);
        }

        [TestMethod]
        public void TestStateTaxRate()
        {
            Assert.AreEqual(TaxContext.StateTaxRate, 4.75);
        }


    }
}
