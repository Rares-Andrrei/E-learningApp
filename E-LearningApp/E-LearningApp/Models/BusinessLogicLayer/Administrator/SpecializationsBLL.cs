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

            return UnitOfWork.Specializations.GetAllProcedure();
        }
        public Specialization GetSpecializationByName(string specializationName)
        {
            if (specializationName == null)
            {
                return null;
            }

            return UnitOfWork.Specializations.GetBySpecializationName(specializationName);
        }

        public bool AlreadyExists(string specializationName)
        {
            if (specializationName != null)
            {
                return UnitOfWork.Specializations.AlreadyExists(specializationName);
            }
            return false;
        }

        public void AddSpecialization(string specializationName)
        {
            if (specializationName == null || specializationName.Trim() == string.Empty)
            {
                throw new InputException("Incorrect input");
            }
            if (AlreadyExists(specializationName))
            {
                throw new InputException("This specialization already exists");
            }
            UnitOfWork.Specializations.Insert(new Specialization { Name = specializationName });
            UnitOfWork.SaveChanges();
        }
        public void DeleteSpecialization(Specialization specialization)
        {
            UnitOfWork.Specializations.Remove(specialization);
            UnitOfWork.SaveChanges();
        }

        public bool UpdateSpecialization(Specialization specialization)
        {
            if (specialization == null || UnitOfWork.Specializations.AlreadyExists(specialization.Name))
            {
                return false;
            }
            UnitOfWork.Specializations.UpdateSpecialization(specialization);
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
