using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.ViewModels
{
    public class AdministratorVM
    {
        public int AdminId { get; set; }
        public AdministratorVM(int id) 
        {
            AdminId = id;
        }
    }
}
