using E_LearningApp.Models.EntityLayer;
using System;
using System.Configuration;
using System.Data.Entity;

namespace E_LearningApp.Models.Database
{
    public class AppDbContext : DbContext
    {
       public AppDbContext() : base(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString){ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
    }
}
