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
                    switch (user.Item2)
                    {
                        case UserRole.Student:
                            Dependencies.MainWindowVM.ShowStudentView(new StudentVM(user.Item1));
                            break;
                        case UserRole.Professor:
                            break;
                        case UserRole.ClassMaster:
                            break;
                        case UserRole.Administrator:
                            Dependencies.MainWindowVM.ShowAdministratorsView(new AdministratorVM(user.Item1));
                            break;
                    }
                }
            }
            catch(LoginException exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }
        #endregion
    }
}
