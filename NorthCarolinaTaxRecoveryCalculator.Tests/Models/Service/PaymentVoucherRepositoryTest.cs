using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class PaymentVoucherRepositoryTest
    {
        private UserProfile user;
        private Project project;

        public PaymentVoucherRepositoryTest()
        {
            user = new UserProfile();
            user.UserId = 1;
            user.UserName = "test user";

            project = createTestProject(user.UserId);
            var projects = new ProjectRepository();
            projects.Create(project);
        }

        private Project createTestProject(int OwnerID)
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test" + new Random().NextDouble();
            project.OwnerID = OwnerID;

            return project;
        }

        private PaymentVoucher createTestVoucher()
        {
            var voucher = new PaymentVoucher();
            voucher.ApprovedBy = "321";
            voucher.CheckNumber = "123";
            voucher.Date = DateTime.Now;
            voucher.PaidTo = "321";
            voucher.ProjectID = project.ID;
            voucher.RBCApproval = "321";
            voucher.TaxCostElement = "blah";

            return voucher;
        }

        private PaymentVoucherEntry createTestVoucherEntry(Guid PaymentVoucherID)
        {
            var entry = new PaymentVoucherEntry();
            entry.Amount = 123;
            entry.CostElement = "123";
            entry.Item = "stuff";
            entry.PaymentVoucherID = PaymentVoucherID;

            return entry;
        }

        [TestMethod]
        public void PaymentVoucherRepository_Create_ShouldSaveANewVoucherInTheDB()
        {
            var voucher = createTestVoucher();
            var vouchers = new PaymentVoucherRepository();

            vouchers.Create(voucher);
        }

        [TestMethod]
        public void PaymentVoucherRepository_Get_ShouldReturnAVoucherWithEntries()
        {
            var voucher = createTestVoucher();
            var vouchers = new PaymentVoucherRepository();

            vouchers.Create(voucher);

            var foundVoucher = vouchers.Get(voucher.ID);
            Assert.IsNotNull(foundVoucher);
            Assert.AreEqual(voucher.ApprovedBy, foundVoucher.ApprovedBy);
            Assert.AreEqual(voucher.CheckNumber, foundVoucher.CheckNumber);
            Assert.AreEqual(voucher.Date, foundVoucher.Date);
            Assert.AreEqual(voucher.PaidTo, foundVoucher.PaidTo);
            Assert.AreEqual(voucher.PreparedBy, foundVoucher.PreparedBy);
            Assert.AreEqual(voucher.ProjectID, foundVoucher.ProjectID);
            Assert.AreEqual(voucher.RBCApproval, foundVoucher.RBCApproval);

            Assert.IsNotNull(foundVoucher.Entries);
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, foundVoucher.Entries.Count);
        }

        public void PaymentVoucherRepository_Create_ShouldSaveEntries()
        {
            //Create test voucher with a few entries
            var voucher = createTestVoucher();
            var vouchers = new PaymentVoucherRepository();

            var entries = new List<PaymentVoucherEntry>();
            for (int i = 0; i < 10; i++)
            {
                var entry = createTestVoucherEntry(voucher.ID);
                entries.Add(entry);
            }

            //save it
            voucher.Entries = entries;
            vouchers.Create(voucher);

            //query it back out
            var newFoundVoucher = vouchers.Get(voucher.ID);

            Assert.IsNotNull(newFoundVoucher.Entries);
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, newFoundVoucher.Entries.Count);            
        }


        [TestMethod]
        public void PaymentVoucherRepository_Update_ShouldSaveNewEntries()
        {
            //Create test voucher
            var voucher = createTestVoucher();
            var vouchers = new PaymentVoucherRepository();

            //Create a few entries
            var entries = new List<PaymentVoucherEntry>();
            for (int i = 0; i < 10; i++)
            {
                var entry = createTestVoucherEntry(voucher.ID);
                entries.Add(entry);
            }

            //save it
            voucher.Entries = entries;
            vouchers.Create(voucher);

            //query it back out
            var newFoundVoucher = vouchers.Get(voucher.ID);

            //Create new entries
            entries = new List<PaymentVoucherEntry>();
            for (int i = 0; i < 10; i++)
            {
                var entry = createTestVoucherEntry(newFoundVoucher.ID);
                entries.Add(entry);
            }

            //save it
            newFoundVoucher.Entries = entries;
            vouchers.Update(newFoundVoucher);

            //query it back out. again to check for Updates
            newFoundVoucher = vouchers.Get(voucher.ID);

            Assert.IsNotNull(newFoundVoucher);
            Assert.IsNotNull(newFoundVoucher.Entries);
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, newFoundVoucher.Entries.Count);    
        }
/*
        [TestMethod]
        public void PaymentVoucher_Update_ShouldOverwriteEntriesWhenUpdating()
        {
            var vouchers = new PaymentVoucherRepository();           
            var voucher = createTestVoucher();

            voucher.Entries =  new List<PaymentVoucherEntry>();
            for(int i = 0; i < 10; i++)
            {
                voucher.Entries.Add(createTestVoucherEntry(voucher.ID));
            }

            //save a refernce to an origianl entry
            var originalEntry = voucher.Entries[0];            

            //save it
            vouchers.Create(voucher);

            var newVoucher = vouchers.Get(voucher.ID);
            CollectionAssert.Contains(newVoucher.Entries, originalEntry);

            voucher.Entries[0].Amount = 12030;
            vouchers.Update(voucher);

            CollectionAssert.DoesNotContain(newVoucher.Entries, originalEntry);
            CollectionAssert.AllItemsAreUnique(newVoucher.Entries);
            Assert.AreEqual(PaymentVoucher.NumberOfEntriesInAVoucher, newVoucher.Entries.Count);
        }    */ 

    }
}
