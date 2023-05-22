using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.BusinessLogicLayer.ProfessorF;
using E_LearningApp.Models.DataAccessLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace E_LearningApp.ViewModels.ProfessorF
{
    public class ManageGradesVM : BasePropertyChanged
    {
        public ProfessorBLL ProfessorBLL { get; set; }
        private List<ProfessorClassGradesDto> displayedGrades;
        public List<ProfessorClassGradesDto> DisplayedGrades
        {
            get { return displayedGrades; }
            set { displayedGrades = value; NotifyPropertyChanged(nameof(DisplayedGrades)); }
        }

        private List<StudentFullNameClassDto> _studentOptions;
        public List<StudentFullNameClassDto> StudentOptions
        {
            get { return _studentOptions; }
            set { _studentOptions = value; NotifyPropertyChanged(nameof(StudentOptions)); }
        }
        private StudentFullNameClassDto _selectedStudent;
        public StudentFullNameClassDto SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                if (value != null)
                {
                    SubjectOptions = ProfessorBLL.GetSubjectOptionsForStudent(ProfessorId, value.ClassId.Value);
                }
            }
        }

        private GradeDto _selectedGradeFromTable;
        public GradeDto SelectedGradeFromTable
        {
            get { return _selectedGradeFromTable; }
            set {
                _selectedGradeFromTable = value; 
                if (value != null)
                {
                    EnableDelete = true;
                }
                else
                {
                    EnableDelete = false;
                }
            }
        }
        private bool _enableDelete;
        public bool EnableDelete
        {
            get { return _enableDelete; }
            set { _enableDelete = value;
            NotifyPropertyChanged(nameof(EnableDelete));}
        }

        public DateTime Date { get; set; }

        public int ProfessorId { get; set; }
        private List<int> _gradeOptions;
        public List<int> GradeOptions
        {
            get { return _gradeOptions; }
            set { _gradeOptions = value; NotifyPropertyChanged(nameof(GradeOptions)); }
        }
        public int SelectedGrade { get; set; }
        private List<SubjectHasThesisDto> _subjectOptions;
        public List<SubjectHasThesisDto> SubjectOptions
        {
            get { return _subjectOptions; }
            set { _subjectOptions = value; NotifyPropertyChanged(nameof(SubjectOptions)); }
        }
        private SubjectHasThesisDto _selectedSubject;
        public SubjectHasThesisDto SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                if (value != null && value.HasThesis)
                {
                    ThesisOptionsVisibility = true;
                }
                else
                {
                    ThesisOptionsVisibility = false;
                }
            }
        }

        public List<int> SemesterOptions { get; set; }
        public int SelectedSemester { get; set; }
        public List<DisplayBooleanDto> ThesisOptions {get;set;}
        public DisplayBooleanDto IsThesis { get; set; }
        private bool _thesisOptionsVisibility;
        public bool ThesisOptionsVisibility
        {
            get { return _thesisOptionsVisibility; }
            set { _thesisOptionsVisibility = value;
            NotifyPropertyChanged(nameof(ThesisOptionsVisibility));}
        }


        public ManageGradesVM(int professorId)
        {
            ProfessorId = professorId;
            ProfessorBLL = new ProfessorBLL();
            UpdateDisplayedGradesList();
            StudentOptions = ProfessorBLL.GetStudents(ProfessorId);
            GradeOptions = new List<int>{1,2, 3, 4, 5, 6, 7, 8, 9, 10};
            SemesterOptions = new List<int> { 1, 2, };
            ThesisOptions = new List<DisplayBooleanDto> { new DisplayBooleanDto { BooleanValue = true, StringValue = "true" }, new DisplayBooleanDto { BooleanValue = false, StringValue = "false" }, };
        }
        public void UpdateDisplayedGradesList()
        {
            DisplayedGrades = ProfessorBLL.GetProfessorsSubjectsAndGrades(ProfessorId);
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
            if (SelectedGrade != 0 && SelectedStudent != null && SelectedSubject != null && SelectedSemester != 0 && IsThesis != null && Date != null)
            {
                ProfessorBLL.AddStudentGrade(new StudentGradesAssociation { Grade = SelectedGrade, StudentId = SelectedStudent.Id, SubjectId = SelectedSubject.Subject.Id, Date = Date, Semester = SelectedSemester, Thesis = IsThesis.BooleanValue });
                UpdateDisplayedGradesList();
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
            if (SelectedGradeFromTable != null)
            {
                ProfessorBLL.DeleteGrade(SelectedGradeFromTable.Id);
                UpdateDisplayedGradesList();
            }
        }



        #endregion

    }
}
