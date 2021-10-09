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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EventPlanner.Models;
using EventPlanner.Modals.User;
using EventPlanner.Modals;

namespace EventPlanner.Components
{
    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void makeRequestNavBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeRequestWindow window = new MakeRequestWindow();
            window.Show();
        }

        private void notificationsNavBtn_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.Singleton().ChangePage("Pages/NotificationPage.xaml");
        }

        private void signOutNavBtn_Click(object sender, RoutedEventArgs e)
        {
            UserService.Singleton().Logout();
            Services.NavigationService.Singleton().ChangePage("Pages/SigninPage.xaml");
        }

        private void homeNavBtn_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = UserService.Singleton().CurrentUser;
            if (currentUser is Admin)
            {
                Services.NavigationService.Singleton().ChangePage("Pages/Admin/Homepage.xaml");
            }
            else if (currentUser is Organizer)
            {
                Services.NavigationService.Singleton().ChangePage("Pages/Organizer/Homepage.xaml");
            }
            else
            {
                Services.NavigationService.Singleton().ChangePage("Pages/User/Homepage.xaml");
            }
        }

        private void profileNavBtn_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.Singleton().ChangePage("Pages/ProfilePage.xaml");
        }

        private void inboxNavBtn_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.Singleton().ChangePage("Pages/MessagesPage.xaml");
        }

        private void tutorialNavBtn_Click(object sender, RoutedEventArgs e)
        {
            Window modal;

            Models.User currentUser = UserService.Singleton().CurrentUser;
            if (currentUser == null)
            {
                modal = new GuestTutorialModal();
            }
            else if (currentUser is Models.Admin)
            {
                modal = new AdminTutorialModal();
            }
            else if (currentUser is Models.Organizer)
            {
                modal = new OrganizerTutorialModal();
            }
            else
            {
                modal = new UserTutorialModal();
            }
            modal.Show();
        }
    }
}
