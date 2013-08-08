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
    public interface IProjectRepository
    {
        /// <summary>
        /// Return a single Project by its ID
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        Project Get(Guid ProjectID);
        
        /// <summary>
        /// Update & Save the changes of  aproject
        /// </summary>
        /// <param name="project"></param>
        void Update(Project project);

        /// <summary>
        /// Create a new Project and save it into the store
        /// </summary>
        /// <param name="project"></param>
        void Create(Project project);

        /// <summary>
        /// Remove a project form the store
        /// </summary>
        /// <param name="Project"></param>
        void Delete(Project project);
    }

    public class ProjectManager : IProjectRepository
    {
        protected ApplicationDBContext db = null;

        public ProjectManager()
        {
            db = new ApplicationDBContext();
        }

        public Project Get(Guid ProjectID)
        {
            return db.Projects.Where(col => col.ID == ProjectID).FirstOrDefault();
        }

        public void Update(Project project)
        {
            db.Projects.Attach(project);
            var entry = db.Entry(project).State = System.Data.EntityState.Modified;
            db.SaveChanges();
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void Delete(Project project)
        {
            db.Projects.Remove(project);
            db.SaveChanges();
        }
    }
}
