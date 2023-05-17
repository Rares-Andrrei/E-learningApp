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
    public class ProfessorSubjectManagerBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public ProfessorSubjectManagerBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }

        public List<EntityFullNameIdDto> GetProfessorsFullNameDto()
        {
            return UnitOfWork.Professors.GetProfessorsToEntityFullNameIdDto();
        }

        public List<Subject> GetSubjectForSpecializationAndYear(int specialization, string yearOfStudy)
        {
            return UnitOfWork.SubjectClassCategoryAssociationDL.GetAllWithSubject().Where(r => r.SpecializationId == specialization && r.YearOfStudy == yearOfStudy).Select(x => x.Subject).ToList();
        }

        public List<Class> GetClasses()
        {
            return UnitOfWork.Classes.GetAll();
        }

        public List<ProfessorSubjectDto> GetProfessorsAndSubjects()
        {
            return UnitOfWork.ProfessorSubjectAssociationDL.GetProfessorSubjects();
        }

        public void AddProfessorSubjectAssociation(int professorId, Subject subject, int classId)
        {
            Professor professor = UnitOfWork.Professors.GetById(professorId);
            Class @class = UnitOfWork.Classes.GetById(classId);
            if (professor != null && subject != null && @class != null)
            {
                var item = new ProfessorSubjectAssociation { Professor = professor, Subject = subject, Class = @class};
                if (!UnitOfWork.ProfessorSubjectAssociationDL.ProfessorSubjectExists(item))
                {
                    UnitOfWork.ProfessorSubjectAssociationDL.Insert(item);
                    UnitOfWork.SaveChanges();
                }
            }
        }

        public void DeleteProfessorSubjectAssociation(int id)
        {
            ProfessorSubjectAssociation professorSubjectAssociation = UnitOfWork.ProfessorSubjectAssociationDL.GetById(id);
            if (professorSubjectAssociation != null)
            {
                UnitOfWork.ProfessorSubjectAssociationDL.Remove(professorSubjectAssociation);
                UnitOfWork.SaveChanges();
            }
        }

    }
}
