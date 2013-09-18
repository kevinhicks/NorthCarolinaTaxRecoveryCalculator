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
    public class TaxCalculatorTest
    {
        [TestMethod]
        public void TaxCalculator_CountyTransitTaxRate_ShouldReturnAValidRate()
        {
            Assert.AreEqual(0, TaxCalculator.CountyTransitTaxRate(County.CLAY, DateTime.Now));
            Assert.AreEqual(.5, TaxCalculator.CountyTransitTaxRate(County.ORANGE, DateTime.Now));
        }

        [TestMethod]
        public void TaxCalculator_CountyTaxRate_ShouldReturnAValidRate()
        {
            Assert.AreEqual(2, TaxCalculator.CountyTaxRate(County.CLAY, DateTime.Now));
            Assert.AreEqual(2.25, TaxCalculator.CountyTaxRate(County.ORANGE, DateTime.Now));
        }

        [TestMethod]
        public void TaxCalculator_TotalTaxRate_ShouldReturnAValidRate()
        {
            Assert.AreEqual(6.75, TaxCalculator.TotalTaxRate(County.CLAY, DateTime.Now));
            Assert.AreEqual(7.5, TaxCalculator.TotalTaxRate(County.ORANGE, DateTime.Now));
            Assert.AreEqual(7.25, TaxCalculator.TotalTaxRate(County.MECKLENBURG, DateTime.Now));
        }        
    }
}
