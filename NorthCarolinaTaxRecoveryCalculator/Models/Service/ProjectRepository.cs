using NorthCarolinaTaxRecoveryCalculator.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Service
{
    public interface IProjectRepository
    {
        Project FindProjectByID(Guid ProjectID);

        void Create(Project project);
        void Update(Project project);
        void Delete(Project project);

        IEnumerable<Project> FindProjectsOwnedByUser(int userID);
        IEnumerable<Project> FindProjectsSharedWithUser(int userID);
        IEnumerable<UsersAccessProjects> FindAllCollaborators(Guid ProjectID);
        UsersAccessProjects CreateCollaboration(Guid ProjectID, string EmailAddress);
        void SendInvitation(UsersAccessProjects acl, IEmailSender emailSender);
    }

    /// <summary>
    /// Handles all data acess for all Projects
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private ApplicationDBContext db;
        public ProjectRepository()
        {
            db = new ApplicationDBContext();
        }

        /// <summary>
        /// Find a single Project specifiyed by ID
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public Project FindProjectByID(Guid ProjectID)
        {
            return db.Projects.Find(ProjectID);
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void Update(Project project)
        {
            db.Entry(project).State = System.Data.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Project project)
        {
            db.Projects.Remove(project);
            db.SaveChanges();
        }
        /// <summary>
        /// Finds all the collaborators for a specific project
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public IEnumerable<UsersAccessProjects> FindAllCollaborators(Guid ProjectID)
        {
            //Find all the collaborators on this project
            return db.UsersAccessProjects
                     .Where(acl => acl.ProjectID == ProjectID)
                     .ToList();
        }

        /// <summary>
        /// Returns the security level, aka permissions of a user for a specifi projet
        /// </summary>
        /// <param name="user"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public SecurityLevel GetSecurityLevel(UserProfile user, Project project)
        {
            //Mos?

            //Project Owner?
            if (user.UserId == project.OwnerID)
            {
                return SecurityLevel.ProjectAdmin;
            }

            //Collaborator?
            if (db.UsersAccessProjects
                     .Where(acl => acl.ProjectID == project.ID && acl.UserID == user.UserId)
                     .FirstOrDefault() != null)
            {
                return SecurityLevel.Collaborator;
            }

            //anything else has NO permission on this project
            return SecurityLevel.None;
        }

        //Add an entry in the ACL, and send an invitation email
        public void SendInvitation(string email, Guid ProjectID, UserType userType, IEmailSender emailSender)
        {
            //make sure this isnt a duplicate
            if (db.UsersAccessProjects.Where(acl => acl.Email == email && acl.ProjectID == ProjectID).Count() == 0)
            {
                //Working with ACL
                var acl = new UsersAccessProjects();
                acl.Email = email;
                acl.invitationAccepted = false;
                acl.ProjectID = ProjectID;
                acl.UserID = null;

                //Save the ACL entry
                db.UsersAccessProjects.Add(acl);
                db.SaveChanges();

                //build an invitaion email
                string body;
                body = "You have been invited to a new project.\n";
                body += "Click the link to accept the invitation.\n";
                body += "http://northcarolinataxrecoverycalculator.apphb.com/Project/AcceptInvite/" + acl.ID;

                //send an invitaion email
                emailSender.SendMail(email, "You have been invited to a project", body);
            }
        }

        /// <summary>
        /// Return all the projects that the specifyed user created
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<Project> FindProjectsOwnedByUser(int userID)
        {
            return db.Projects
                     .Where(proj => proj.OwnerID == userID)
                     .ToList();
        }

        /// <summary>
        /// Return all the projects that the specifyied user can collaborate on
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<Project> FindProjectsSharedWithUser(int userID)
        {
            var acls = db.UsersAccessProjects
                         .Where(acl => acl.UserID == userID)
                         .Select(acl => acl.ProjectID)
                         .ToList();

            return db.Projects
                     .Where(proj => acls.Contains(proj.ID))
                     .ToList();
        }

        /// <summary>
        /// Allow collaborations on our project with a spcified user
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="EmailAddress"></param>
        /// <returns></returns>
        public UsersAccessProjects CreateCollaboration(Guid ProjectID, string EmailAddress)
        {
            var acl = new UsersAccessProjects();
            acl.Email = EmailAddress;
            acl.invitationAccepted = false;
            acl.ProjectID = ProjectID;
            acl.UserID = null;

            //Save the ACL entry
            db.UsersAccessProjects.Add(acl);
            db.SaveChanges();

            return acl;
        }

        /// <summary>
        /// remove an invitation from teh DB if we no longer want to allow this user to collaborate with us on this project
        /// </summary>
        /// <param name="acl"></param>
        public void RevokeCollaboration(UsersAccessProjects acl)
        {
            db.UsersAccessProjects.Remove(acl);
            db.SaveChanges();
        }

        /// <summary>
        /// Send an invition via email to the invited user to collaborate on a project
        /// </summary>
        /// <param name="acl"></param>
        /// <param name="emailSender"></param>
        public void SendInvitation(UsersAccessProjects acl, IEmailSender emailSender)
        {
            //build an invitaion email. this is dirty :( 
            string body;
            body = "You have been invited to a new project.\n";
            body += "Click the link to accept the invitation.\n";
            body += "http://northcarolinataxrecoverycalculator.apphb.com/Project/AcceptInvite/" + acl.ID;

            //send an invitaion email
            emailSender.SendMail(acl.Email, "You have been invited to a project", body);
        }
    }
}