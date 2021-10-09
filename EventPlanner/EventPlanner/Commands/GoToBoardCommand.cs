using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class GoToBoardCommand : ICommand
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
            if (parameter is Event)
            {
                NavigationService.Singleton().ChangePage("Pages/EventBoardPage.xaml", parameter as Event);
            }
            else if (parameter is int)
            {
                Event e = EventService.Singleton().GetEventInfo((int)parameter);
                NavigationService.Singleton().ChangePage("Pages/EventBoardPage.xaml", e);
            }
        }
    }
}
