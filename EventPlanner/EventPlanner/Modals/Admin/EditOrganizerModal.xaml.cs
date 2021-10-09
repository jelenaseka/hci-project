using EventPlanner.Models;
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

namespace EventPlanner.Modals.Admin
{
    /// <summary>
    /// Interaction logic for EditOrganizerModal.xaml
    /// </summary>
    public partial class EditOrganizerModal : Window
    {
        public EditOrganizerModal()
        {
            InitializeComponent();
        }

        private void CancelEditOrganizerButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveEditOrganizerModalButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
