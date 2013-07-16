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
    public class RecieptModelsTest
    {/*
        [TestMethod]
        public void TestStateTaxPortion()
        {
            //A recipet from durham on the 31st of march should return 351.85
            RecieptEntity reciept = new RecieptEntity();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(351.85, Math.Round(reciept.StateTaxPortion(), 2), .001);

            //A recipet from durham on the 1st of april(1 day later) should return 336.29
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(339.29, Math.Round(reciept.StateTaxPortion(), 2), .001);

        }

        [TestMethod]
        public void TestCountyTaxPortion()
        {
            //A recipet from durham on the 31st of march should return 351.85
            RecieptEntity reciept = new RecieptEntity();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(148.15, Math.Round(reciept.CountyTaxPortion(), 2), .001);

            //A recipet from durham on the 1st of april(1 day later) should return 336.29
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(160.71, Math.Round(reciept.CountyTaxPortion(), 2), .001);
        }

        [TestMethod]
        public void TestTransitTaxPortion()
        {
            //A recipet from Mecklenburg on the 31st of march should return 34.48
            RecieptEntity reciept = new RecieptEntity();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(34.48, Math.Round(reciept.TransitTaxPortion(), 2), .001);

            //A recipet from Mecklenburg on the 1st of april(1 day later) should STILL return 34.48
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(34.48, Math.Round(reciept.TransitTaxPortion(), 2), .001);

            //A recipet from Durham on the 1st of april should return 0
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(0, Math.Round(reciept.TransitTaxPortion(), 2), .001);
        }

        [TestMethod]
        public void TestTotalTaxPortions()
        {
            //The total portions of tax shoud be the same as the sales tax paid
            RecieptEntity reciept = new RecieptEntity();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion(), .001);

            //The total portions of tax shoud be the same as the sales tax paid
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion(), .001);

            //The total portions of tax shoud be the same as the sales tax paid
            reciept = new RecieptEntity();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion(), .001);

            //The total portions of tax shoud be the same as the sales tax paid
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion(), .001);

        }*/
        /*
        [TestMethod]
        public void TestGetTaxPeriod()
        {
            RecieptEntity reciept = new RecieptEntity();

            reciept.DateOfSale = new DateTime(2012, 4, 2);
            Assert.AreEqual(0, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2012, 4, 1);
            Assert.AreEqual(0, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2012, 3, 31);
            Assert.AreEqual(1, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2012, 1, 1);
            Assert.AreEqual(1, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2011, 12, 31);
            Assert.AreEqual(2, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2011, 10, 1);
            Assert.AreEqual(2, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2011, 9, 30);
            Assert.AreEqual(3, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2011, 1, 1);
            Assert.AreEqual(3, reciept.GetTaxPeriod());

            reciept.DateOfSale = new DateTime(2010, 1, 1);
            try
            {
                Assert.AreEqual(3, reciept.GetTaxPeriod());
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("We do not calculate tax on a recipet so old.", e.Message);
            }

        }
        
        [TestMethod]
        public void TestNonTaxableItemsShouldNotBeIncludedInTheTotal()
        {
            Project project = TestProjectsData.GetInstance().ProjectToTestNonTaxable;

            Assert.AreEqual(0, project.GetTotalStateTax(TestProjectsData.GetInstance().Reciepts));
            Assert.AreEqual(0, project.GetTotalCountyTax(TestProjectsData.GetInstance().Reciepts));
            Assert.AreEqual(0, project.GetTotalTransitTax(TestProjectsData.GetInstance().Reciepts));
        }*/
    }
}
