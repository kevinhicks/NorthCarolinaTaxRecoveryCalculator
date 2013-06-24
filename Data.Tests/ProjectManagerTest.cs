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
        public void FindProjectByID_WithValidProjectID_ReturnsProject()
        {
            //Add a project to the database
            var newProject = new Project();
            //The ID we will search for later
            var testGuid = newProject.ID;

            //required bits for the project
            newProject.Name = "FindProjectByID_WithValidProjectID_ReturnsProject";
            var result = projectManager.CreateProject(newProject);

            //Now the search
            var foundProject = projectManager.FindProjectByID(testGuid);

            //Now chaeck to makes sure we retrieved the same thing we saved
            Assert.IsNotNull(foundProject);
            Assert.AreEqual(newProject.ID, foundProject.ID);
            Assert.AreEqual(newProject.Name, foundProject.Name);
            Assert.AreEqual(newProject.DateStarted, foundProject.DateStarted);
            Assert.AreEqual(newProject.Owner, foundProject.Owner);
        }
        
        [TestMethod]
        public void FindProjectByID_WithInvalidProjectID_ReturnsNull()
        {
            Assert.IsNull(projectManager.FindProjectByID(Guid.NewGuid()));
        }

        [TestMethod]
        public void CreateProject_AddsNewProjectToTheDatabase()
        {
            //Add a project to the database
            var project = new Project();

            //required bits for the project
            project.Name = "CreateProject_AddsNewProjectToTheDatabase";
            project.OwnerID = 0;

            //Now the Add
            var result = projectManager.CreateProject(project);

            //And make sure everything went ok
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, project.ID);
        }


        [TestMethod]
        public void UpdateProject_ChangesTheProjectInTheDatabase()
        {
            //Add a project to the database
            var project = new Project();

            //We will check against this later
            Guid firstProjectID = project.ID;

            //required bits for the project
            project.Name = "UpdateProject_ChangesTheProjectInTheDatabase";
            
            //Now the Add
            project = projectManager.CreateProject(project);

            //Now Change Something
            string changedName = "UpdateProject " + new Random().NextDouble();
            project.Name = changedName;

            //Now Update
            projectManager.UpdateProject(project);
            
            //Search the project back out from the database
            var reloadedProject = projectManager.FindProjectByID(firstProjectID);

            //And make sure everythign was saved, including the changes
            Assert.IsNotNull(reloadedProject);
            Assert.AreEqual(project.ID, reloadedProject.ID);
            Assert.AreEqual(project.Name, reloadedProject.Name);
            Assert.AreEqual(project.DateStarted, reloadedProject.DateStarted);
            Assert.AreEqual(project.Owner, reloadedProject.Owner);
        }

        [TestMethod]
        public void DeleteProject_RemovesTheProjectFromTheDatabase()
        {
            //Add a project to the database
            var project = new Project();

            //We will check against this later
            Guid firstProjectID = project.ID;
            
            //Now the Add
            project = projectManager.CreateProject(project);

            //Now Remove it
            projectManager.DeleteProject(project);

            //Try to search it back out
            Assert.IsNull(projectManager.FindProjectByID(firstProjectID));
        }
    }
}
