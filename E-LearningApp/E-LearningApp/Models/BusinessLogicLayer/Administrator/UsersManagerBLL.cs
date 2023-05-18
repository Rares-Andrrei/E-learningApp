using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Models.Enums;
using E_LearningApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_LearningApp.Models.BusinessLogicLayer
{
    public class UsersManagerBLL
    {
        public UnitOfWork UnitOfWork { get; set; }
        public UsersManagerBLL()
        {
            UnitOfWork = Dependencies.UnitOfWork;
        }
        public List<ClassDto> GetClassesDto()
        {
            return UnitOfWork.Classes.GetClassesDtos();
        }
        public List<RoleDto> GetUserRoles()
        {
            List<UserRole> roles = new List<UserRole>();
            roles.AddRange(Enum.GetValues(typeof(UserRole)).Cast<UserRole>());
            return roles.Select(r => new RoleDto(r)).Where(r => r.UserRole != UserRole.Administrator).ToList();
        }
        public bool AddNewProfessor(Professor professor)
        {
            if (UnitOfWork.Users.EmailAlreadyUsed(professor.User.Email))
            {
                return false;
            }
            UnitOfWork.Professors.Insert(professor);
            UnitOfWork.SaveChanges();
            return true;
        }
        public bool AddNewStudent(Student student)
        {
            if (UnitOfWork.Users.EmailAlreadyUsed(student.User.Email))
            {
                return false;
            }
            if (student.ClassId != null)
            {
                student.Class = UnitOfWork.Classes.GetById(student.ClassId.Value);
                UnitOfWork.StudentsDL.Insert(student);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
        public List<UserDisplayDto> GetUserDisplayDtos()
        {
            return UnitOfWork.Professors.GetProfessorToUserDisplayDto().Concat(UnitOfWork.StudentsDL.GetStudentToUserDisplayDto()).ToList();
        }

        public void DeleteUser(UserDisplayDto userDisplayDto)
        {               
            switch(userDisplayDto.UserRole)
            {
                case UserRole.Student:
                    Student student = UnitOfWork.StudentsDL.GetById(userDisplayDto.Id);
                    if (student != null)
                    {
                        var person = UnitOfWork.PersonDL.GetById(student.PersonalDataId);
                        var user = UnitOfWork.UserDL.GetById(student.UserId);
                        UnitOfWork.StudentsDL.Remove(student);
                        UnitOfWork.PersonDL.Remove(person);
                        UnitOfWork.UserDL.Remove(user);
                        UnitOfWork.SaveChanges();
                    }
                    break;
                case UserRole.Professor:
                    Professor professor = UnitOfWork.Professors.GetById(userDisplayDto.Id);
                    if (professor != null)
                    {
                        var person = UnitOfWork.PersonDL.GetById(professor.PersonalDataId);
                        var user = UnitOfWork.UserDL.GetById(professor.UserId);
                        UnitOfWork.Professors.Remove(professor);
                        UnitOfWork.PersonDL.Remove(person);
                        UnitOfWork.UserDL.Remove(user);
                        UnitOfWork.SaveChanges();
                    }
                    break;
            }
        }
    }
}
