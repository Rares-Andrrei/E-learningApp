using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class StudentAbsenceAssociationBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public StudentAbsenceAssociationBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public List<SubejctAbsenceDisplayDto> GetAbsencesToDisplay(int studentId)
        {
            var absences = UnitOfWork.StudentAbsenceAssociationDL.GetStudentsAbsences(studentId);
            List<SubejctAbsenceDisplayDto> list = new List<SubejctAbsenceDisplayDto>();
            foreach (var absence in absences)
            {
                SubejctAbsenceDisplayDto exists = list.Where(r => r.SubjectName == absence.Subject.Name).FirstOrDefault();
                if (exists == null)
                {
                    exists = new SubejctAbsenceDisplayDto { SubjectName = absence.Subject.Name };
                    list.Add(exists);
                }
                if (absence.Semester == 1)
                {
                    exists.FirstSemesterAbsences.Add(new AbsenceInfoDto { Date = absence.AbsenceDate, Reasoned = absence.Reasoned});
                }
                else
                {
                    exists.SecondSemesterAbsences.Add(new AbsenceInfoDto { Date = absence.AbsenceDate, Reasoned = absence.Reasoned });
                }
            }
            return list;
        }
    }
}
