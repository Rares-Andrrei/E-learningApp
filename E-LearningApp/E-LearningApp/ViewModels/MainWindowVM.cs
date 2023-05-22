using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.ProfessorF;
using E_LearningApp.Views;
using E_LearningApp.Views.ClassMasterF;
using E_LearningApp.Views.ProfessorF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace E_LearningApp.ViewModels
{
    public class MainWindowVM : BasePropertyChanged
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; NotifyPropertyChanged(nameof(CurrentView));}
        }
        public MainWindowVM()
        {
            CurrentView = new LoginView();
        }
        public void ShowAdministratorsView(AdministratorVM administratorVM)
        {
            CurrentView = new AdministratorView(administratorVM);
        }
        public void ShowStudentView(StudentVM studentVM)
        {
            CurrentView = new StudentView(studentVM);
        }
        public void ShowLogInView()
        {
            CurrentView = new LoginView();
        }
        public void ShowClassMasterView(int userId)
        {
            CurrentView = new ClassMasterView();
        }
        public void ShowProfessorView(int userId)
        {
            CurrentView = new ProfessorView(new ProfessorVM(userId));
        }
        public void ShowClassMasterOptions(LoginVM loginVM)
        {
            CurrentView = new ClassmasterOptionsView(loginVM);
        }
    }
}
