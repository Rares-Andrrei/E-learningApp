using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class AdministratorsDL : BaseDL<Administrator>
    {
        private readonly AppDbContext dbContext;
        public AdministratorsDL(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
