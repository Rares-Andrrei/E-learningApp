using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class SubjectsManagerBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public SubjectsManagerBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public bool AddSubject(string subjectName)
        {
            if (subjectName != null && subjectName.Trim() != string.Empty) 
            {
                Subject subject = new Subject { Name = subjectName };
                if (!UnitOfWork.SubjectsDL.IsNameTaken(subject))
                {
                    UnitOfWork.SubjectsDL.Insert(subject);
                    UnitOfWork.SaveChanges();
                    return true;
                }              
            }
            return false;
        }
        public bool DeleteSubject(Subject specialization)
        {
            if (specialization != null)
            {
                UnitOfWork.SubjectsDL.Remove(specialization);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Subject> GetAllSubjects()
        {
            return UnitOfWork.SubjectsDL.GetAll();
        }
        public List<Specialization> GetAllSpecializations()
        {
            return UnitOfWork.Specializations.GetAll();
        }

        public bool AddAssociation(SubjectClassCategoryAssociation s)
        {
            if (!(s.Subject == null || s.Specialization == null || s.YearOfStudy == null))
            {
                if (!UnitOfWork.SubjectClassCategoryAssociationDL.AlreadyExist(s))
                {
                    UnitOfWork.SubjectClassCategoryAssociationDL.Insert(s);
                    UnitOfWork.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public List<ClassCategorySubjectsDto> GetClassCategoryWithSubject()
        {
            List<SubjectClassCategoryAssociation> list = UnitOfWork.SubjectClassCategoryAssociationDL.GetAll();
            List<ClassCategorySubjectsDto> list2 = new List<ClassCategorySubjectsDto>();
            foreach (SubjectClassCategoryAssociation sc in list)
            {
                var exists = list2.FirstOrDefault(record => record.Specialization == sc.Specialization && record.YearOfStudy == sc.YearOfStudy);
                if (exists == null)
                {
                    list2.Add(new ClassCategorySubjectsDto { Specialization = sc.Specialization, YearOfStudy = sc.YearOfStudy, Subjects = new List<SubjectHasThesisDto> { new SubjectHasThesisDto { Subject = sc.Subject, HasThesis = sc.Thesis, Id = sc.Id } } });
                }
                else
                {
                    exists.Subjects.Add(new SubjectHasThesisDto { Subject = sc.Subject, HasThesis = sc.Thesis, Id = sc.Id});
                }
            }
            return list2;   
        }
   
        public bool DeleteSubjectClassCategoryAssociation(int associationId)
        {
            SubjectClassCategoryAssociation subjectClassCategoryAssociation = UnitOfWork.SubjectClassCategoryAssociationDL.GetById(associationId);

            if (subjectClassCategoryAssociation != null)
            {
                UnitOfWork.SubjectClassCategoryAssociationDL.Remove(subjectClassCategoryAssociation);
                UnitOfWork.SaveChanges();
                return true;

            }
            return false;
        }
    }
}
