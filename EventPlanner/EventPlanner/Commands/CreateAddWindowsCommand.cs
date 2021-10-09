using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class CreateAddWindowsCommand : ICommand
    {
        public CreateAddWindowsCommand()
        {

        }

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
            if (parameter is OrganizersViewModel)
            {
                Modals.Admin.RegisterOrganizerModal window = new Modals.Admin.RegisterOrganizerModal();
                window.DataContext = new ViewModels.OrganizerViewModel(new Organizer(UserService.Singleton().GetLastId(), "", "", "", "", 0), 
                    true, (OrganizersViewModel)parameter);
                window.Show();
            }
            else if (parameter is CollaboratorViewModel)
            {
                Modals.Admin.RegisterCollaboratorModal window = new Modals.Admin.RegisterCollaboratorModal();
                window.DataContext = new ViewModels.OneCollaboratorViewModel(new Collaborator(CollaboratorService.Singleton().GetLastId(), "", CollaboratorType.RESTAURANT, ""), 
                    (CollaboratorViewModel)parameter);
                window.Show();
            }
            else if (parameter is UserViewModel)
            {

            }
            else
            {

            }

        }
    }
}
