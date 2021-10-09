using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    class OrganizersViewModel : ObservableObject, ISearchable
    {
        private Organizer selectedOrganizer;
        public Organizer SelectedOrganizer
        {
            get => selectedOrganizer; 
            set { selectedOrganizer = value; RaisePropertyChngedEvent("SelectedOrganizer"); }
        }
        /// <summary>
        /// Gets the Organizer Collection instance
        /// </summary>
        public ObservableCollection<Organizer> Organizers
        {
            get => organizers;
        }

        /// <summary>
        /// Gets the FilterOrganizersCommand for the ViewModel
        /// </summary>
        public ICommand SearchCmd
        {
            get;
            private set;
        }

        public ICommand CreateEditWindowCmd
        {
            get;
            private set;
        }

        public ICommand CreateAddWindowCmd
        {
            get;
            private set;
        }
        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        public OrganizersViewModel()
        {
            InitData();
            InitCommands();
        }

        public void Search(string search)
        {
            AddOriginalData();
            if (search.Length > 0)
            {
                List<Organizer> organizers = new List<Organizer>(Organizers);
                this.organizers.Clear();
                organizers.FindAll(organizer =>
                    organizer.FirstName.Contains(search)
                    || organizer.LastName.Contains(search)
                    || organizer.Username.Contains(search)
                ).ForEach(this.organizers.Add);
            }
        }

        private void InitCommands()
        {
            SearchCmd = new SearchCommand(this);
            CreateEditWindowCmd = new CreateEditWindowsCommand(this);
            CreateAddWindowCmd = new CreateAddWindowsCommand();
            DeleteCommand = new DeleteOrganizerCommand(this);
        }

        private ObservableCollection<Organizer> organizers;

        private void InitData()
        {
            organizers = new ObservableCollection<Organizer>();
            AddOriginalData();
        }

        private void AddOriginalData()
        {
            this.organizers.Clear();
            UserService service = UserService.Singleton();
            List<Organizer> organizers = service.GetOrganizers();

            organizers.ForEach(this.organizers.Add);
        }
        public void delete(Organizer organizer)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure you wish to delete this organizer permanently?",
                "Event Planner", btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    UserService service = UserService.Singleton();
                    service.Delete(organizer);
                    Organizers.Clear();
                    List<Organizer> organizers = service.GetOrganizers();
                    organizers.ForEach(Organizers.Add);

                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.DataContext == this) item.Close();
                    }

                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }
    }
}
