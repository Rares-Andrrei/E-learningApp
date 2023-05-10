using E_LearningApp.Models.DataAccessLayer;
using System;

namespace E_LearningApp.Models.Database
{
    public class UnitOfWork
    {
        public UsersDL Users { get; set; }
        public SpecializationDL Specializations { get; set; }


        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            UsersDL users,
            SpecializationDL specializations
        )
        {
            _dbContext = dbContext;
            Users = users;
            Specializations = specializations;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}
