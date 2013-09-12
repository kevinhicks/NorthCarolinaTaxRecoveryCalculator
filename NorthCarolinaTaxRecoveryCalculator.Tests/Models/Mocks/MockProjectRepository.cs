using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models.Mocks
{
    class MockProjectRepository : IProjectRepository
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
