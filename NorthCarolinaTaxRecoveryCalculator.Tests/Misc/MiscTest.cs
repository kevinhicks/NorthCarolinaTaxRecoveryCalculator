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
    public class PaymentVoucherReportGeneratorTest
    {
        private PaymentVoucher Voucher = null;

        [TestInitialize]
        public void InitVoucher()
        {        
            var project = new Project();
            project.Name = "Test";

            Voucher = new PaymentVoucher();
            Voucher.Project = project;
            Voucher.CheckNumber = "123";
            Voucher.PaidTo = "Kevin";

            var entry = new PaymentVoucherEntry();
            entry.Item = "someting";
            entry.CostElement = "big";
            entry.Amount = 234.23;
            Voucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "someting else";
            entry.CostElement = "not so big";
            entry.Amount = .01;
            Voucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "that thing";
            entry.CostElement = "small";
            entry.Amount = 2304990324.3;
            Voucher.Entries.Add(entry);
        }

        [TestMethod]
        public void TestGeneratePDFForVoucherShouldNotReturnNull()
        {
            var result = new PaymentVoucherReportGenerator().GeneratePDFForVoucher(Voucher);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGeneratePDFForVoucherShouldReturnAValidPDF()
        {
            var result = new PaymentVoucherReportGenerator().GeneratePDFForVoucher(Voucher);

            try
            {
                new iTextSharp.text.pdf.PdfReader(result);
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                Assert.Fail("Did not generate valid PDF Document");
            }
        }    
    }
}
