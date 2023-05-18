using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace E_LearningApp.Models.EntityLayer
{
    public class StudentAbsenceAssociation : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int Semester { get; set; }
        public DateTime AbsenceDate { get; set; }
        public bool Reasoned { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
