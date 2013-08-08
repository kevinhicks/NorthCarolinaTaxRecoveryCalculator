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
using NorthCarolinaTaxRecoveryCalculator.Misc;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Misc
{
    [TestClass]
    public class MiscTest
    {
        [TestMethod]
        public void TestPdf()
        {
            var project = new Project();
            project.Name = "Test";

            var voucher = new PaymentVoucher();
            voucher.Project = project;
            voucher.CheckNumber = "123";
            voucher.PaidTo = "Kevin";

            var entry = new PaymentVoucherEntry();
            entry.Item = "someting";
            entry.CostElement = "big";
            entry.Amount = 234.23;
            voucher.Entries.Add(entry); 
            entry = new PaymentVoucherEntry();
            entry.Item = "someting else";
            entry.CostElement = "not so big";
            entry.Amount = .01;
            voucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "that thing";
            entry.CostElement = "small";
            entry.Amount = 2304990324.3;
            voucher.Entries.Add(entry);


            new PaymentVoucherReportGenerator().GeneratePDFForVoucher(voucher);           
            
        }    
    }
}
