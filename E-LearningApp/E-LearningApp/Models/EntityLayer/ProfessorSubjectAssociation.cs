using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class ProfessorSubjectAssociation : BaseEntity
    {
        public int ProfessorId { get; set; }
        public  Professor  Professor { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
