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

namespace EventPlanner.Modals
{
    /// <summary>
    /// Interaction logic for ForgotPasswordModal.xaml
    /// </summary>
    public partial class ForgotPasswordModal : Window
    {
        public ForgotPasswordModal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Check your email for a reset link.", "Reset email", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
