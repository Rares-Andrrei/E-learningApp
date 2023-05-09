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

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassCategory> ClassCategoryes { get; set; }

        public DbSet<ClassCategoryThesisAssociation> ClassCategoryThesisAssociations { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<ProfessorSubjectAssociation> ProfessorSubjectAssociations { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentAbsenceAssociation> StudentAbsenceAssociations { get; set; }

        public DbSet<StudentGradesAssociation> StudentGradesAssociations { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectClassAssociation> SubjectClassAssociations { get; set; }

        public DbSet<YearOfStudy> YearsOfStudy { get; set; }
    }
}
