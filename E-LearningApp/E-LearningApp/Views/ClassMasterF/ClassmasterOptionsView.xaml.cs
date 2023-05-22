using E_LearningApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_LearningApp.Views.ClassMasterF
{
    /// <summary>
    /// Interaction logic for ClassmasterOptionsView.xaml
    /// </summary>
    public partial class ClassmasterOptionsView : UserControl
    {
        public ClassmasterOptionsView(LoginVM loginVM)
        {
            InitializeComponent();
            DataContext = loginVM;
        }
    }
}
