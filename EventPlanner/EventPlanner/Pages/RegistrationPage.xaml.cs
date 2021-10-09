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

namespace EventPlanner.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.Singleton().ChangePage("Pages/SigninPage.xaml");
        }
    }
}
