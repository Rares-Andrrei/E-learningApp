using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using E_LearningApp.ViewModels.Commands;
using E_LearningApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class StudentVM : BasePropertyChanged
    {
        public int StudentId { get; set; }
        private string _email;
        public string Email
        {
            get { return _email;}
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }



        public StudentBLL StudentBLL { get; set; }
        public StudentVM(int userId) 
        {
            StudentBLL = new StudentBLL();
            StudentId = StudentBLL.GetStudentIdByUserId(userId);
            Email = StudentBLL.GetUsersEmail(userId);
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; NotifyPropertyChanged(nameof(CurrentView)); }
        }




        #region commands


        private ICommand _showAbsencesViewCommand;
        public ICommand ShowAbsencesViewCommand
        {
            get
            {
                if (_showAbsencesViewCommand == null)
                {
                    _showAbsencesViewCommand = new RelayCommandsV2(ShowAbsencesView);
                }
                return _showAbsencesViewCommand;
            }
        }
        public void ShowAbsencesView(object parameter)
        {
            CurrentView = new StudentAbsencesView(new StudentAbsencesVM(StudentId));
        }

        private ICommand _showGradesViewCommand;
        public ICommand ShowGradesViewCommand
        {
            get
            {
                if (_showGradesViewCommand == null)
                {
                    _showGradesViewCommand = new RelayCommandsV2(ShowGradesView);
                }
                return _showGradesViewCommand;
            }
        }
        public void ShowGradesView(object parameter)
        {
            CurrentView = new StudentGradesView(new StudentGradesVM(StudentId));
        }

        private ICommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (_logOutCommand == null)
                {
                    _logOutCommand = new RelayCommandsV2(LogOut);
                }
                return _logOutCommand;
            }
        }
        public void LogOut(object parameter)
        {
            Dependencies.MainWindowVM.ShowLogInView();
        }

        #endregion
    }
}
