using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class StudentAbsencesDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<AbsenceInfoDto> FirstSemesterAbsences { get; set; }
        public List<AbsenceInfoDto> SecondSemesterAbsences { get; set; }
        public StudentAbsencesDto()
        {
            FirstSemesterAbsences = new List<AbsenceInfoDto>();
            SecondSemesterAbsences = new List<AbsenceInfoDto>();
        }
    }
}
