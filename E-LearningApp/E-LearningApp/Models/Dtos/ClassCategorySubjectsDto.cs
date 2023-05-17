using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class ClassCategorySubjectsDto
    {
        public string YearOfStudy { get; set; }
        public Specialization Specialization { get; set; }
        public List<SubjectHasThesisDto> Subjects { get; set; }
    }
}
