using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.DataAccessLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.ViewModels
{
    public class StudentGradesVM : BasePropertyChanged
    {
        private List<StudentSubjectGradesDto> _studentSubjectGrades;
        public List<StudentSubjectGradesDto> StudentSubjectGrades
        {
            get { return _studentSubjectGrades; }
            set { _studentSubjectGrades = value; NotifyPropertyChanged(nameof(StudentSubjectGrades)); }
        }
        public StudentGradesBLL StudentGradesBLL { get; set;}
        public int StudentId { get; set;}
        public StudentGradesVM(int studentId)
        {
            StudentGradesBLL = new StudentGradesBLL();
            StudentId = studentId;
            InitGradesList();
        }
        public void InitGradesList()
        {
            StudentSubjectGrades = StudentGradesBLL.GetGradesToDisplay(StudentId);
        }

    }
}
