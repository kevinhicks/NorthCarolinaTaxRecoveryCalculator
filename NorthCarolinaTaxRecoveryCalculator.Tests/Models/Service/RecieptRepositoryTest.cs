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
    public class RecieptRepositoryTest
    {
        
        private Project project;

        public RecieptRepositoryTest()
        {
            project = createTestProject(1);
            var projects = new ProjectRepository();
            projects.Create(project);
        }

        [TestMethod]
        public void RecieptRepository_FindRecieptByID_ShouldReturnNullForInvalidRecieptID()
        {
            var recieptRepository = new RecieptRepository();
            Assert.IsNull(recieptRepository.FindRecieptByID(Guid.NewGuid()));
        }

        [TestMethod]
        public void RecieptRepository_Create_ShouldAddANewRecieptToTheDB()
        {
            var recieptRepository = new RecieptRepository();
            var reciept = createTestReciept();           
            recieptRepository.Create(reciept);
        }
        
        /*
        [TestMethod]
        public void RecieptRepository_Update_ShouldAddANewRecieptToTheDB()
        {
            
        }
        */

        private Project createTestProject(int OwnerID)
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test" + new Random().NextDouble();
            project.OwnerID = OwnerID;

            return project;
        }

        private RecieptEntity createTestReciept()
        {
            var reciept = new RecieptEntity();
            reciept.County = County.ALAMANCE;
            reciept.DateOfSale = DateTime.Now;
            reciept.RIF = Guid.NewGuid().ToString();
            reciept.StoreName = "Store";
            reciept.ProjectID = project.ID;
            
            return reciept;
        }
    }
}
