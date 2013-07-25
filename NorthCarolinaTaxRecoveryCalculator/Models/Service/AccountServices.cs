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
    }
}
