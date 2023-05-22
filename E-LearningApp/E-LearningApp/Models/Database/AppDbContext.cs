using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

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

        public DbSet<Person> Persons { get; set; }

        public DbSet<Professor> Professors { get; set; }

        public DbSet<ProfessorSubjectAssociation> ProfessorSubjectAssociations { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentAbsenceAssociation> StudentAbsenceAssociations { get; set; }

        public DbSet<StudentGradesAssociation> StudentGradesAssociations { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectClassCategoryAssociation> SubjectClassAssociations { get; set; }

        public List<Specialization> GetSpecializations()
        {
            var result = Database.SqlQuery<Specialization>("GetSpecializations").ToList();
            return result;
        }
        public List<User> GetUsers()
        {
            var result = Database.SqlQuery<User>("GetUsers").ToList();
            return result;
        }
        public List<Subject> GetSubejcts()
        {
            var result = Database.SqlQuery<Subject>("GetSubejcts").ToList();
            return result;
        }
        public List<Subject> GetPersons()
        {
            var result = Database.SqlQuery<Subject>("GetPersons").ToList();
            return result;
        }
        public void UpdateSpecializationName(int id, string newName)
        {
            Database.ExecuteSqlCommand("UpdateSpecialziationName @ID, @NewName",
                new SqlParameter("@ID", id),
                new SqlParameter("@NewName", newName));
        }

        public void UpdateSubjectnName(int id, string newName)
        {
            Database.ExecuteSqlCommand("UpdateSubjectName @ID, @NewName",
                new SqlParameter("@ID", id),
                new SqlParameter("@NewName", newName));
        }
        public void InsertSpecialization(string name)
        {
            Database.ExecuteSqlCommand("InsertSpecialization @Name",
                new SqlParameter("@Name", name));
        }

        public void InsertSubject(string name)
        {
            Database.ExecuteSqlCommand("InsertSubject @Name",
                new SqlParameter("@Name", name));
        }

        public void DeleteSpecialization(int id)
        {
            Database.ExecuteSqlCommand("DeleteSpecialization @Id",
                new SqlParameter("@Id", id));
        }
        public void DeleteSubject(int id)
        {
            Database.ExecuteSqlCommand("DeleteSubject @Id",
                new SqlParameter("@Id", id));
        }

    }
}
