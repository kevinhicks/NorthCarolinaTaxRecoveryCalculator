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
        public DbSet<RecieptEntity> Reciepts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UsersAccessProjects> UsersAccessProjects { get; set; }
    }
}