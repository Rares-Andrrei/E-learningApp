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
        public int GetStudentIdByUserId(int userId)
        {
            return GetRecords().Where(r => r.UserId == userId).Select(r=> r.Id).FirstOrDefault();
        }
        public List<EntityFullNameIdDto> GetStudentsFromClass(int classId)
        {
            return GetRecords().Include(r=>r.PersonalData).Where(r => r.ClassId == classId).Select(r=> new EntityFullNameIdDto { Id = r.Id, FullName = r.PersonalData.FirstName + " " + r.PersonalData.LastName}).ToList();
        }
        public List<StudentFullNameClassDto> GetStudentsFromClass2(int classId)
        {
            return GetRecords().Include(r => r.PersonalData).Where(r => r.ClassId == classId).Select(r=> new StudentFullNameClassDto { Id = r.Id, ClassId = r.ClassId, FullName = r.PersonalData.FirstName + " " + r.PersonalData.LastName }).ToList();
        }
        public Student GetStudent(int id)
        {
            return GetRecords().Where(r => r.Id == id).Include(r => r.PersonalData).Include(r => r.User).FirstOrDefault();
        }
        public List<Student> GetAllWithPersonalData()
        {
            return GetRecords().Include(r => r.PersonalData).ToList();
        }
    }
}
