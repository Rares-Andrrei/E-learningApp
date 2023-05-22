﻿using E_LearningApp.ViewModels.ProfessorF;
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

namespace E_LearningApp.Views.ProfessorF
{
    /// <summary>
    /// Interaction logic for ManageGradesView.xaml
    /// </summary>
    public partial class ManageGradesView : UserControl
    {
        public ManageGradesView(ManageGradesVM manageGradesView)
        {
            InitializeComponent();
            DataContext = manageGradesView;
        }
    }
}
