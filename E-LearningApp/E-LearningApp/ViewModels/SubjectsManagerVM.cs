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

        private List<EntityFullNameIdDto> _professorOptions;
        public List<EntityFullNameIdDto> ProfessorOptions
        {
            get { return _professorOptions; }
            set { _professorOptions = value; NotifyPropertyChanged(nameof(ProfessorOptions)); }
        }

        public EntityFullNameIdDto SelectedProfessor { get; set; } 

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

        public DisplayBoolean HaveThesis { get; set; }

        public List<DisplayBoolean> ThesisOptions { get; set; }

        private List<ClassCategorySubjects> _classCategorySubjects;
        public List<ClassCategorySubjects> ClassCategorySubjects
        {
            get { return _classCategorySubjects; }
            set { _classCategorySubjects = value; NotifyPropertyChanged(nameof(ClassCategorySubjects)); }
        }

        private List<ProfessorSubject> _professorSubjectsList;
        public List<ProfessorSubject> ProfessorSubjectsList
        {
            get { return _professorSubjectsList; }
            set { _professorSubjectsList = value; NotifyPropertyChanged(nameof(ProfessorSubjectsList)); }
        }

        public SubjectsManagerBLL  SubjectsManagerBLL { get; set; }

        private bool _professorSubjectButtonVisibility;
        public bool ProfessorSubjectButtonVisibility
        {
            get { return _professorSubjectButtonVisibility; }
            set { _professorSubjectButtonVisibility = value; NotifyPropertyChanged(nameof(ProfessorSubjectButtonVisibility)); }
        }

        private ProfessorSubject _selectedProfessorSubject;
        public ProfessorSubject SelectedProfessorSubject
        {
            get { return _selectedProfessorSubject; }
            set
            {
                _selectedProfessorSubject = value;
                if (value != null)
                {
                    ProfessorSubjectButtonVisibility = true;
                }
                else
                {
                    ProfessorSubjectButtonVisibility = false;
                }
            }
        }

        public SubjectsManagerVM()
        {
            SubjectsManagerBLL = new SubjectsManagerBLL();
            YearOptions = new List<string> { "9", "10", "11", "12" };
            ThesisOptions = new List<DisplayBoolean> {new DisplayBoolean { BooleanValue = true, StringValue = "true"}, new DisplayBoolean { BooleanValue = false, StringValue = "false" } };
            InitUi();
        }
        public void InitUi()
        {
            UpdateSubjectsUi();
            UpdateSpecializationsList();
            UpdateAssociationsList();
            UpdateProfessorOptions();
            UpdateProfessorSubjectList();
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
        public void UpdateProfessorOptions()
        {
            ProfessorOptions = SubjectsManagerBLL.GetProfessorsFullNameDto();
        }
        public void UpdateProfessorSubjectList()
        {
            ProfessorSubjectsList = SubjectsManagerBLL.GetProfessorsAndSubjects();
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
                UpdateProfessorSubjectList();
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
            UpdateAssociationsList();
        }

        private ICommand _deleteProfessorAssociation;
        public ICommand DeleteProfessorAssociationCommand
        {
            get
            {
                if (_deleteProfessorAssociation == null)
                {
                    _deleteProfessorAssociation = new RelayCommandsV2(DeleteProfessorAssociation);
                }
                return _deleteProfessorAssociation;
            }
        }
        public void DeleteProfessorAssociation(object parameter)
        {
            if (SelectedProfessorSubject != null)
            {
                SubjectsManagerBLL.DeleteProfessorSubjectAssociation(SelectedProfessorSubject.Id);
                UpdateProfessorSubjectList();
            }     
        }

        private ICommand _addProfessorAssociation;
        public ICommand AddProfessorAssociationCommand
        {
            get
            {
                if (_addProfessorAssociation == null)
                {
                    _addProfessorAssociation = new RelayCommandsV2(AddProfessorAssociation);
                }
                return _addProfessorAssociation;
            }
        }
        public void AddProfessorAssociation(object parameter)
        {
            if (SelectedProfessor != null)
            {
                SubjectsManagerBLL.AddProfessorSubjectAssociation(SelectedProfessor.Id, SelectedItem);
                UpdateProfessorSubjectList();
            }
        }


        #endregion
    }
}
