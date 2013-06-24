using Data.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Reciept> Reciepts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UsersAccessProjects> UsersAccessProjects { get; set; }
        
        /// <summary>
        /// Update the database to the latest version
        /// 
        /// WARNING! This can make drastic & irreversable changes to the database!
        /// </summary>
        public void UpdateDatabase()
        {
            var updateDBInit = new MigrateDatabaseToLatestVersion<ApplicationDBContext, Configuration>();
            updateDBInit.InitializeDatabase(this);
        }
    }
}