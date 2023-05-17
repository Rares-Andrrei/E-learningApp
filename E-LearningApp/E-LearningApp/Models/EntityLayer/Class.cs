using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public string YearOfStudy { get; set; }

        public int? ClassMasterId { get; set; }
        public Professor ClassMaster { get; set; }
    }
}