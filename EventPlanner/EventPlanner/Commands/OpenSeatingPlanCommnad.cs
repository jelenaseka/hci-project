using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class OpenSeatingPlanCommnad : ICommand
    {
        public OpenSeatingPlanCommnad(TaskViewModel viewModel)
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
            return _ViewModel.Task.Type == TaskType.SEATING;
        }

        public void Execute(object parameter)
        {
            _ViewModel.OpenSeatingPlan((Task)parameter);
        }
    }
}
