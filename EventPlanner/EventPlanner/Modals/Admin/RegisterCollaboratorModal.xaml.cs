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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventPlanner.Modals.Admin
{
    /// <summary>
    /// Interaction logic for RegisterCollaboratorModal.xaml
    /// </summary>
    public partial class RegisterCollaboratorModal : Window
    {
        public RegisterCollaboratorModal()
        {
            InitializeComponent();
        }

        private void SubmitRegisterCollaboratorModalButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelRegisterCollaboratorModalButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
