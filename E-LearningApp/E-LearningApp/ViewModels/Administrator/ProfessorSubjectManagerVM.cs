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
    public class ProfessorSubjectManagerVM : BasePropertyChanged
    {

        private ProfessorSubjectManagerBLL ProfessorSubjectManagerBLL { get; set; }

        public ProfessorSubjectManagerVM()
        {
            ProfessorSubjectManagerBLL = new ProfessorSubjectManagerBLL();
            UpdateProfessorSubjectList();
            UpdateProfessorOptions();
            UpdateClassOptions();
        }

        private List<ProfessorSubjectDto> _professorSubjectsList;
        public List<ProfessorSubjectDto> ProfessorSubjectsList
        {
            get { return _professorSubjectsList; }
            set { _professorSubjectsList = value; NotifyPropertyChanged(nameof(ProfessorSubjectsList)); }
        }


        private List<EntityFullNameIdDto> _professorOptions;
        public List<EntityFullNameIdDto> ProfessorOptions
        {
            get { return _professorOptions; }
            set { _professorOptions = value; NotifyPropertyChanged(nameof(ProfessorOptions)); }
        }

        public EntityFullNameIdDto SelectedProfessor { get; set; }


        private List<Class> _classOptions;
        public List<Class> ClasssOptions
        {
            get { return _classOptions; }
            set { _classOptions = value; NotifyPropertyChanged(nameof(ClasssOptions)); }
        }
        private Class _selectedClass;
        public Class SelectedClass
        {
            get { return _selectedClass; }
            set { _selectedClass = value; UpdateSubjectsList();}
        }


        private List<Subject> _subjectOptions;
        public List<Subject> SubjectOptions
        {
            get { return _subjectOptions; }
            set { _subjectOptions = value; NotifyPropertyChanged(nameof(SubjectOptions)); }
        }

        public Subject SelectedSubject { get; set; }

        private ProfessorSubjectDto _selectedProfessorSubject;
        public ProfessorSubjectDto SelectedProfessorSubject
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

        private bool _professorSubjectButtonVisibility;
        public bool ProfessorSubjectButtonVisibility
        {
            get { return _professorSubjectButtonVisibility; }
            set { _professorSubjectButtonVisibility = value; NotifyPropertyChanged(nameof(ProfessorSubjectButtonVisibility)); }
        }

        public void UpdateProfessorSubjectList()
        {
            ProfessorSubjectsList = ProfessorSubjectManagerBLL.GetProfessorsAndSubjects();
        }

        private void UpdateProfessorOptions()
        {
            ProfessorOptions = ProfessorSubjectManagerBLL.GetProfessorsFullNameDto();
        }

        private void UpdateSubjectsList()
        {
            if (SelectedClass != null)
            {
                SubjectOptions = ProfessorSubjectManagerBLL.GetSubjectForSpecializationAndYear(SelectedClass.SpecializationId, SelectedClass.YearOfStudy);
            }         
        }
        private void UpdateClassOptions()
        {
            ClasssOptions = ProfessorSubjectManagerBLL.GetClasses();
        }

        #region commands

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
                ProfessorSubjectManagerBLL.DeleteProfessorSubjectAssociation(SelectedProfessorSubject.Id);
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
            if (SelectedProfessor != null && SelectedClass != null && SelectedSubject != null)
            {
                ProfessorSubjectManagerBLL.AddProfessorSubjectAssociation(SelectedProfessor.Id, SelectedSubject, SelectedClass.Id);
                UpdateProfessorSubjectList();
            }
        }

        #endregion
    }
}
