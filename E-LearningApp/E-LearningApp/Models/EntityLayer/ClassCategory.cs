using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class ClassCategory : BaseEntity
    {
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public int YearofStudyId { get; set; }
        public YearOfStudy YearOfStudy { get; set; }
    }
}
