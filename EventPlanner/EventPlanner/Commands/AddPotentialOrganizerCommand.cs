﻿using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class AddPotentialOrganizerCommand : ICommand
    {
        public AddPotentialOrganizerCommand(EventDetailsViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        private EventDetailsViewModel _ViewModel;

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
            if (parameter == null)
            {
                return;
            }

            _ViewModel.AddPotentialOrganizer(parameter as Organizer);
        }
    }
}
