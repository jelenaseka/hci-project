using EventPlanner.Models;
using EventPlanner.Pages;
using EventPlanner.Services;
using EventPlanner.ViewModels;
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
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Services.NavigationService.Singleton().PageChanged += ChangePage;
            Services.NavigationService.Singleton().PageChangedWithModel += ChangePage;
            MainPageFrame.LoadCompleted += MainPageFrame_LoadCompleted; ;
        }

        private void MainPageFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Frame frame = sender as Frame;
            Page page = frame.Content as Page;
            if (currentViewModel == typeof(EventBoardViewModel))
            {
                var vm = page.DataContext as EventBoardViewModel;
                vm.ChangeCurrentEvent(currentModel as Event);
            }
            currentViewModel = null;
            currentModel = null;
        }

        private void ChangePage(object sender, string pagePath)
        {
            MainPageFrame.Source = new Uri($"pack://application:,,,/{pagePath}");
        }
        private void ChangePage(object sender, dynamic obj)
        {
            string pagePath = obj.page;
            object model = obj.model;
            if (pagePath.Equals("Pages/EventBoardPage.xaml"))
            {
                currentViewModel = typeof(EventBoardViewModel);
                currentModel = model;
            }
            MainPageFrame.Source = new Uri($"pack://application:,,,/{pagePath}");
        }

        private Type currentViewModel;
        private object currentModel;

        private void MainPageFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help.ShowHelp(null, "Data/help.chm");
            }
            else if (e.Key == Key.P && Keyboard.Modifiers == (ModifierKeys.Alt | ModifierKeys.Control))
            {
                UserService.Singleton().Logout();
                Services.NavigationService.Singleton().ChangePage("Pages/SigninPage.xaml");
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.H)
                {
                    User currentUser = UserService.Singleton().CurrentUser;
                    if (currentUser == null)
                    {
                        return;
                    }

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
                else if (e.Key == Key.P)
                {
                    User currentUser = UserService.Singleton().CurrentUser;
                    if (currentUser == null)
                    {
                        return;
                    }

                    Services.NavigationService.Singleton().ChangePage("Pages/ProfilePage.xaml");
                }
                else if (e.Key == Key.M)
                {
                    User currentUser = UserService.Singleton().CurrentUser;
                    if (currentUser == null || currentUser is Admin)
                    {
                        return;
                    }

                    Services.NavigationService.Singleton().ChangePage("Pages/MessagesPage.xaml");
                }
                else if (e.Key == Key.I)
                {
                    User currentUser = UserService.Singleton().CurrentUser;
                    if (currentUser == null || currentUser is Admin)
                    {
                        return;
                    }

                    Services.NavigationService.Singleton().ChangePage("Pages/NotificationPage.xaml");
                }
            }
        }
    }
}
