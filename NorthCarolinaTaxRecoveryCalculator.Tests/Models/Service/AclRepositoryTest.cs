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
using FakeItEasy;
using NorthCarolinaTaxRecoveryCalculator.Misc;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class AclRepositoryTest
    {
        private UserProfile user;
        public AclRepositoryTest()
        {
            user = new UserProfile();
            user.UserId = 1;
            user.UserName = "test user";
        }

        private Project createTestProject(int OwnerID)
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test" + new Random().NextDouble();
            project.OwnerID = OwnerID;

            return project;
        }

        /// <summary>
        /// removes existing proejcts from teh database so we can count the projects correctly
        /// </summary>
        /// <param name="userID"></param>
        private void removeExistingProjectsFromUser(int userID)
        {
            var acl = new AclRepository();
            var ownedProjects = acl.FindProjectsOwnedByUser(userID);

            var projects = new ProjectRepository();
            foreach (var project in ownedProjects)
            {
                var foundProject = projects.FindProjectByID(project.ID);
                projects.Delete(foundProject);
            }
        }

        [TestMethod]
        public void AclRepository_FindProjectsOwnedByUser_ShouldReturnAllOwedProjectsOfUser()
        {
            removeExistingProjectsFromUser(1);
            removeExistingProjectsFromUser(2);

            var project = new ProjectRepository();
            project.Create(createTestProject(1));
            project.Create(createTestProject(2));
            project.Create(createTestProject(1));

            var acl = new AclRepository();
            
            var ownedProjects = acl.FindProjectsOwnedByUser(1);
            Assert.AreEqual(2, ownedProjects.Count());
            
            ownedProjects = acl.FindProjectsOwnedByUser(2);
            Assert.AreEqual(1, ownedProjects.Count());
        }

        [TestMethod]
        public void AclRepository_CreateCollaboration_ShouldCreateNewCollaborationInDB()
        {
            var acl = new AclRepository();
            var mockProjectID = Guid.NewGuid();

            var invitation = acl.CreateCollaboration(mockProjectID, "test@test.com");

            Assert.AreEqual(mockProjectID, invitation.ProjectID);
            Assert.AreEqual("test@test.com", invitation.Email);
        }

        [TestMethod]
        public void AclRepository_SendInvitation_ShoudltryToSendAnEmailWithInvitaion()
        {
            var acl = new AclRepository();
            var mockProjectID = Guid.NewGuid();
            var mockEmailSender = A.Fake<IEmailSender>();
            var invitation = acl.CreateCollaboration(mockProjectID, "test@test.com");

            A.CallTo(() => mockEmailSender.SendMail("", "", "")).WithAnyArguments().MustNotHaveHappened();
            acl.SendInvitation(invitation, mockEmailSender);
            A.CallTo(() => mockEmailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened();

            Assert.AreEqual(mockProjectID, invitation.ProjectID);
            Assert.AreEqual("test@test.com", invitation.Email);
        }
    }
}
