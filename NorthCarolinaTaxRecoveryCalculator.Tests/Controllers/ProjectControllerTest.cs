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
using NorthCarolinaTaxRecoveryCalculator.ViewModels.Project;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Models;

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

        //TODO: MUCH more testing
        
        private class MockProjectRepository : IProjectRepository
        {
            private List<Project> Projects;            
            private List<UsersAccessProjects> Acl;
            

            public MockProjectRepository()
            {
                Projects = new List<Project>();
                Acl = new List<UsersAccessProjects>();
            }

            public NorthCarolinaTaxRecoveryCalculator.Models.Project FindProjectByID(Guid ProjectID)
            {
                return Projects.Where(col => col.ID == ProjectID).FirstOrDefault();
            }

            public void Create(NorthCarolinaTaxRecoveryCalculator.Models.Project project)
            {
                Projects.Add(project);
            }

            public void Update(NorthCarolinaTaxRecoveryCalculator.Models.Project project)
            {
                return;
            }

            public void Delete(NorthCarolinaTaxRecoveryCalculator.Models.Project project)
            {
                Projects.Remove(project);
            }
            
            public IEnumerable<Project> FindProjectsOwnedByUser(int userID)
            {
                return Projects.Where(col => col.OwnerID == userID).ToList();
            }

            public IEnumerable<Project> FindProjectsSharedWithUser(int userID)
            {
                var projectIDs = Acl.Where(col => col.UserID == userID)
                                    .Select(col => col.ProjectID)
                                    .ToList();
                return Projects.Where(col => projectIDs.Contains(col.ID)).ToList();
            }

            public IEnumerable<UsersAccessProjects> FindAllCollaborators(Guid ProjectID)
            {
                throw new NotImplementedException();
            }

            public UsersAccessProjects CreateCollaboration(Guid ProjectID, string EmailAddress)
            {
                var acl = new UsersAccessProjects();
                acl.ProjectID = ProjectID;
                acl.Email = EmailAddress;

                Acl.Add(acl);

                return acl;
            }

            public void SendInvitation(UsersAccessProjects acl, NorthCarolinaTaxRecoveryCalculator.Misc.IEmailSender emailSender)
            {
                throw new NotImplementedException();
            }


            public void RevokeCollaboration(UsersAccessProjects acl)
            {
                throw new NotImplementedException();
            }

            public void AcceptCollaboration(int aclID, int UserID)
            {
                var acl = Acl.Where(col => col.ID == aclID).FirstOrDefault();
                acl.invitationAccepted = true;
                acl.UserID = UserID;
            }
        }
    }
}
