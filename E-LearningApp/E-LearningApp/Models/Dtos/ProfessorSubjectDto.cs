using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class ProfessorSubjectDto
    {
        public int Id { get; set; }
        public EntityFullNameIdDto ProfessorDtoEntity { get; set; }
        public Subject Subejct { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}
