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
    }
}