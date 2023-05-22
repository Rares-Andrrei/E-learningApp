using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class StudentFullNameClassDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? ClassId { get; set; }      
   }
}
