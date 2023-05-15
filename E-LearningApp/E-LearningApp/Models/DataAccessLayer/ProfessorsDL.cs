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
    public class ProfessorsDL : BaseDL<Professor>
    {
        private readonly AppDbContext dbContext;
        public ProfessorsDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserDisplayDto> GetProfessorToUserDisplayDto()
        {

            List<UserDisplayDto> list = new List<UserDisplayDto>();
            var items = GetRecords()
                .Include(p => p.PersonalData)
                .Include(p => p.User)
                .ToList();
            foreach (var item in items)
            {
                list.Add(new UserDisplayDto(item));
            }
            return list;
        }

        public List<Professor> GetAllAndRelations()
        {
            return GetRecords()
                .Include(p => p.PersonalData)
                .Include(p => p.User)
                .ToList();
        }
        public List<EntityFullNameIdDto> GetProfessorsToEntityFullNameIdDto()
        {
            return GetRecords()
                .Include(p => p.PersonalData)
                .Select(r => new EntityFullNameIdDto { FullName = r.PersonalData.FirstName + " " + r.PersonalData.LastName, Id = r.Id }).ToList();
        }
    }
}
