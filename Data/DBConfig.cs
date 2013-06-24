using Data.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Data
{
    public class ApplicationDBContext : DbContext, IDisposable
    {
        public DbSet<Reciept> Reciepts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UsersAccessProjects> UsersAccessProjects { get; set; }

        /// <summary>
        /// This should be called at appllication start. 
        /// It will setup the database with any pending changes. 
        /// </summary>
        public void UpdateDatabaseToLatestVersion()
        {
            new MigrateDatabaseToLatestVersion<ApplicationDBContext, Configuration>().InitializeDatabase(this);
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}