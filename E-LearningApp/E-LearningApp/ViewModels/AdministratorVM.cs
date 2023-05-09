using E_LearningApp.Models.BusinessLogicLayer;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.ViewModels
{
    public class AdministratorVM : BasePropertyChanged
    {
        public AdministratorBLL AdministratorBLL { get; set; }
        public AdministratorVM(int id)
        {
            AdminId = id;
            AdministratorBLL = new AdministratorBLL();
        }

        public int AdminId { get; set; }
        private string _email;
        public string Email
        {
            get { 
                if (_email == null)
                {
                    _email = AdministratorBLL.GetEmailById(AdminId);
                }
                return _email;
            }
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }
    }
}
