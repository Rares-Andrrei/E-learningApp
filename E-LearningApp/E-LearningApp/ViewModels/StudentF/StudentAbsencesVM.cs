using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.Dtos;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.ViewModels
{
    public class StudentAbsencesVM : BasePropertyChanged
    {
        StudentAbsenceAssociationBLL StudentAbsenceAssociationBLL { get; set; }
        private List<SubejctAbsenceDisplayDto> _studentAbsenceAssociations;
        public List<SubejctAbsenceDisplayDto> StudentAbsenceAssociations
        {
            get { return _studentAbsenceAssociations; }
            set { _studentAbsenceAssociations = value; NotifyPropertyChanged(nameof(StudentAbsenceAssociations)); }
        }
        public int StudentId { get; set; }

        public StudentAbsencesVM(int studentd)
        {
            StudentAbsenceAssociationBLL = new StudentAbsenceAssociationBLL();
            StudentId = studentd;
            InitAbsencesList();
        }
        public void InitAbsencesList()
        {
            StudentAbsenceAssociations = StudentAbsenceAssociationBLL.GetAbsencesToDisplay(StudentId);
        }
    }
}
