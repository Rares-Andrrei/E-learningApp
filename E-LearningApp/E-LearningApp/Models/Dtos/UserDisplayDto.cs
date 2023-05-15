using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class UserDisplayDto
    {
        public UserDisplayDto(Professor professor) 
        {
            Id = professor.Id;
            FirstName = professor.PersonalData.FirstName;
            LastName = professor.PersonalData.LastName;
            BirthDate = professor.PersonalData.BirthDate;
            Email = professor.User.Email;
            Password = professor.User.Password;
            UserRole = UserRole.Professor;
            ClassName = "-";
        }
        public UserDisplayDto(Student student)
        {
            Id = student.Id;
            FirstName = student.PersonalData.FirstName;
            LastName = student.PersonalData.LastName;
            BirthDate = student.PersonalData.BirthDate;
            Email = student.User.Email;
            Password = student.User.Password;
            UserRole = UserRole.Student;
            if (student.Class == null)
            {
                ClassName = "-";
            }
            else
            {
                ClassName = student.Class.Name;
            }
        }


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClassName { get; set; }
        public UserRole UserRole { get; set; }
    }
}
