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
using NorthCarolinaTaxRecoveryCalculator.Misc;
using FakeItEasy;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class ProjectRepositoryTest
    {
        private UserProfile user;
        public ProjectRepositoryTest()
        {
            user = new UserProfile();
            user.UserId = 1;
            user.UserName = "test user";
        }

        [TestMethod]
        public void ProjectRepository_Create_ShouldCreateNewProjectInDB()
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test";
            project.Owner = user;
            project.OwnerID = user.UserId;

            var repo = new ProjectRepository();

            repo.Create(project);
        }

        [TestMethod]
        public void ProjectRepository_Find_ShouldReturnOneProjectFromTheDB_IfItExists()
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test";
            project.Owner = user;
            project.OwnerID = user.UserId;

            var repo = new ProjectRepository();
            repo.Create(project);

            var foundProject = repo.FindProjectByID(project.ID);

            Assert.IsNotNull(foundProject);
            Assert.AreEqual(project.DateStarted, foundProject.DateStarted);
            Assert.AreEqual(project.Name, foundProject.Name);
            Assert.AreEqual(project.OwnerID, foundProject.OwnerID);
        }

        [TestMethod]
        public void ProjectRepository_Find_ShouldReturnNull_IftheProjectDoesntExist()
        {
            var repo = new ProjectRepository();
            var foundProject = repo.FindProjectByID(Guid.NewGuid());

            Assert.IsNull(foundProject);
        }

        [TestMethod]
        public void ProjectRepository_Edit_ShouldUpdateTheDB()
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test";
            project.Owner = user;
            project.OwnerID = user.UserId;

            var repo = new ProjectRepository();
            repo.Create(project);

            var foundProject = repo.FindProjectByID(project.ID);

            Assert.IsNotNull(foundProject);
            Assert.AreEqual(project.DateStarted, foundProject.DateStarted);
            Assert.AreEqual(project.Name, foundProject.Name);
            Assert.AreEqual(project.OwnerID, foundProject.OwnerID);

            foundProject.Name = "other";

            repo.Update(project);

            var newFoundProject = repo.FindProjectByID(foundProject.ID);
            Assert.IsNotNull(newFoundProject);
            Assert.AreEqual(foundProject.DateStarted, newFoundProject.DateStarted);
            Assert.AreEqual(foundProject.Name, newFoundProject.Name);
            Assert.AreEqual(foundProject.OwnerID, newFoundProject.OwnerID);                 
        }

        [TestMethod]
        public void ProjectRepository_Delete_ShouldRemoveAProjectFromTheDB()
        {
            var project = new Project();
            project.DateStarted = DateTime.Now;
            project.Name = "test";
            project.Owner = user;
            project.OwnerID = user.UserId;

            var repo = new ProjectRepository();
            repo.Create(project);

            Assert.IsNotNull(repo.FindProjectByID(project.ID));

            repo.Delete(project);
            Assert.IsNull(repo.FindProjectByID(project.ID));
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
            var projects = new ProjectRepository();
            var ownedProjects = projects.FindProjectsOwnedByUser(userID);
            foreach (var project in ownedProjects)
            {
                var foundProject = projects.FindProjectByID(project.ID);
                projects.Delete(foundProject);
            }
        }

        [TestMethod]
        public void ProjectRepository_FindProjectsOwnedByUser_ShouldReturnAllOwedProjectsOfUser()
        {
            removeExistingProjectsFromUser(1);
            removeExistingProjectsFromUser(2);

            var project = new ProjectRepository();
            project.Create(createTestProject(1));
            project.Create(createTestProject(2));
            project.Create(createTestProject(1));
            
            var ownedProjects = project.FindProjectsOwnedByUser(1);
            Assert.AreEqual(2, ownedProjects.Count());

            ownedProjects = project.FindProjectsOwnedByUser(2);
            Assert.AreEqual(1, ownedProjects.Count());
        }

        [TestMethod]
        public void ProjectRepository_CreateCollaboration_ShouldCreateNewCollaborationInDB()
        {
            var projects = new ProjectRepository();
            var mockProjectID = Guid.NewGuid();

            var invitation = projects.CreateCollaboration(mockProjectID, "test@test.com");

            Assert.AreEqual(mockProjectID, invitation.ProjectID);
            Assert.AreEqual("test@test.com", invitation.Email);
        }

        [TestMethod]
        public void ProjectRepository_SendInvitation_ShoudltryToSendAnEmailWithInvitaion()
        {
            var projects = new ProjectRepository();
            var mockProjectID = Guid.NewGuid();
            var mockEmailSender = A.Fake<IEmailSender>();
            var invitation = projects.CreateCollaboration(mockProjectID, "test@test.com");

            A.CallTo(() => mockEmailSender.SendMail("", "", "")).WithAnyArguments().MustNotHaveHappened();
            projects.SendInvitation(invitation, mockEmailSender);
            A.CallTo(() => mockEmailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened();

            Assert.AreEqual(mockProjectID, invitation.ProjectID);
            Assert.AreEqual("test@test.com", invitation.Email);
        }

    }
}
