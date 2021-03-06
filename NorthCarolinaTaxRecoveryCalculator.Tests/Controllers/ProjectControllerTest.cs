﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Security;
using FakeItEasy;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.Project;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Tests.Models.Mocks;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Controllers
{ 
    [TestClass]
    public class ProjectControllerTest
    {
        private IUserRepository validUser;
        private IUserRepository invalidUser;
        private IProjectRepository projectRepository;

        private ProjectController controller;

        public ProjectControllerTest() 
        {
            validUser = A.Fake<IUserRepository>();
            A.CallTo(() => validUser.CurrentUserId).Returns(1);

            invalidUser = A.Fake<IUserRepository>();
            A.CallTo(() => invalidUser.CurrentUserId).Returns(2);

            projectRepository = new MockProjectRepository();
            controller = new ProjectController(validUser, null, projectRepository);

            //Create some test data
            //User 1 creates a project
            var firstProject = new Project();
            firstProject.Name = "Owned Project";
            firstProject.OwnerID = validUser.CurrentUserId;
            projectRepository.Create(firstProject);

            //User 2 creates a project
            var secondProject = new Project();
            secondProject.Name = "Shared project";
            secondProject.OwnerID = invalidUser.CurrentUserId;
            projectRepository.Create(secondProject);

            //User 2 invites user 1 to the project
            var acl = projectRepository.CreateCollaboration(secondProject.ID, "test@test.com");

            //User 1 accepts
            projectRepository.AcceptCollaboration(acl.ID, validUser.CurrentUserId);
        }

        [TestMethod]
        public void ProjectController_Index_ShouldReturnOwnedAndSharedProjects()
        {
            var result = controller.Index() as ViewResult;
            var model = result.Model as OwnedAndSharedProjectViewModels;

            Assert.AreEqual(1, model.MyProjects.Count());
            Assert.AreEqual(1, model.SharedProjects.Count());
        }

        //TODO: Test the rest of the actions
    }
}
