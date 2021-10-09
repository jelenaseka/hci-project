using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    internal class SearchCommand : ICommand
    {
        public SearchCommand(ISearchable viewModel)
        {
            _ViewModel = viewModel;
        }
        private ISearchable _ViewModel;

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
            _ViewModel.Search((string)parameter);
        }
    }
}
