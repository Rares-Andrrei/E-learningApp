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
    public  class StudentGradesBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public StudentGradesBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }

        public List<StudentSubjectGradesDto> GetGradesToDisplay(int studentId)
        {
            var grades = UnitOfWork.StudentSubjectGradesDL.GetStudentGrades(studentId);
            List <StudentSubjectGradesDto> list = new List<StudentSubjectGradesDto>();
            foreach (var item in grades)
            {
                StudentSubjectGradesDto exists = list.Where(r => r.SubejctName == item.Subject.Name).FirstOrDefault();
                if (exists == null)
                {
                    exists = new StudentSubjectGradesDto { SubejctName = item.Subject.Name };
                    list.Add(exists);
                }
                if (item.Semester == 1)
                {
                    exists.FirstSemesterGrades.Add(new GradeDto { Grade = item.Grade, IsThesis = item.Thesis, Date = item.Date});
                }
                else
                {
                    exists.SecondSemesterGrades.Add(new GradeDto { Grade = item.Grade, IsThesis = item.Thesis, Date = item.Date});
                }
            }
            return list;
        }
    }
}
