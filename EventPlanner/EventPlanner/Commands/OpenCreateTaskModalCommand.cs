using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class OpenCreateTaskModalCommand : ICommand
    {
        public OpenCreateTaskModalCommand(EventBoardViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        private EventBoardViewModel _ViewModel;
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
            var createTaskModal = new Modals.TaskModal();
            createTaskModal.DataContext = new TaskViewModel(_ViewModel);
            createTaskModal.Show();
        }
    }
}
