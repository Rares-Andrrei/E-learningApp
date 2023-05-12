using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class ClassDto
    {
        public string Name { get; set; }

        public Specialization Specialization { get; set; }

        public string YearOfStudy { get; set; }

        public int? ClassMasterId { get; set; }
        public string ClassMasterFullName { get; set; }
    }
}
