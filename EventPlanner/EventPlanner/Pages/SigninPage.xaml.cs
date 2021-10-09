using EventPlanner.Modals;
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

namespace EventPlanner.Pages
{
    /// <summary>
    /// Interaction logic for SigninPage.xaml
    /// </summary>
    public partial class SigninPage : Page
    {
        public SigninPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = UserService.Singleton();
            if (userService.Login(usernameTextbox.Text, passwordTextBox.Password))
            {
                string page;
                if (userService.CurrentUser is Models.Admin)
                {
                    page = "Pages/Admin/Homepage.xaml";
                }
                else if (userService.CurrentUser is Models.Organizer)
                {
                    page = "Pages/Organizer/Homepage.xaml";
                }
                else
                {
                    page = "Pages/User/Homepage.xaml";
                }
                Services.NavigationService.Singleton().ChangePage(page);
            }
            else
            {
                MessageBox.Show("Failed to login with these credentials.");
            }
        }

        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
                Services.NavigationService.Singleton().ChangePage("Pages/RegistrationPage.xaml");
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordModal forgotPasswordModal = new ForgotPasswordModal();
            forgotPasswordModal.Show();
        }

        private void passwordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                loginBtn.Focus();
            }
        }

        private void usernameTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                loginBtn.Focus();
            }
        }
    }
}
