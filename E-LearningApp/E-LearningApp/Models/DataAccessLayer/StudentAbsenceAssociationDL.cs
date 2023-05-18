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
    public class StudentAbsenceAssociationDL : BaseDL<StudentAbsenceAssociation>
    {
        private readonly AppDbContext dbContext;
        public StudentAbsenceAssociationDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<StudentAbsenceAssociation> GetStudentsAbsences(int studentId)
        {
            return GetRecords().Include(r => r.Subject).Where(r => r.StudentId == studentId).ToList();
        }
    }
}
