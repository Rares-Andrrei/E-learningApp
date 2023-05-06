using E_LearningApp.Models.Database;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class LoginBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public LoginBLL() 
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }


    }
}
