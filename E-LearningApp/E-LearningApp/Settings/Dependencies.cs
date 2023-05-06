﻿using E_LearningApp.Models.Database;
using E_LearningApp.ViewModels;
using E_LearningApp.Models.DataAccessLayer;

namespace E_LearningApp.Settings
{
    public static class Dependencies
    {
        public static AppDbContext AppDbContext { get; set; }
        public static UnitOfWork UnitOfWork { get; set; }
        public static MainWindowVM MainWindowVM { get; set; }
        static Dependencies() 
        {
            MainWindowVM = new MainWindowVM();
            AppDbContext = new AppDbContext();
            UnitOfWork = new UnitOfWork(AppDbContext, new UsersDL(AppDbContext), new AdministratorsDL(AppDbContext));
        }
    }
}