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

        public List<ProfessorSubjectDto> GetProfessorSubjects()
        {
            return GetRecords()
                .Include(r => r.Professor)
                .Include(r => r.Professor.PersonalData)
                .Include(r => r.Subject)
                .Include(r => r.Class)
                .Select(p => new ProfessorSubjectDto
                {
                    Id = p.Id,
                    ProfessorDtoEntity = new EntityFullNameIdDto
                    {
                        Id = p.Id,
                        FullName = p.Professor.PersonalData.FirstName + " " + p.Professor.PersonalData.LastName
                    },
                    Subejct = p.Subject,
                    ClassId = p.Class.Id,
                    ClassName = p.Class.Name
                }).ToList();
        }
        public bool ProfessorSubjectExists(ProfessorSubjectAssociation professorSubjectAssociation)
        {
            return Any(r => r.ProfessorId == professorSubjectAssociation.Professor.Id && r.SubjectId == professorSubjectAssociation.Subject.Id && r.ClassId == professorSubjectAssociation.Class.Id);
        }
        public List<ProfessorSubjectAssociation> GetAllByProfessor(int professorId)
        {
            return GetRecords().Include(r=>r.Subject).Include(r=>r.Class).Where(r => r.ProfessorId == professorId).ToList();
        }
        public List<int> GetAllIdsByProfessor(int professorId)
        {
            return GetRecords().Where(r => r.ProfessorId == professorId).Select(r=>r.ClassId).ToList();
        }
        public List<Subject> GetSubjectsProfessorClass(int professorId, int classId)
        {
            return GetRecords().Include(r=>r.Subject).Where(r => r.ProfessorId == professorId && r.ClassId == classId).Select(r => r.Subject).ToList();
        }
    }
}
