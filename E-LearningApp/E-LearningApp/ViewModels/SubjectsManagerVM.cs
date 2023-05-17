using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E_LearningApp.ViewModels
{
    public class SubjectsManagerVM : BasePropertyChanged
    {
        private string _subjectToAdd;
        public string SubjectToAdd
        {
            get { return _subjectToAdd; }
            set { _subjectToAdd = value; NotifyPropertyChanged(nameof(SubjectToAdd)); }
        }

        private List<Subject> _subjects;
        public List<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; NotifyPropertyChanged(nameof(Subjects)); }
        }

        private Subject _selectedItem;
        public Subject SelectedItem
        {
            get { return _selectedItem; }
            set { 
                _selectedItem = value; 
                NotifyPropertyChanged(nameof(SelectedItem));
                if (value != null)
                {
                    ButtonsEnabled = true;
                }
                else
                {
                    ButtonsEnabled = false;
                }
            }
        }

        private SubjectHasThesisDto _selectedAssociation;
        public SubjectHasThesisDto SelectedAssociation
        {
            get { return _selectedAssociation; }
            set
            {
                _selectedAssociation = value;
                if (value == null)
                {
                    DeleteAssociationSCButtonV = false;
                }
                else
                {
                    DeleteAssociationSCButtonV = true;
                }
            }
        }

        private bool _deleteAssociationSCButtonV;
        public bool DeleteAssociationSCButtonV
        {
            get { return _deleteAssociationSCButtonV; }
            set { _deleteAssociationSCButtonV = value; NotifyPropertyChanged(nameof(DeleteAssociationSCButtonV)); }
        }

        private bool _buttonsEnabled;
        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set { _buttonsEnabled = value; NotifyPropertyChanged(nameof(ButtonsEnabled)); }
        }

        private List<Specialization> _specializationOptions;
        public List<Specialization> SpecializationOptions
        {
            get { return _specializationOptions; }
            set { _specializationOptions = value; NotifyPropertyChanged(nameof(SpecializationOptions)); }
        }

        public Specialization SelectedSpecialization { get; set; }

        public string SelectedYear { get; set; }

        public List<string> YearOptions { get; set; }

        public DisplayBooleanDto HaveThesis { get; set; }

        public List<DisplayBooleanDto> ThesisOptions { get; set; }

        private List<ClassCategorySubjectsDto> _classCategorySubjects;
        public List<ClassCategorySubjectsDto> ClassCategorySubjects
        {
            get { return _classCategorySubjects; }
            set { _classCategorySubjects = value; NotifyPropertyChanged(nameof(ClassCategorySubjects)); }
        }

        public SubjectsManagerBLL  SubjectsManagerBLL { get; set; }

        public SubjectsManagerVM()
        {
            SubjectsManagerBLL = new SubjectsManagerBLL();
            YearOptions = new List<string> { "9", "10", "11", "12" };
            ThesisOptions = new List<DisplayBooleanDto> {new DisplayBooleanDto { BooleanValue = true, StringValue = "true"}, new DisplayBooleanDto { BooleanValue = false, StringValue = "false" } };
            InitUi();
        }
        public void InitUi()
        {
            UpdateSubjectsUi();
            UpdateSpecializationsList();
            UpdateAssociationsList();
        }
        public void UpdateSubjectsUi()
        {
            Subjects = SubjectsManagerBLL.GetAllSubjects();
        }
        public void UpdateSpecializationsList()
        {
            SpecializationOptions = SubjectsManagerBLL.GetAllSpecializations();
        }
        public void UpdateAssociationsList()
        {
            ClassCategorySubjects = SubjectsManagerBLL.GetClassCategoryWithSubject();
        }

        #region commands

        private ICommand _addSubjectCommand;
        public ICommand AddSubjectCommand
        {
            get
            {
                if (_addSubjectCommand == null)
                {
                    _addSubjectCommand = new RelayCommandsV2(AddSubject);
                }
                return _addSubjectCommand;
            }
        }
        public void AddSubject(object parameter)
        {           
            if (SubjectsManagerBLL.AddSubject(SubjectToAdd))
            {
                UpdateSubjectsUi();
            }
            SubjectToAdd = "";
        }

        private ICommand _deleteSubjectCommand;
        public ICommand DeleteSubjectCommand
        {
            get
            {
                if (_deleteSubjectCommand == null)
                {
                    _deleteSubjectCommand = new RelayCommandsV2(DeleteSubject);
                }
                return _deleteSubjectCommand;
            }
        }
        public void DeleteSubject(object parameter)
        {
            if (SubjectsManagerBLL.DeleteSubject(SelectedItem))
            {
                UpdateSubjectsUi();
                UpdateAssociationsList();
            }
        }

        private ICommand _addAssociationCommand;
        public ICommand AddAssociationCommand
        {
            get
            {
                if (_addAssociationCommand == null)
                {
                    _addAssociationCommand = new RelayCommandsV2(AddAssociation);
                }
                return _addAssociationCommand;
            }
        }
        public void AddAssociation(object parameter)
        {
            if(HaveThesis != null)
            {
                SubjectsManagerBLL.AddAssociation(new SubjectClassCategoryAssociation { Specialization = SelectedSpecialization, Subject = SelectedItem, Thesis = HaveThesis.BooleanValue, YearOfStudy = SelectedYear });
                UpdateAssociationsList();
            }
        }

        private ICommand _deleteAssociationCommand;
        public ICommand DeleteAssociationCommand
        {
            get
            {
                if (_deleteAssociationCommand == null)
                {
                    _deleteAssociationCommand = new RelayCommandsV2(DeleteAssociation);
                }
                return _deleteAssociationCommand;
            }
        }
        public void DeleteAssociation(object parameter)
        {
            if (SelectedAssociation != null && SubjectsManagerBLL.DeleteSubjectClassCategoryAssociation(SelectedAssociation.Id))
            {
                UpdateAssociationsList();
            }
        }
        #endregion
    }
}
