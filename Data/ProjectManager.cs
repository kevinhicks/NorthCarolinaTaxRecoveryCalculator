﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Data
{
    /// <summary>
    /// This will habeld any and all logic for retrieveing Project data from the Data-Access-Layer
    /// </summary>
    public class ProjectManager
    {
        ApplicationDBContext database = null;

        public ProjectManager()
        {
            database = new ApplicationDBContext();
        }

        /// <summary>
        /// Return a list of all projects that the specified user ownes
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Project> FindAllProjectsOwnedByUser(int UserID)
        {
            //TODO: Allow searching for deleted Projects
            return database.Projects.Where(cols => cols.OwnerID == UserID && cols.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Return a list of all projects that the specified user is a collaborator on
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Project> FindAllProjectsSharedWithUser(int UserID)
        {
            //This will be done in 2 steps.
            //First, we need to find the list of Project IDs that the user has 
            //  accepted previous invitations for.
            List<Guid> projectIDs = database.UsersAccessProjects.Where(cols => cols.UserID == UserID && cols.invitationAccepted == true).Select(col => col.ProjectID).ToList();

            //New return the a list of projects matching the IDs
            return database.Projects.Where(cols => projectIDs.Contains(cols.ID) && cols.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Returns a Project from the store specifyed by ID
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public Project FindProjectByID(Guid ProjectID)
        {
            //TODO: Allow searching for deleted Projects
            return database.Projects.FirstOrDefault(cols => cols.ID == ProjectID && cols.IsDeleted == false);
        }

        /// <summary>
        /// Adds a new Project to the database store
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Project CreateProject(Project project)
        {
            var result = database.Projects.Add(project);
            database.SaveChanges();
            return result;
        }

        /// <summary>
        /// Saves any changes to the database records
        /// </summary>
        /// <param name="project"></param>
        public void UpdateProject(Project project) {
            database.SaveChanges();
        }

        /// <summary>
        /// Removes a Project From the database
        /// </summary>
        /// <param name="project"></param>
        public void DeleteProject(Project project) {
            project.IsDeleted = true;
            database.SaveChanges();
        }
    }
}