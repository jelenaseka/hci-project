using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class ConfirmRequestCommand : ICommand
    {
        public ConfirmRequestCommand(EventDetailsViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private EventDetailsViewModel _ViewModel;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure you wish to create your request?", "Confirm request", btnMessageBox, icnMessageBox);

            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                _ViewModel.SaveEventDetails();
                if (parameter is Window)
                {
                    (parameter as Window).Close();
                }
            }
        }
    }
}
