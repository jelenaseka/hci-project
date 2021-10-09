using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{

    class OneCollaboratorViewModel : ObservableObject
    {
        public OneCollaboratorViewModel(Collaborator c)
        {
            collaborator = c;
            temp = new Collaborator(c.ID, c.Name, c.Type, c.Address);
            InitCommands();
        }

        public OneCollaboratorViewModel(Collaborator c, CollaboratorViewModel model)
        {
            collaborator = c;
            temp = new Collaborator(c.ID, c.Name, c.Type, c.Address);
            parent = model;
            InitCommands();
        }

        private Collaborator collaborator;
        private Collaborator temp;
        private CollaboratorViewModel parent;

        private void InitCommands()
        {
            SaveCommand = new EditCollaboratorCommand(this);
        }

        public Collaborator Temp
        {
            get => temp;
        }

        public Collaborator Collaborator
        {
            get => collaborator;
        }

        public void saveChanges()
        {
            bool add = false;
            if(collaborator.Name == "")
            {
                CollaboratorService.Singleton().Add(temp);
                add = true;
            }
            else
            {
                CollaboratorService.Singleton().Modify(temp);
            }

            collaborator.Name = temp.Name;
            collaborator.Type = temp.Type;
            collaborator.Address = temp.Address;

            if (add)
            {
                parent.Collaborators.Add(collaborator);
            }
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }


        public bool CanUpdate { get {
                if (Temp == null)
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(Temp.Name))
                {
                    return false;
                }else if (String.IsNullOrWhiteSpace(Temp.Address))
                {
                    return false;
                }
                return true;
            } }
    }
}
