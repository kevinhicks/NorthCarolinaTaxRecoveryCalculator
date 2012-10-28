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
            Assert.AreEqual(2.0, TaxContext.CountyTaxRate(County.ASHE, new DateTime(2012, 10, 28)));
            //On Dec 31, 2011, Halifax tax rate = 2.0
            Assert.AreEqual(2.0, TaxContext.CountyTaxRate(County.HALIFAX, new DateTime(2011, 12, 31)));
            //On Jan 1, 2012, Halifax tax rate = 2.25
            Assert.AreEqual(2.25, TaxContext.CountyTaxRate(County.HALIFAX, new DateTime(2012, 1, 1)));
            //On Mar 31, 2012, Durham tax rate = 2.0
            Assert.AreEqual(2.0, TaxContext.CountyTaxRate(County.DURHAM, new DateTime(2012, 3, 31)));
            //On Apr 1, 2012, Durham tax rate = 2.25
            Assert.AreEqual(2.25, TaxContext.CountyTaxRate(County.DURHAM, new DateTime(2012, 4, 1)));
            //On Feb 2, 2012, Halifax tax rate = 2.25
            Assert.AreEqual(2.25, TaxContext.CountyTaxRate(County.HALIFAX, new DateTime(2012, 2, 19)));
        }

        [TestMethod]
        public void TestStateTaxRate()
        {
            Assert.AreEqual(TaxContext.StateTaxRate, 4.75);
        }

        [TestMethod]
        public void TestTotalTaxRate()
        {
            //On Oct 28, 2012, Ashe tax rate = 2.0 + 4.75 = 6.75
            Assert.AreEqual(6.75, TaxContext.TotalTaxRate(County.ASHE, new DateTime(2012, 10, 28)));
            //On Dec 31, 2011, Halifax tax rate = 2.0 + 4.75 = 6.75
            Assert.AreEqual(6.75, TaxContext.TotalTaxRate(County.HALIFAX, new DateTime(2011, 12, 31)));
            //On Jan 1, 2012, Halifax tax rate = 2.25 + 4.75 = 7.0
            Assert.AreEqual(7.0, TaxContext.TotalTaxRate(County.HALIFAX, new DateTime(2012, 1, 1)));
            //On Mar 31, 2012, Durham tax rate = 2.0 + 4.75 = 6.75
            Assert.AreEqual(6.75, TaxContext.TotalTaxRate(County.DURHAM, new DateTime(2012, 3, 31)));
            //On Apr 1, 2012, Durham tax rate = 2.25 + 4.75 = 7.0
            Assert.AreEqual(7.0, TaxContext.TotalTaxRate(County.DURHAM, new DateTime(2012, 4, 1)));
            //On Feb 2, 2012, Halifax tax rate = 2.25 + 4.75 = 7.0
            Assert.AreEqual(7.0, TaxContext.TotalTaxRate(County.HALIFAX, new DateTime(2012, 2, 19)));
            //On Apr 1, 2012, Mecklenburg tax rate = 2.0 + 4.75 + .5 = 7.25
            Assert.AreEqual(7.25, TaxContext.TotalTaxRate(County.MECKLENBURG, new DateTime(2012, 4, 1)));
        }

        [TestMethod]
        public void TestCountyTaxRateWithOutOfRangeCounty()
        {
            try
            {
                Assert.AreEqual(2.0, TaxContext.CountyTaxRate(County.ASHE + 1209012, new DateTime(2012, 10, 28)));
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCountyTaxRateWithOutOfRangeDate()
        {
            try
            {
                Assert.AreEqual(2.0, TaxContext.CountyTaxRate(County.ASHE, new DateTime(2000, 10, 28)));
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
