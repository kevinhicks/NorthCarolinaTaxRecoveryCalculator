using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Tests.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Security
{
    [TestClass]
    public class AuthenticationTest
    {
        [TestMethod]
        public void TestProjectIsOwnedBySomeone()
        {
            UserProfile firstUser = TestProjectsData.GetInstance().FirstUserToTestSecurity;
            UserProfile secondUser = TestProjectsData.GetInstance().SecondUserToTestSecurity;

            Project project = TestProjectsData.GetInstance().FirstProjectToTestSecurity;

            Assert.IsTrue(project.BelongsTo(firstUser.UserId));
            Assert.IsFalse(project.BelongsTo(secondUser.UserId));
        }

        [TestMethod]
        public void TestProjectCanBeSharedWithSomeone()
        {
            UserProfile firstUser = TestProjectsData.GetInstance().FirstUserToTestSecurity;
            UserProfile secondUser = TestProjectsData.GetInstance().SecondUserToTestSecurity;

            Project project = TestProjectsData.GetInstance().FirstProjectToTestSecurity;

            Assert.IsTrue(project.BelongsTo(firstUser.UserId));
            Assert.IsFalse(project.BelongsTo(secondUser.UserId));
        }

        [TestMethod]
        public void TestCountyTaxPortion()
        {

        }
            
    }
}
