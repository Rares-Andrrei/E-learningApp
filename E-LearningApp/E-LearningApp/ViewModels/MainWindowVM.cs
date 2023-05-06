using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace E_LearningApp.ViewModels
{
    public class MainWindowVM
    {
        public UserControl CurrentView { get; set; }
        public MainWindowVM()
        {
            CurrentView = new LoginView();
        }
    }
}
