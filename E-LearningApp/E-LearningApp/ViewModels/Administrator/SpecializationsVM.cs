using E_LearningApp.Exceptions;
using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.DataAccessLayer;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using E_LearningApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace E_LearningApp.ViewModels
{
    public class SpecializationsVM : BasePropertyChanged
    {
        private SpecializationsBLL SpecializationsBLL { get; set; }

        private ObservableCollection<Specialization> _specializations;
        public ObservableCollection<Specialization> Specializations
        {
            get { return _specializations; }
            set { _specializations = value; NotifyPropertyChanged(nameof(Specializations)); }
        }
        private Specialization _selectedItem;
        public Specialization SelectedItem
        {
            get { return _selectedItem; }
            set { 
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
        private bool _buttonsEnabled;
        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set { _buttonsEnabled = value;  NotifyPropertyChanged(nameof(ButtonsEnabled)); }
         }

        private string _specializationName;
        public string SpecializationName
        {
            get { return _specializationName; }
            set { _specializationName = value; NotifyPropertyChanged(nameof(SpecializationName));}
        }

        public SpecializationsVM() 
        {
            SpecializationsBLL = new SpecializationsBLL();
            UpdateSpecilaizationsList();
        }
        private void UpdateSpecilaizationsList()
        {
            Specializations = new ObservableCollection<Specialization>(SpecializationsBLL.GetSpecializations());
        }

        #region Command Members
        private ICommand _addSpecializationCommand;
        public ICommand AddSpecializationCommand
        {
            get
            {
                if (_addSpecializationCommand == null)
                {
                    _addSpecializationCommand = new RelayCommandsV2(AddSpecialization);
                }
                return _addSpecializationCommand;
            }
        }
        public void AddSpecialization(object parameter)
        {
            try
            {
                SpecializationsBLL.AddSpecialization(SpecializationName);
                UpdateSpecilaizationsList();
                SpecializationName = string.Empty;
            }
            catch (InputException exception) 
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private ICommand _deleteSpecializationCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteSpecializationCommand == null)
                {
                    _deleteSpecializationCommand = new RelayCommandsV2(DeleteSpecialization);
                }
                return _deleteSpecializationCommand;
            }
        }
        public void DeleteSpecialization(object parameter)
        {
            if (SelectedItem != null)
            {
                SpecializationsBLL.DeleteSpecialization(SelectedItem);
                UpdateSpecilaizationsList();
            }
        }


        private ICommand _cellEditEndingCommand;
        public ICommand CellEditEndingCommand
        {
            get
            {
                if (_cellEditEndingCommand == null)
                {
                    _cellEditEndingCommand = new RelayCommandsV2(CellEditEnding);
                }
                return _cellEditEndingCommand;
            }
        }

        public void CellEditEnding(object parameter)
        {
            if (parameter is DataGridCellEditEndingEventArgs e)
            {
                var cell = e.EditingElement as TextBox;
                var binding = cell?.GetBindingExpression(TextBox.TextProperty);

                if (binding != null)
                {
                    if (cell.Text == SelectedItem.Name || SpecializationsBLL.AlreadyExists(cell.Text))
                    {
                        cell.Text = SelectedItem.Name;
                    }
                    else
                    {
                        Specialization editedSpecialzation = new Specialization
                        {
                            Id = SelectedItem.Id,
                            Name = cell.Text
                        };
                        SpecializationsBLL.UpdateSpecialization(editedSpecialzation);
                    }
                }
            }
        }
        #endregion 
    }
}
