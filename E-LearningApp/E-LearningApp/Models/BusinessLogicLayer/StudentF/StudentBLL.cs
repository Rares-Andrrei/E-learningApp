using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
        public string GetStudentYear(int stuedntId)
        {
            var student = UnitOfWork.StudentsDL.GetById(stuedntId);
            if (student.ClassId != null)
            {
                var @class = UnitOfWork.Classes.GetById(student.ClassId.Value);
                return @class.YearOfStudy;
            }
            return string.Empty;
        }

        public string GetStudentSpecialization(int stuedntId)
        {
            var student = UnitOfWork.StudentsDL.GetById(stuedntId);
            if (student.ClassId != null)
            {
                var @class = UnitOfWork.Classes.GetById(student.ClassId.Value);
                var specializtion = UnitOfWork.Specializations.GetById(@class.SpecializationId);
                return specializtion.Name;
            }
            return string.Empty;
        }
    }
}
