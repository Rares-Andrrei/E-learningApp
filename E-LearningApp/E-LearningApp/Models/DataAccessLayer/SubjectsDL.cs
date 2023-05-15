using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public  class SubjectsDL : BaseDL<Subject>
    {
        private readonly AppDbContext dbContext;
        public SubjectsDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool IsNameTaken(Subject subejct)
        {
            return Any(s => s.Name == subejct.Name);
        }
    }
}
