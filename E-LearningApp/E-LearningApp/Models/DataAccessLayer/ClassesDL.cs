using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class ClassesDL : BaseDL<Class>
    {
        private readonly AppDbContext dbContext;
        public ClassesDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool IsNameTaken(string classname)
        {
            return Any(c => c.Name == classname);
        }
        public List<ClassDto> GetClassesDtos()
        {
            return GetRecords().Select(c => new ClassDto { Id = c.Id, Name = c.Name, Specialization = c.Specialization, 
                ClassMasterFullName = c.ClassMaster.PersonalData.FirstName + " " + c.ClassMaster.PersonalData.LastName, YearOfStudy = c.YearOfStudy, ClassMasterId = c.ClassMasterId }).ToList();
        }
    }
}
