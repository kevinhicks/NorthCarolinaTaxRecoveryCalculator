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
using NorthCarolinaTaxRecoveryCalculator.Models.Service;

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

    [TestClass]
    public class PaymentVoucherTest
    {
        [TestMethod]
        public void PaymentVoucher_ShouldComeWithEntriesWhenInitialized()
        {
            var voucher = new PaymentVoucher();

            Assert.IsNotNull(voucher.Entries);
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, voucher.Entries.Count);
        }

       /* [TestMethod]
        public void PaymentVoucher_ShouldComeWithTheRequiredNumberOfEntriesAfterSettingThemWithLessThenTheRequiredNumber()
        {
            var voucher = new PaymentVoucher();

            var entries = new List<PaymentVoucherEntry>();
            //Create a list with insufficiant entries
            for (int i = 0; i < PaymentVoucher.NumberOfEntriesInAVoucher / 2; i++)
            {
                entries.Add(new PaymentVoucherEntry());
            }

            voucher.Entries = entries;
            
            //We shoudl still get the required number of entries
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, voucher.Entries.Count);
        }*/
    }
}
