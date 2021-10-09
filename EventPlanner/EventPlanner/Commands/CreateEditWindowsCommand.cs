using EventPlanner.Models;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.Commands
{
    class CreateEditWindowsCommand : ICommand
    {
        public CreateEditWindowsCommand(ObservableObject model)
        {
            parent = model;
        }
        private ObservableObject parent;

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
            if(parameter is Organizer){
                Modals.Admin.EditOrganizerModal window = new Modals.Admin.EditOrganizerModal();
                window.DataContext = new ViewModels.OrganizerViewModel((Organizer)parameter, false, (OrganizersViewModel)parent);
                window.Show();
            }else if(parameter is Collaborator)
            {
                Modals.Admin.EditCollaboratorModal window = new Modals.Admin.EditCollaboratorModal();
                window.DataContext = new ViewModels.OneCollaboratorViewModel((Collaborator)parameter, (CollaboratorViewModel)parent);
                window.Show();
            }
            else if(parameter is User)
            {

            }
            else
            {

            }
            
        }
    }
}
