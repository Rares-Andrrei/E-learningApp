using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class ClassCategoryThesisAssociation : BaseEntity
    {
        public int ClassCategoryId { get; set; }
        public ClassCategory ClassCategory { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
