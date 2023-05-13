using E_LearningApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.Dtos
{
    public class RoleDto
    {
        public RoleDto(UserRole userRole) { UserRole = userRole;  RoleName = userRole.ToString(); }   
        public UserRole UserRole { get; set; }
        public string RoleName { get; set; }
    }
}
