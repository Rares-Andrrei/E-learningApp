using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class SpecializationDL : BaseDL<Specialization>
    {
        private readonly AppDbContext dbContext;
        public SpecializationDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public Specialization GetBySpecializationName(string specializationName)
        {
            return GetRecordsWhere(s => s.Name == specializationName).FirstOrDefault();
        }
        public bool AlreadyExists(string specializationName)
        {
            return Any(s => s.Name == specializationName);
        }
        public void UpdateSpecialization(Specialization specialization)
        {
            if (specialization !=null)
            {
                Update(specialization);
            }
        }
    }
}
