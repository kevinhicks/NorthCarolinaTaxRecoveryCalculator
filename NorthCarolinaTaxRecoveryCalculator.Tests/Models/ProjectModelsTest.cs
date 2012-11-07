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
    public class ProjectModelsTest
    {
        [TestMethod]
        public void TestFindCountyTaxForAllReciepts()
        {
            Project project = TestProjectsData.GetInstance().Projects[2];

            int countyIndex = County.DURHAM;
            var reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex);

            Assert.AreEqual(121.03, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);
        }

        [TestMethod]
        public void TestFindRecieptsByTaxPeriods()
        {
            Project project = TestProjectsData.GetInstance().Projects[2];

            int countyIndex = County.DURHAM;
            var reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex &&
                                                                                rec.GetTaxPeriod() == 0);
            Assert.AreEqual(32.14, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex &&
                                                                                rec.GetTaxPeriod() == 1);
            Assert.AreEqual(29.63, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex &&
                                                                                rec.GetTaxPeriod() == 2);
            Assert.AreEqual(29.63, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.County == countyIndex &&
                                                                                rec.GetTaxPeriod() == 3);
            Assert.AreEqual(29.63, Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);
        }

        [TestMethod]
        public void TestAllRecieptsInAllTaxPeriodsForCountyTax()
        {
            Project project = TestProjectsData.GetInstance().Projects[3];
            double expectedTotalCountyTax = 0;

            var reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.ProjectID == project.ID);
            Assert.AreEqual(707, reciepts.Count());

            /* Periods Beginning January 1, 2011 through September 30, 2011
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Catawba, Cumberland, Duplin, Haywood, Hertford, 
             * Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, Rowan, 
             * Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One After
            expectedTotalCountyTax += (82 * 296.2962963);  //82 counties at 2%
            expectedTotalCountyTax += (17 * 321.4285714);  //17 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //11 counties at 2.25% with transit tax

            /* Periods Beginning October 1, 2011 through December 31, 2011
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Cabarrus, Catawba, Cumberland, Duplin, Haywood, 
             * Hertford, Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, Rowan, 
             * Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalCountyTax += (82 * 296.2962963);  //82 counties at 2%
            expectedTotalCountyTax += (17 * 321.4285714);  //17 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[2]);
            Assert.AreEqual(Math.Round(expectedTotalCountyTax, 2), Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            //One After
            expectedTotalCountyTax += (81 * 296.2962963);  //81 counties at 2%
            expectedTotalCountyTax += (18 * 321.4285714);  //18 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax


            /* Periods Beginning January 1, 2012 through March 31, 2012
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Cabarrus, Catawba, Cumberland, Duplin, Halifax, 
             * Haywood, Hertford, Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, 
             * Rowan, Sampson, Surry, and Wilkes
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalCountyTax += (81 * 296.2962963);  //81 counties at 2%
            expectedTotalCountyTax += (18 * 321.4285714);  //18 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[1]);
            Assert.AreEqual(Math.Round(expectedTotalCountyTax, 2), Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            //One After
            expectedTotalCountyTax += (80 * 296.2962963);  //80 counties at 2%
            expectedTotalCountyTax += (19 * 321.4285714);  //19 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax

            /* Periods Beginning April 1, 2012
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Buncombe, Cabarrus, Catawba, Cumberland, Duplin, Durham, Halifax, 
             * Haywood, Hertford, Lee, Martin, Montgomery, New Hanover, Onslow, Orange, 
             * Pitt, Randolph, Robeson, Rowan, Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalCountyTax += (80 * 296.2962963);  //80 counties at 2%
            expectedTotalCountyTax += (19 * 321.4285714);  //19 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[0]);
            Assert.AreEqual(Math.Round(expectedTotalCountyTax, 2), Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);

            //One After
            expectedTotalCountyTax += (76 * 296.2962963);  //76 counties at 2%
            expectedTotalCountyTax += (23 * 321.4285714);  //23 counties at 2.25%
            expectedTotalCountyTax += (1 * 275.862069);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts;
            Assert.AreEqual(Math.Round(expectedTotalCountyTax, 2), Math.Round(project.GetTotalCountyTax(reciepts), 2), .001);
        }

        [TestMethod]
        public void TestAllRecieptsInAllTaxPeriodsForStateTax()
        {
            Project project = TestProjectsData.GetInstance().Projects[3];
            double expectedTotalStateTax = 0;

            var reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.ProjectID == project.ID);
            Assert.AreEqual(707, reciepts.Count());

            /* Periods Beginning January 1, 2011 through September 30, 2011
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Catawba, Cumberland, Duplin, Haywood, Hertford, 
             * Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, Rowan, 
             * Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One After
            expectedTotalStateTax += (82 * 703.7037037);  //82 counties at 2%
            expectedTotalStateTax += (17 * 678.5714286);  //17 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //11 counties at 2.25% with transit tax

            /* Periods Beginning October 1, 2011 through December 31, 2011
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Cabarrus, Catawba, Cumberland, Duplin, Haywood, 
             * Hertford, Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, Rowan, 
             * Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalStateTax += (82 * 703.7037037);  //82 counties at 2%
            expectedTotalStateTax += (17 * 678.5714286);  //17 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[2]);
            Assert.AreEqual(Math.Round(expectedTotalStateTax, 2), Math.Round(project.GetTotalStateTax(reciepts), 2), .001);

            //One After
            expectedTotalStateTax += (81 * 703.7037037);  //81 counties at 2%
            expectedTotalStateTax += (18 * 678.5714286);  //18 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax


            /* Periods Beginning January 1, 2012 through March 31, 2012
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Cabarrus, Catawba, Cumberland, Duplin, Halifax, 
             * Haywood, Hertford, Lee, Martin, New Hanover, Onslow, Pitt, Randolph, Robeson, 
             * Rowan, Sampson, Surry, and Wilkes
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalStateTax += (81 * 703.7037037);  //81 counties at 2%
            expectedTotalStateTax += (18 * 678.5714286);  //18 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[1]);
            Assert.AreEqual(Math.Round(expectedTotalStateTax, 2), Math.Round(project.GetTotalStateTax(reciepts), 2), .001);

            //One After
            expectedTotalStateTax += (80 * 703.7037037);  //80 counties at 2%
            expectedTotalStateTax += (19 * 678.5714286);  //19 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax

            /* Periods Beginning April 1, 2012
             * All counties are subject to the 2.0% sales and use tax rate except:
             * Alexander, Buncombe, Cabarrus, Catawba, Cumberland, Duplin, Durham, Halifax, 
             * Haywood, Hertford, Lee, Martin, Montgomery, New Hanover, Onslow, Orange, 
             * Pitt, Randolph, Robeson, Rowan, Sampson, Surry, and Wilkes 
             * which are subject to the 2.25% sales and use tax rate.
             */

            //One Before
            expectedTotalStateTax += (80 * 703.7037037);  //80 counties at 2%
            expectedTotalStateTax += (19 * 678.5714286);  //19 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts.Where(rec => rec.DateOfSale < TaxContext.TaxPeriods[0]);
            Assert.AreEqual(Math.Round(expectedTotalStateTax, 2), Math.Round(project.GetTotalStateTax(reciepts), 2), .001);

            //One After
            expectedTotalStateTax += (76 * 703.7037037);  //76 counties at 2%
            expectedTotalStateTax += (23 * 678.5714286);  //23 counties at 2.25%
            expectedTotalStateTax += (1 * 655.1724138);  //1 counties at 2.25% with transit tax

            //All the reciepts in this project, that are BEFORE the second oldest tax period
            reciepts = TestProjectsData.GetInstance().Reciepts;
            Assert.AreEqual(Math.Round(expectedTotalStateTax, 2), Math.Round(project.GetTotalStateTax(reciepts), 2), .001);
        }
    }
}
