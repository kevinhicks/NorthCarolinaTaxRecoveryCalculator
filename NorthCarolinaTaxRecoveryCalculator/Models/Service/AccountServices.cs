using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Data;
using System.Linq;
using NorthCarolinaTaxRecoveryCalculator.Misc;

namespace NorthCarolinaTaxRecoveryCalculator.Models
{
    public class ACLManager
    {
        /// <summary>
        /// Finds all the collaborators for a specific project
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public IEnumerable<UsersAccessProjects> FindAllCollaborators(Guid ProjectID)
        {
            var db = new ApplicationDBContext();

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
            var db = new ApplicationDBContext();
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
            //Database access
            var db = new ApplicationDBContext();
            
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
    }
}
