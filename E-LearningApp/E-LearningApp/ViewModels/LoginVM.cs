using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Security;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class LoginVM : BasePropertyChanged
    {
        public LoginBLL LoginBLL { get; set; }
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

        }
        #endregion
    }
}
