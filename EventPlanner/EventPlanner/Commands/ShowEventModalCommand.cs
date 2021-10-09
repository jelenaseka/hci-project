using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class ShowEventModalCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Event selectedEvent = (Event) parameter;

            var eventDetailsModal = new Modals.User.MakeRequestWindow();
            eventDetailsModal.DataContext = new EventDetailsViewModel(selectedEvent, true, false);
            eventDetailsModal.ShowDialog();
        }
    }
}
