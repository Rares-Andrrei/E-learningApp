using E_LearningApp.Models.BusinessLogicLayer.ProfessorF;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Settings;
using E_LearningApp.ViewModels.Commands;
using E_LearningApp.Views.ProfessorF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace E_LearningApp.ViewModels.ProfessorF
{
    public class ProfessorVM : BasePropertyChanged
    {
        public ProfessorBLL ProfessorBLL { get; set; }
        public int ProfessorId { get; set; }

        public ProfessorVM(int userId) 
        {
            ProfessorBLL = new ProfessorBLL();
            Email = ProfessorBLL.GetProfessorEmailByUserId(userId);
            ProfessorId = ProfessorBLL.GetProfessorIdByUserId(userId);
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; NotifyPropertyChanged(nameof(CurrentView)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }


        #region commands

        private ICommand _showManageAbsencesCommand;
        public ICommand ShowManageAbsencesCommand
        {
            get
            {
                if (_showManageAbsencesCommand == null)
                {
                    _showManageAbsencesCommand = new RelayCommandsV2(ShowManageAbsences);
                }
                return _showManageAbsencesCommand;
            }
        }
        public void ShowManageAbsences(object parameter)
        {
            CurrentView = new ManageAbencesView(new ManageAbsencesVM(ProfessorId));
        }

        private ICommand _showManageGradesCommand;
        public ICommand ShowManageGradesCommand
        {
            get
            {
                if (_showManageGradesCommand == null)
                {
                    _showManageGradesCommand = new RelayCommandsV2(ShowManageGrades);
                }
                return _showManageGradesCommand;
            }
        }
        public void ShowManageGrades(object parameter)
        {
            CurrentView = new ManageGradesView(new ManageGradesVM(ProfessorId));
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
