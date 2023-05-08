using E_LearningApp.Migrations;
using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Models.Enums;
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
        public Tuple<int, UserRole> Login(string username, string password)
        {
            if (username == null || password == null) 
            {
                throw new LoginException("Failed to login");
            }
            User user = UnitOfWork.Users.GetByEmail(username);
            if (user == null)
            {
                throw new LoginException("Account not found");
            }
            else if(password != user.Password) 
            {
                throw new LoginException("Incorect password");
            }
            return new Tuple<int, UserRole>(user.Id, user.Role);
        }
    }
}
