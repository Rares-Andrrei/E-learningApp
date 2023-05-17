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
    public class SubjectClassCategoryAssociationDL : BaseDL<SubjectClassCategoryAssociation>
    {
        private readonly AppDbContext dbContext;
        public SubjectClassCategoryAssociationDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AlreadyExist(SubjectClassCategoryAssociation sc)
        {
            return Any(s => s.YearOfStudy == sc.YearOfStudy && s.Specialization == sc.Specialization && s.Subject == sc.Subject);
        }
        public List<SubjectClassCategoryAssociation> GetAllWithSubject()
        {
            return GetRecords().
                Include(r => r.Subject).ToList();
        }
    }
}
