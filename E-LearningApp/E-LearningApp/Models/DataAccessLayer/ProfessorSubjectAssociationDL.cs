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
    public class ProfessorSubjectAssociationDL : BaseDL<ProfessorSubjectAssociation>
    {
        private readonly AppDbContext dbContext;
        public ProfessorSubjectAssociationDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProfessorSubject> GetProfessorSubjects()
        {
            return  GetRecords()
                .Include(r => r.Professor)
                .Include(r => r.Professor.PersonalData)
                .Include(r => r.Subject)
                .Select(p => new ProfessorSubject
                {
                    Id = p.Id,
                    ProfessorDtoEntity = new EntityFullNameIdDto
                    {
                        Id = p.Id,
                        FullName = p.Professor.PersonalData.FirstName + " " + p.Professor.PersonalData.LastName
                    },
                    Subejct = p.Subject
                }).ToList();
        }
        public bool ProfessorSubjectExists(ProfessorSubjectAssociation professorSubjectAssociation)
        {
            return Any(r => r.ProfessorId == professorSubjectAssociation.Professor.Id && r.SubjectId == professorSubjectAssociation.Subject.Id);
        }
    }
}
