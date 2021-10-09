using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class SendMessageCommand : ICommand
    {
        public SendMessageCommand(MessagesViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        private MessagesViewModel _ViewModel;

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
            string message = parameter as string;
            if (message.Length > 0)
            {
                _ViewModel.SendMessage(message);
            }
        }
    }
}
