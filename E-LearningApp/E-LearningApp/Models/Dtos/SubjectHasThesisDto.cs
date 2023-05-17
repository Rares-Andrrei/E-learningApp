using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class SubjectHasThesisDto
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public bool HasThesis { get; set; }
    }
}
