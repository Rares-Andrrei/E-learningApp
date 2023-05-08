using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.EntityLayer
{
    public class Student : BaseEntity
    {
        public int PersonalDataId { get; set; }
        public Person PersonalData { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int? ClassCategoryId { get; set; }
        public ClassCategory ClassCategory { get; set; }

        public int? ClassId { get; set; }
        public Class Class { get; set; }
    }
}
