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
    public class RecieptModelsTest
    {
        [TestMethod]
        public void TestStateTaxPortion()
        {
            //A recipet from durham on the 31st of march should return 351.85
            Reciept reciept = new Reciept();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(351.85, reciept.StateTaxPortion());

            //A recipet from durham on the 1st of april(1 day later) should return 336.29
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(339.29, reciept.StateTaxPortion());
        
        }

        [TestMethod]
        public void TestCountyTaxPortion()
        {
            //A recipet from durham on the 31st of march should return 351.85
            Reciept reciept = new Reciept();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(148.15, reciept.CountyTaxPortion());

            //A recipet from durham on the 1st of april(1 day later) should return 336.29
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(160.71, reciept.CountyTaxPortion());
        }

        [TestMethod]
        public void TestTransitTaxPortion()
        {
            //A recipet from Mecklenburg on the 31st of march should return 34.48
            Reciept reciept = new Reciept();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(34.48, reciept.TransitTaxPortion());

            //A recipet from Mecklenburg on the 1st of april(1 day later) should STILL return 34.48
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(34.48, reciept.TransitTaxPortion());

            //A recipet from Durham on the 1st of april should return 0
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(0, reciept.TransitTaxPortion());
        }

        [TestMethod]
        public void TestTotalTaxPortions()
        {
            //The total portions of tax shoud be the same as the sales tax paid
            Reciept reciept = new Reciept();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion());

            //The total portions of tax shoud be the same as the sales tax paid
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.DURHAM;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion());

            //The total portions of tax shoud be the same as the sales tax paid
            reciept = new Reciept();
            reciept.DateOfSale = new DateTime(2012, 3, 21);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion());

            //The total portions of tax shoud be the same as the sales tax paid
            reciept.DateOfSale = new DateTime(2012, 4, 1);
            reciept.County = County.MECKLENBURG;
            reciept.SalesTax = 500;
            Assert.AreEqual(reciept.SalesTax, reciept.StateTaxPortion() +
                                              reciept.CountyTaxPortion() +
                                              reciept.TransitTaxPortion());

        }            
    }
}
