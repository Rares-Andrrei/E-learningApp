using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class SubjectClassCategoryAssociation : BaseEntity
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public bool Thesis { get; set; }

        public string YearOfStudy { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
