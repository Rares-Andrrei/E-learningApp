using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class StudentSubjectGradesDto2
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<GradeDto> FirstSemesterGrades { get; set; }
        public List<GradeDto> SecondSemesterGrades { get; set; }
        public StudentSubjectGradesDto2()
        {
            FirstSemesterGrades = new List<GradeDto>();
            SecondSemesterGrades = new List<GradeDto>();
        }
    }
}
