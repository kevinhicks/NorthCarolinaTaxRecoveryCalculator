using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Migrations;
using System.Data.Entity;
using System.Configuration;
using System.Data;
using NorthCarolinaTaxRecoveryCalculator.Hubs;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestFixture]
    public class RecieptHubTest
    {
        ApplicationDBContext db = null;
        Project testProject = null;
        RecieptHub hub = null;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {            
            db = new ApplicationDBContext();
            db.Database.Connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=NCTaxCalcApp;Integrated Security=SSPI";

            hub = new RecieptHub();

            testProject = new Project();
            testProject.DateStarted = DateTime.Now;
            testProject.Name = "Testing Project";
            db.Projects.Add(testProject);
            db.SaveChanges();
        }

        [TearDown]
        public void Dispose()
        {
            db.Projects.Remove(testProject);
            db.SaveChanges();
        }

        #endregion

        #region Tests

        [Test]
        public void CreateRecieptTest()
        {
            //Save a reciept to the database
            Reciept reciept = new Reciept();
            reciept.ProjectID = testProject.ID;
            reciept.RIF = "1";
            reciept.SalesAmount = 30;
            reciept.DateOfSale = DateTime.Now;
            reciept.County = 1;
            reciept.StoreName = "My Secret Store";

            hub.AddReciept(reciept);

            //Get it back
            Reciept returnedReciept = new Reciept();
            returnedReciept = db.Reciepts.Where(rec => rec.RIF == reciept.RIF && rec.ProjectID == reciept.ProjectID).Single();

            //Make sure it equals what we oringinally sent
            Assert.AreEqual(reciept.ID, returnedReciept.ID);
            Assert.AreEqual(reciept.ProjectID, returnedReciept.ProjectID);
            Assert.AreEqual(reciept.RIF, returnedReciept.RIF);
            Assert.AreEqual(reciept.SalesAmount, returnedReciept.SalesAmount);
            Assert.AreEqual(reciept.DateOfSale, returnedReciept.DateOfSale);
            Assert.AreEqual(reciept.County, returnedReciept.County);
            Assert.AreEqual(reciept.StoreName, returnedReciept.StoreName);

        }

        [Test]
        public void UpdateRecieptTest()
        {
            //Save a reciept to the database
            Reciept reciept = new Reciept();
            reciept.ProjectID = testProject.ID;
            reciept.RIF = "1";
            reciept.SalesAmount = 30;
            reciept.DateOfSale = DateTime.Now;
            reciept.County = 1;
            reciept.StoreName = "My Secret Store";

            hub.AddReciept(reciept);


            string testNotes = "osduifouiashfloiuasdlfiausdbfliu uaf iouash dfiua       ouawsfoi";

            //Make some changes
            reciept.Notes = testNotes;
            reciept.SalesAmount = 300;

            //Save the changes
            hub.UpdateReciept(reciept);

            //Get it back
            Reciept returnedReciept = new Reciept();
            returnedReciept = db.Reciepts.Where(rec => rec.RIF == reciept.RIF && rec.ProjectID == reciept.ProjectID).Single();

            //Assert changes were correctly made
            Assert.AreEqual(reciept.ID, returnedReciept.ID);
            Assert.AreEqual(reciept.ProjectID, returnedReciept.ProjectID);
            Assert.AreEqual(reciept.RIF, returnedReciept.RIF);
            Assert.AreEqual(300, returnedReciept.SalesAmount);
            Assert.AreEqual(reciept.DateOfSale, returnedReciept.DateOfSale);
            Assert.AreEqual(reciept.County, returnedReciept.County);
            Assert.AreEqual(reciept.StoreName, returnedReciept.StoreName);
            Assert.AreEqual(testNotes, returnedReciept.Notes);

        }

        #endregion
    }
}
