using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Security;
using FakeItEasy;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Controllers
{ 
    [TestClass]
    public class ProjectControllerTest
    {
        private IUserRepository validUser;
        private IUserRepository invalidUser;

        private ProjectController controller;

        public ProjectControllerTest() 
        {
            validUser = A.Fake<IUserRepository>();
            A.CallTo(() => validUser.CurrentUserId).Returns(1);

            invalidUser = A.Fake<IUserRepository>();
            A.CallTo(() => invalidUser.CurrentUserId).Returns(2);

            controller = new ProjectController(validUser, null);
        }

        [TestMethod]
        public void SendInvitation()
        {
            controller.Index();
            
            /*
            // Arrange
            ProjectController controller = new ProjectController(null);

            // Act
            ViewResult result = controller.SendInvitation() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
          * */
        }
    }
}
