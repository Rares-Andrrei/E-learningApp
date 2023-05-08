using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class Professor : BaseEntity
    {
        public int PersonalDataId { get; set; }
        public Person PersonalData { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
