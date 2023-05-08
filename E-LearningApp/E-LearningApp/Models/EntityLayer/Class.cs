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

        public int ClassCategoryId { get; set; }
        public ClassCategory ClassCategory { get; set; }

        public int? ClassMasterId { get; set; }
        public Professor ClassMaster { get; set; }
    }
}
