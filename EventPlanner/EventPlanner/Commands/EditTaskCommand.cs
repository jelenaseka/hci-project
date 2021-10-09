using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class EditTaskCommand : ICommand
    {
        public EditTaskCommand(TaskViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        private TaskViewModel _ViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate();
        }

        public void Execute(object parameter)
        {
            if(_ViewModel.Mode == Mode.Editing)
            {
                _ViewModel.EditTask();
            } else
            {
                _ViewModel.AddTask();
                // close modal
            }

            (parameter as Window).Close();
        }
    }
}
