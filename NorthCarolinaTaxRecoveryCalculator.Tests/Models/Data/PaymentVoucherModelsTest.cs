using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class PaymentVoucherEntryTest
    {
        [TestMethod]
        public void TestBankEntriesShouldReturnTrueIfBlank()
        {
            var entry = new PaymentVoucherEntry();
            Assert.IsTrue(entry.IsBlankEntry());

            entry.Item = null;
            entry.CostElement = null;
            Assert.IsTrue(entry.IsBlankEntry());

            entry.Item = "";
            entry.CostElement = "";
            Assert.IsTrue(entry.IsBlankEntry());

            entry.Amount = 0;
            Assert.IsTrue(entry.IsBlankEntry());

            entry.Item = "123";
            entry.CostElement = "123";
            entry.Amount = 123;
            Assert.IsFalse(entry.IsBlankEntry());
        }    
    }
}
