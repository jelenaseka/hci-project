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
    /// Interaction logic for RegisterOrganizerModal.xaml
    /// </summary>
    public partial class RegisterOrganizerModal : Window
    {
        public RegisterOrganizerModal()
        {
            InitializeComponent();
        }

        private void CancelProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitProfileModalButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
