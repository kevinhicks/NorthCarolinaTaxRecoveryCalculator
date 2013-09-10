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

            return voucher;
        }

        [TestMethod]
        public void ProjectRepository_Create_ShouldSaveANewVoucherInTheDB()
        {
            var voucher = createTestVoucher();
            var vouchers = new PaymentVoucherRepository();

            vouchers.Create(voucher);
        }

        [TestMethod]
        public void ProjectRepository_Get_ShouldReturnAVoucherWithEntries()
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

    }
}
