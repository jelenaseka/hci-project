using EventPlanner.Modals.Admin;
using EventPlanner.Models;
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

namespace EventPlanner.Pages.Admin
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void AddOrganizerButton_Click(object sender, RoutedEventArgs e) {
            var w = new RegisterOrganizerModal();
            w.ShowDialog();
        }

        private void AddCollaboratorButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new RegisterCollaboratorModal();
            w.ShowDialog();
        }
    }
}
