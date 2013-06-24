using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;

namespace Data.Tests
{
    [TestClass]
    public class ProjectManagerTest
    {
        ProjectManager projectManager = null;

        [TestInitialize]
        public void TestStartUp()
        {
            projectManager = A.Fake<ProjectManager>();
        }

        [TestMethod]
        public void FindAllProjectsOwnedByUser_ReturnsNotNull()
        {
            Assert.IsNotNull(projectManager.FindAllProjectsOwnedByUser(9999));
        }

        [TestMethod]
        public void FindAllProjectsSharedWithUser_ReturnsNotNull()
        {
            Assert.IsNotNull(projectManager.FindAllProjectsSharedWithUser(9999));
        }

        [TestMethod]
        public void FindProjectByID_WithValidProjectID_ReturnsNotNull()
        {
            //Assert.IsNull(projectManager.FindProjectByID(Guid.NewGuid()));
        }

        [TestMethod]
        public void FindProjectByID_WithInvalidProjectID_ReturnsNull()
        {
            Assert.IsNull(projectManager.FindProjectByID(Guid.NewGuid()));
        }

        [TestMethod]
        public void CreateProject_AddsNewProjectToTheDatabase()
        {

            var project = new Project();
            project.Name = "test";
            project.IsDeleted = false;
            project.DateStarted = DateTime.Now;
            project.Owner = null;
            project.OwnerID = 0;

            var result = projectManager.CreateProject(project);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, project.ID);
        }        
    }
}
