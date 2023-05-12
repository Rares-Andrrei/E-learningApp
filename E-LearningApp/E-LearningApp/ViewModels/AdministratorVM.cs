using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using E_LearningApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class AdministratorVM : BasePropertyChanged
    { 
        public AdministratorVM(int id)
        {
            AdminId = id;
            AdministratorBLL = new AdministratorBLL();
        }

        private AdministratorBLL AdministratorBLL { get; set; }
        public int AdminId { get; set; }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; NotifyPropertyChanged(nameof(CurrentView)); }
        }


        private string _email;
        public string Email
        {
            get { 
                if (_email == null)
                {
                    _email = AdministratorBLL.GetEmailById(AdminId);
                }
                return _email;
            }
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }

        #region Command Members
        private ICommand _showAddSpecializationCommand;
        public ICommand ShowAddSpecializationCommand
        {
            get
            {
                if (_showAddSpecializationCommand == null)
                {
                    _showAddSpecializationCommand = new RelayCommandsV2(ShowAddSpecialization);
                }
                return _showAddSpecializationCommand;
            }
        }
        public void ShowAddSpecialization(object parameter)
        {
            CurrentView = new SpecializationsManagerView();
        }

        private ICommand _showClassesManagerCommand;
        public ICommand ShowClassesManagerCommand
        {
            get
            {
                if (_showClassesManagerCommand == null)
                {
                    _showClassesManagerCommand = new RelayCommandsV2(ShowClassesManager);
                }
                return _showClassesManagerCommand;
            }
        }
        public void ShowClassesManager(object parameter)
        {
            CurrentView = new ClassesManagerView();
        }

        private ICommand _showUsersManagerCommand;
        public ICommand ShowUsersManagerCommand
        {
            get
            {
                if (_showUsersManagerCommand == null)
                {
                    _showUsersManagerCommand = new RelayCommandsV2(ShowUsersManager);
                }
                return _showUsersManagerCommand;
            }
        }
        public void ShowUsersManager(object parameter)
        {
            CurrentView = new UsersManagerView();
        }
        #endregion
    }
}
