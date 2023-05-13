using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class UsersDL : BaseDL<User>
    {
        private readonly AppDbContext dbContext;
        public UsersDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public User GetByEmail(string email)
        {
            return GetRecordsWhere(u => u.Email == email).FirstOrDefault();
        }
        public bool EmailAlreadyUsed(string email) 
        { 
            return Any(u => u.Email == email);
        }
    }
}
