using E_LearningApp.Exceptions;
using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class SpecializationsBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public SpecializationsBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }

        public List<Specialization> GetSpecializations()
        {

            return UnitOfWork.Specializations.GetAll();
        }
        public Specialization GetSpecializationByName(string specializationName)
        {
            if (specializationName == null)
            {
                return null;
            }

            return UnitOfWork.Specializations.GetBySpecializationName(specializationName);
        }
        public void AddSpecialization(string specialization)
        {
            if (specialization == null || specialization.Trim() == string.Empty)
            {
                throw new InputException("Incorrect input");
            }
            if (UnitOfWork.Specializations.Any(s => s.Name == specialization))
            {
                throw new InputException("This specialization already exists");
            }
            UnitOfWork.Specializations.Insert(new Specialization { Name = specialization });
            UnitOfWork.SaveChanges();
        }
        public void DeleteSpecialization(Specialization specialization)
        {
            UnitOfWork.Specializations.Remove(specialization);
            UnitOfWork.SaveChanges();
        }
    }
}
