using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class StudentSubjectGradesDL :BaseDL<StudentGradesAssociation>
    {
        private readonly AppDbContext dbContext;
        public StudentSubjectGradesDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<StudentGradesAssociation> GetStudentGrades(int studentId)
        {
            return GetRecords().Include(r => r.Subject).Where(r => r.StudentId == studentId).ToList();
        }
        public List<StudentGradesAssociation> GetStudentGradesForSubject(int studentId, int subjectId)
        {
            return GetRecords().Include(r => r.Subject).Where(r => r.StudentId == studentId && r.SubjectId==subjectId).ToList();
        }
    }
}
