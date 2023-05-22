using E_LearningApp.Components;
using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Models.Enums;
using E_LearningApp.Settings;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class LoginVM : BasePropertyChanged
    {
        private LoginBLL LoginBLL { get; set; }
        private int CurrentUserId { get; set; }
        public LoginVM()
        {
            LoginBLL = new LoginBLL();
        }
        #region Data Members
        public string UserName { get; set; }
        public string Password { get; set; }
        #endregion


        #region Command Members

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommandsV2(Login);
                }
                return loginCommand;
            }
        }
        public void Login(object parameter)
        {
            try
            {
                Tuple<int, UserRole> user = LoginBLL.Login(UserName, Password);
                if (user != null)
                {
                    CurrentUserId = user.Item1;
                    switch (user.Item2)
                    {
                        case UserRole.Student:
                            Dependencies.MainWindowVM.ShowStudentView(new StudentVM(CurrentUserId));
                            break;
                        case UserRole.Professor:
                            Dependencies.MainWindowVM.ShowProfessorView(CurrentUserId);
                            break;
                        case UserRole.ClassMaster:
                            Dependencies.MainWindowVM.ShowClassMasterOptions(this);
                            break;
                        case UserRole.Administrator:
                            Dependencies.MainWindowVM.ShowAdministratorsView(new AdministratorVM(CurrentUserId));
                            break;
                    }
                }
            }
            catch (LoginException exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

            private ICommand _showProfessorViewCommand;
            public ICommand ShowProfessorViewCommand
            {
                get
                {
                    if (_showProfessorViewCommand == null)
                    {
                        _showProfessorViewCommand = new RelayCommandsV2(ShowProfessorView);
                    }
                    return _showProfessorViewCommand;
                }
            }
            public void ShowProfessorView(object parameter)
            {
                Dependencies.MainWindowVM.ShowProfessorView(CurrentUserId);
            }


        private ICommand _showClassMasterView;
        public ICommand ShowClassMasterViewCommand
        {
            get
            {
                if (_showClassMasterView == null)
                {
                    _showClassMasterView = new RelayCommandsV2(ShowClassMasterView);
                }
                return _showClassMasterView;
            }
        }
        public void ShowClassMasterView(object parameter)
        {
            Dependencies.MainWindowVM.ShowClassMasterView(CurrentUserId);
        }

        #endregion
    }
}
