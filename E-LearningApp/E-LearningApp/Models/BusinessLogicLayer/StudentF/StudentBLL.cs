using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class StudentBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public StudentBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public int GetStudentIdByUserId(int userId)
        {
            return UnitOfWork.StudentsDL.GetStudentIdByUserId(userId);
        }
        public string GetUsersEmail(int userId)
        {
            return UnitOfWork.Users.GetEmailByUserId(userId);
        }
    }
}
