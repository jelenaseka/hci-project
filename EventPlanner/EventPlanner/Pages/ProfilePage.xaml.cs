using EventPlanner.ViewModels;
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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void editUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserViewModel viewModel = (UserViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.CanUpdate = true;
            }
        }
    }
}
