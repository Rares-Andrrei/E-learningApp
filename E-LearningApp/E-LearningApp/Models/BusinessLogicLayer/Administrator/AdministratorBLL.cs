using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class AdministratorBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public AdministratorBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        } 
        public string GetEmailById(int id)
        {
            User user = UnitOfWork.Users.GetById(id);
            if (user == null)
            {
                return string.Empty;
            }
            return user.Email;
        }
    }
}
