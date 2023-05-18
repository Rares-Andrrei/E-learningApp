using E_LearningApp.Models.Database;
using E_LearningApp.ViewModels;
using E_LearningApp.Models.DataAccessLayer;
using E_LearningApp.Models.EntityLayer;

namespace E_LearningApp.Settings
{
    public static class Dependencies
    {
        public static AppDbContext AppDbContext { get; set; }
        public static UnitOfWork UnitOfWork { get; set; }
        public static MainWindowVM MainWindowVM { get; set; }
        static Dependencies() 
        {
            AppDbContext = new AppDbContext();
            UnitOfWork = new UnitOfWork(AppDbContext, new UsersDL(AppDbContext), new SpecializationDL(AppDbContext), new ProfessorsDL(AppDbContext), new ClassesDL(AppDbContext), new StudentsDL(AppDbContext),
                new SubjectsDL(AppDbContext), new SubjectClassCategoryAssociationDL(AppDbContext), new ProfessorSubjectAssociationDL(AppDbContext), new UserDL(AppDbContext), new PersonDL(AppDbContext));
            MainWindowVM = new MainWindowVM();
        }
    }
}
