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
    public class TaxPeriodsTest
    {
        [TestMethod]
        public void TaxPeriods_ShouldLoadInformationByFirstCall()
        {
            Assert.IsNotNull(TaxPeriods.Periods);
        }

        [TestMethod]
        public void TaxPeriods_EveryTaxPeriodShouldHave100Counties()
        {
            foreach (var period in TaxPeriods.Periods)
            {
                Assert.AreEqual(100, period.CountyRates.Count());
            }
        }

        [TestMethod]
        public void TaxPeriods_EveryCountyInATaxPeriodShouldBeUnique()
        {
            foreach (var period in TaxPeriods.Periods)
            {
                //Get an array of all the names
                string[] names = new string[period.CountyRates.Count()];
                for (int i = 0; i < period.CountyRates.Count(); i++)
                {
                    names[i] = period.CountyRates[i].Name;
                }

                //They should all be unique
                CollectionAssert.AllItemsAreUnique(names);
            }
        }

        [TestMethod]
        public void TaxPeriods_TaxPeriodsShouldBeOrderedFromNewestToOldest()
        {
            DateTime last = DateTime.MaxValue;

            foreach (var period in TaxPeriods.Periods)
            {
                Console.WriteLine(period.StartOfPeriod);

                Assert.IsTrue(period.StartOfPeriod.CompareTo(last) < 0);
                last = period.StartOfPeriod;
            }
        }

        [TestMethod]
        public void TaxPeriods_GetPeriodByDate_ShouldReturnThePeriodThatTheDateLiesIn()
        {
            var today = DateTime.Now;

            TaxPeriod period = TaxPeriods.GetPeriodByDate(DateTime.Now);
            Console.WriteLine(period.StartOfPeriod);

            //Right now should be AFTER the start of the returned tax period
            Assert.IsTrue(DateTime.Now.CompareTo(period.StartOfPeriod) > 0);
        }

        [TestMethod]
        public void TaxPeriods_GetPeriodByDate_ShouldReturnNullForReallyOldDates()
        {
            Assert.IsNull(TaxPeriods.GetPeriodByDate(DateTime.MinValue));
        }

        [TestMethod]
        public void TaxPeriods_ShouldBeAbleToPrintToString()
        {
            foreach(var period in TaxPeriods.Periods) {
                Console.WriteLine(period);
            }
        }

    }
}
