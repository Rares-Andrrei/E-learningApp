using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class ProfessorClassGradesDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

       public List<StudentSubjectGradesDto2> Grades { get; set; }
        public ProfessorClassGradesDto()
        {
            Grades = new List<StudentSubjectGradesDto2>();
        }
    }
}
