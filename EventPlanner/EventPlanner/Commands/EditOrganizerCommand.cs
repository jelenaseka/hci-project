﻿using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class EditOrganizerCommand : ICommand
    {
        public EditOrganizerCommand(OrganizerViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        private OrganizerViewModel _ViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {

            _ViewModel.saveChanges();
        }
    }
}
