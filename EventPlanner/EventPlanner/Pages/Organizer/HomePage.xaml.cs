using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace EventPlanner.Pages.Organizer
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public HomePage()
        {
            InitializeComponent();
        }

        private void AddCollaboratorButton_Click(object sender, RoutedEventArgs e)
        {
            var AddCollaboratorModal = new Modals.AddCollaboratorModal(); // TO DO - same as bellow
            AddCollaboratorModal.ShowDialog();
        }

        private void FreeEventsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEvent = ((ListViewItem)sender).Content as Event;

            var eventDetailsModal = new Modals.EventDetailsModal();
            eventDetailsModal.DataContext = new EventDetailsViewModel(selectedEvent, true, false);
            eventDetailsModal.ShowDialog();
        }
        private void OrganizersEventsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEvent = ((ListViewItem)sender).Content as Event;

            var eventDetailsModal = new Modals.EventDetailsModal();
            eventDetailsModal.DataContext = new EventDetailsViewModel(selectedEvent, false, true);
            eventDetailsModal.ShowDialog();
        }
    }
}
