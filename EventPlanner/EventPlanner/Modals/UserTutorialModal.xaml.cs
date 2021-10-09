using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventPlanner.Modals
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserTutorialModal : Window
    {
        public UserTutorialModal()
        {
            InitializeComponent();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Close();
        }
    }
}
