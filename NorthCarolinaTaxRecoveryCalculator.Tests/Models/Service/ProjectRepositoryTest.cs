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

    }
}
