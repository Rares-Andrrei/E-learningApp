using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class StudentSubjectGradesDto
    {
        public string SubejctName { get; set; }
        public List<GradeDto> FirstSemesterGrades { get; set; }
        public List<GradeDto> SecondSemesterGrades { get; set; }
        public StudentSubjectGradesDto()
        {
            FirstSemesterGrades = new List<GradeDto>();
            SecondSemesterGrades = new List<GradeDto>();
        }
    }
}
