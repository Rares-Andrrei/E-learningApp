using E_LearningApp.Models.BusinessLogicLayer.ProfessorF;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using E_LearningApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E_LearningApp.ViewModels.ProfessorF
{
    public class ManageAbsencesVM :BasePropertyChanged
    {
        public int ProfessorId { get; set; }

        public ProfessorBLL ProfessorBLL { get; set; }

        private List<ClassDto> classOptions { get; set; }
        public List<ClassDto> ClassOptions
        {
            get { return classOptions; }
            set
            {
                classOptions = value;
                NotifyPropertyChanged(nameof(classOptions));
            }
        }

        private ClassDto _selectedItem;
        public ClassDto SelectedClass
        {
            get { return _selectedItem; }
            set { 
                _selectedItem = value;
                if (value != null)
                {
                    SubjectOptions = ProfessorBLL.SubjectsProfessorClass(ProfessorId, SelectedClass.Id);
                    StudentsOptions = ProfessorBLL.GetStudentsFromClass(SelectedClass.Id);
                }
                else
                {
                    SubjectOptions = null;
                    StudentsOptions = null;
                }
            }
        }
        private List<Subject> _subjectOptions;
        public List<Subject> SubjectOptions
        {
            get { return _subjectOptions; }
            set
            {
                _subjectOptions = value;
                NotifyPropertyChanged(nameof(SubjectOptions));
            }
        }
        
        private Subject _selectedSubject;
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set { 
                _selectedSubject = value;
                if (value != null && SelectedClass != null)
                {
                    StudentsAndAbsences = ProfessorBLL.GetStudentAbsences(SelectedClass.Id, SelectedSubject.Id);
                }
            }
        }

        private List<EntityFullNameIdDto> _studentOptionsDto;
        public List<EntityFullNameIdDto> StudentsOptions
        {
            get { return _studentOptionsDto; }
            set
            {
                _studentOptionsDto = value;
                NotifyPropertyChanged(nameof(StudentsOptions));
            }
        }

        public int SelectedSemester { get; set; }
        public List<int> SemesterOptions { get; set; }

        private EntityFullNameIdDto _selectedStudent;
        public EntityFullNameIdDto SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                NotifyPropertyChanged(nameof(SelectedStudent));
            }
        }

        public DateTime Date { get; set; }

        private AbsenceInfoDto _selectedAbsence;
        public AbsenceInfoDto SelectedAbsence
        {
            get { return _selectedAbsence; }
            set {  
                _selectedAbsence = value;
                if (value!=null)
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
            set {  _buttonsEnabled = value; NotifyPropertyChanged(nameof(ButtonsEnabled)); }
        }


        private List<StudentAbsencesDto> _studentsAndAbsences;
        public List<StudentAbsencesDto> StudentsAndAbsences
        {
            get { return _studentsAndAbsences; }
            set
            {
                _studentsAndAbsences = value;
                NotifyPropertyChanged(nameof(StudentsAndAbsences));
            }
        }

        public ManageAbsencesVM(int professorId) 
        {
            ProfessorId = professorId;
            ProfessorBLL = new ProfessorBLL();
            ClassOptions = ProfessorBLL.GetClassesForProfessor(ProfessorId);
            SemesterOptions = new List<int> { 1, 2 };
        }

        public void UpdateAbsencesList()
        {
            StudentsAndAbsences = ProfessorBLL.GetStudentAbsences(SelectedClass.Id, SelectedSubject.Id);
        }


        private ICommand _addAbsenceCommand;
        public ICommand AddAbsenceCommand
        {
            get
            {
                if (_addAbsenceCommand == null)
                {
                    _addAbsenceCommand = new RelayCommandsV2(AddAbsence);
                }
                return _addAbsenceCommand;
            }
        }
        public void AddAbsence(object parameter)
        {
            if (SelectedStudent != null && SelectedSubject != null && Date.Year >= DateTime.Now.Year - 200 && SelectedSemester != 0)
            {
                ProfessorBLL.AddAbsence(new StudentAbsenceAssociation { AbsenceDate = Date, Reasoned = false, StudentId = SelectedStudent.Id, SubjectId = SelectedSubject.Id, Semester = SelectedSemester });
                UpdateAbsencesList();
            }
        }

        private ICommand _motivateAbsenceCommand;
        public ICommand MotivateAbsenceCommand
        {
            get
            {
                if (_motivateAbsenceCommand == null)
                {
                    _motivateAbsenceCommand = new RelayCommandsV2(MotivateAbsence);
                }
                return _motivateAbsenceCommand;
            }
        }
        public void MotivateAbsence(object parameter)
        {
            ProfessorBLL.MotivateAbsence(SelectedAbsence.Id);
            UpdateAbsencesList();

        }

        private ICommand _deleteAbsenceCommand;
        public ICommand DeleteAbsenceCommand
        {
            get
            {
                if (_deleteAbsenceCommand == null)
                {
                    _deleteAbsenceCommand = new RelayCommandsV2(DeleteAbsenceC);
                }
                return _deleteAbsenceCommand;
            }
        }
        public void DeleteAbsenceC(object parameter)
        {
            ProfessorBLL.DeleteAbsence(SelectedAbsence.Id);
            UpdateAbsencesList();
        }

    }
}
