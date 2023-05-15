using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class ClassesManagerBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public ClassesManagerBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public List<Specialization> GetSpecializations() 
        {
            return UnitOfWork.Specializations.GetAll();
        }
        public List<EntityFullNameIdDto> GetClassMasters()
        {
            return UnitOfWork.Professors.GetAllAndRelations().Select(p => new EntityFullNameIdDto { FullName = p.PersonalData.FirstName + " " + p.PersonalData.LastName, Id = p.Id }).ToList();
        }
        public bool AddClass(Class _class)
        {
            if (UnitOfWork.Classes.IsNameTaken(_class.Name))
            {
                return false;
            }
            UnitOfWork.Classes.Insert(_class);
            UnitOfWork.SaveChanges();
            return true;
        }
        public List<ClassDto> GetClasses()
        {
            return UnitOfWork.Classes.GetClassesDtos();
        }
        public void DeleteClass(ClassDto _classDto)
        {
            Class _class = UnitOfWork.Classes.GetById(_classDto.Id);
            if (_class != null)
            {
                UnitOfWork.Classes.Remove(_class);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
