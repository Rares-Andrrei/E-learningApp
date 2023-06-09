﻿using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.Models.Enums;
using E_LearningApp.ViewModels.Commands;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class UsersManagerVM : BasePropertyChanged
    {
        UsersManagerBLL UsersManagerBLL { get; set; }

        public UsersManagerVM()
        {
            BirthDate = DateTime.Now;
            UsersManagerBLL = new UsersManagerBLL();
            ClassList = UsersManagerBLL.GetClassesDto();
            RoleList = UsersManagerBLL.GetUserRoles();
            UpdateUiList();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        private bool _selectClassVisibility;
        public bool SelectClassVisibility
        {
            get { return _selectClassVisibility; }
            set { _selectClassVisibility = value; NotifyPropertyChanged(nameof(SelectClassVisibility)); }
        }
        private RoleDto _role;
        public RoleDto Role
        {
            get { return _role;}
            set
            {
                _role = value;
                if (value.UserRole != UserRole.Student)
                {
                    Class = null;
                    SelectClassVisibility = false;
                }
                else
                {
                    SelectClassVisibility = true;
                }
            }
        }

        private bool _buttonsEnabled;
        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set { _buttonsEnabled = value; NotifyPropertyChanged(nameof(ButtonsEnabled)); }
        }

        private UserDisplayDto _selectedItem;
        public UserDisplayDto SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    ButtonsEnabled = true;
                }
                else
                {
                    ButtonsEnabled = false;
                }
            }
        }

        private ClassDto _class;
        public ClassDto Class
        {
            get { return _class; }
            set { _class = value; NotifyPropertyChanged(nameof(Class)); }
        }

        private List<ClassDto> _classList;
        public List<ClassDto> ClassList
        {
            get { return _classList; }
            set { _classList = value; NotifyPropertyChanged(nameof(ClassList)); }
        }

        private List<RoleDto> _rolesList;
        public List<RoleDto> RoleList
        {
            get { return _rolesList; }
            set { _rolesList = value; NotifyPropertyChanged(nameof(RoleList)); }
        }

        private List<UserDisplayDto> _userDisplayDtos;
        public List<UserDisplayDto> UserDisplayDtos
        {
            get { return _userDisplayDtos; }
            set { _userDisplayDtos = value; NotifyPropertyChanged(nameof(UserDisplayDtos)); }
        }

        public void UpdateUiList()
        {
            UserDisplayDtos = UsersManagerBLL.GetUserDisplayDtos();
        }

        #region commands


        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (_addUserCommand == null)
                {
                    _addUserCommand = new RelayCommandsV2(AddUser);
                }
                return _addUserCommand;
            }
        }
        public void AddUser(object parameter)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                  BirthDate == null || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                  Role == null)
            {
                MessageBox.Show("Complete all fiedls", " Error");
                return;
            }
            else if (BirthDate.Year <= DateTime.Now.Year - 200 || BirthDate.Year >= DateTime.Now.Year)
            {
                MessageBox.Show("Invalid date", " Error");
                return;
            }
            else if (!Regex.IsMatch(Email, pattern))
            {
                MessageBox.Show("Invalid e-mail adress", " Error");
                return;
            }
            bool succes;
            switch (Role.UserRole)
            {
                case (UserRole.Student):
                    if (Class == null)
                    {
                        MessageBox.Show("Complete all fiedls", " Error");
                        return;
                    }
                    succes = UsersManagerBLL.AddNewStudent(new Student
                    {
                        PersonalData = new Person { FirstName = this.FirstName, LastName = this.LastName, BirthDate = this.BirthDate },
                        User = new User { Email = this.Email, Password = this.Password, Role = this.Role.UserRole },
                        ClassId = Class.Id
                    });
                    break;
                default:
                   succes =  UsersManagerBLL.AddNewProfessor(new Professor
                    {
                        PersonalData = new Person { FirstName = this.FirstName, LastName = this.LastName, BirthDate = this.BirthDate },
                        User = new User { Email = this.Email, Password = this.Password, Role = this.Role.UserRole }
                    });
                    break;
            }
            if (!succes)
            {
                MessageBox.Show("E-mail already taken", " Error");
            }
            else
            {
                UpdateUiList();
            }
        }

        private ICommand _deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (_deleteUserCommand == null)
                {
                    _deleteUserCommand = new RelayCommandsV2(DeleteClass);
                }
                return _deleteUserCommand;
            }
        }
        public void DeleteClass(object parameter)
        {
            if (SelectedItem != null)
            {
                UsersManagerBLL.DeleteUser(SelectedItem);
                UpdateUiList();
            }
        }

        #endregion 
    }
}
