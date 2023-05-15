using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.DataAccessLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class ClassesManagerVM : BasePropertyChanged
    {
        private ClassesManagerBLL ClassesManagerBLL { get; set; }
        public ObservableCollection<Specialization> SpecializationOptions { get; set; }
        public Specialization SelectedSpecialization { get; set; }

        private bool _buttonsEnabled;
        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set { _buttonsEnabled = value; NotifyPropertyChanged(nameof(ButtonsEnabled)); }
        }

        private ClassDto _selectedItem;
        public ClassDto SelectedItem
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

        private ObservableCollection<ClassDto> _classes;
        public ObservableCollection<ClassDto> Classes
        {
            get { return _classes; }
            set { _classes = value; NotifyPropertyChanged(nameof(Classes)); }
        }

        public ObservableCollection<EntityFullNameIdDto> ClassMasterOptions { get; set; }
        public EntityFullNameIdDto SelectedClassMaster { get; set; }

        public string ClassName { get; set; }
        private string _yearOfStudy;
        public string YearOfStudy { get; set; }
        public List<string> YearsOfStudy { get; set; }

        public ClassesManagerVM()
        {
            ClassesManagerBLL = new ClassesManagerBLL();
            YearsOfStudy = new List<string> { "9", "10", "11", "12" };
            InitializeUI();
        }
        private void InitializeUI()
        {
            SpecializationOptions = new ObservableCollection<Specialization>(ClassesManagerBLL.GetSpecializations());
            ClassMasterOptions = new ObservableCollection<EntityFullNameIdDto>(ClassesManagerBLL.GetClassMasters());
            UpdateClassesList();
        }

        private void UpdateClassesList()
        {
            Classes = new ObservableCollection<ClassDto>(ClassesManagerBLL.GetClasses());
        }

        #region commands
        private ICommand _addClassCommand;
        public ICommand AddClassCommand
        {
            get
            {
                if (_addClassCommand == null)
                {
                    _addClassCommand = new RelayCommandsV2(AddClass);
                }
                return _addClassCommand;
            }
        }
        public void AddClass(object parameter)
        {
            if (ClassName != null && SelectedSpecialization != null && YearOfStudy  != null && SelectedClassMaster != null)
            {
                Class _class = new Class { Name = ClassName, SpecializationId = SelectedSpecialization.Id, YearOfStudy = YearOfStudy, ClassMasterId = SelectedClassMaster.Id };
                if (!ClassesManagerBLL.AddClass(_class)) 
                { MessageBox.Show("Name already taken", "Error"); }
                else
                {
                    UpdateClassesList();
                }
            }
            else
            {
                MessageBox.Show("Complete all fields", "Error");
            }
        }

        private ICommand _deleteClassCommand;
        public ICommand DeleteClassCommand
        {
            get
            {
                if (_deleteClassCommand == null)
                {
                    _deleteClassCommand = new RelayCommandsV2(DeleteClass);
                }
                return _deleteClassCommand;
            }
        }
        public void DeleteClass(object parameter)
        {
            if (SelectedItem != null)
            {
                ClassesManagerBLL.DeleteClass(SelectedItem);
                UpdateClassesList();
            }
        }

        #endregion 
    }
}
