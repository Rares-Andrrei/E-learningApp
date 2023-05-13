using E_LearningApp.Models.Database;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public  class StudentsDL : BaseDL<Student>
    {
        private readonly AppDbContext dbContext;
        public StudentsDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<UserDisplayDto> GetStudentToUserDisplayDto()
        {
            List<UserDisplayDto> list = new List<UserDisplayDto>();
            var items = GetRecords()
                .Include(s => s.PersonalData)
                .Include(s => s.User)
                .Include(s => s.Class)
                .ToList();
            foreach (var item in items)
            {
                list.Add(new UserDisplayDto(item));
            }
            return list;
        }
    }
}
