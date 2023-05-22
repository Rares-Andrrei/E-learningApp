using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class AbsenceInfoDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Reasoned { get; set; }
    }
}
