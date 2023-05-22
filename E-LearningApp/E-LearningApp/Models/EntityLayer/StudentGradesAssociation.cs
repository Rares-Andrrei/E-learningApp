using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class StudentGradesAssociation : BaseEntity
    { 
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime Date { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
        public bool Thesis { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
