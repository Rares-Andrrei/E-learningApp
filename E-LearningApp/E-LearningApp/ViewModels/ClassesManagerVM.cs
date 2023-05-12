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
        public Class SelectedItem { get; set; }

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
        public string YearOfStudy
        {
            get { return _yearOfStudy; }
            set {
                int number;
                if (value.Length <= 2 && int.TryParse(value, out number))
                {
                    if (number > 0 && number <= 12)
                    {
                        _yearOfStudy = value;
                    }
                }
                else if (value == string.Empty)
                {
                    _yearOfStudy = value;
                }
            }
        }

        public ClassesManagerVM()
        {
            ClassesManagerBLL = new ClassesManagerBLL();
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
        #endregion
    }
}
