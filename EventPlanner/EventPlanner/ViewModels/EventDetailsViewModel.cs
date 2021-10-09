using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    public class EventDetailsViewModel : ObservableObject
    {
        private Event _EventDetails;
        private bool _IsFree;
        private bool _IsOrganizersEvent;
        private bool _isCreated = true;

        public EventDetailsViewModel()
        {
            _isCreated = false;

            EventDetails = new Event(EventService.Singleton().GetLastId(),
                                     "",
                                     EventType.BIRTHDAY,
                                     "",
                                     DateTime.Now,
                                     DateTime.Now,
                                     UserService.Singleton().CurrentUser.ID,
                                     -1,
                                     new List<int>(),
                                     new List<Task>());
            IsFree = true;
            IsOrganizersEvent = false;
            AddPotentialOrganizerCmd = new AddPotentialOrganizerCommand(this);
            ConfirmRequestCmd = new ConfirmRequestCommand(this);
            CancelRequestCmd = new CancelRequestCommand();
        }

        internal void SaveEventDetails()
        {
            if (_isCreated == false)
            {
                EventService.Singleton().Add(EventDetails);
            }
            else
            {
                EventService.Singleton().Modify(EventDetails);
            }
        }

        public EventDetailsViewModel(Event eventDetails, bool isFree, bool isOrganizersEvent)
        {
            EventDetails = eventDetails;
            IsFree = isFree;
            IsOrganizersEvent = isOrganizersEvent;
            AddPotentialOrganizerCmd = new AddPotentialOrganizerCommand(this);
            ConfirmRequestCmd = new ConfirmRequestCommand(this);
            CancelRequestCmd = new CancelRequestCommand();
        }

        internal void AddPotentialOrganizer(Organizer organizer)
        {
            if (EventDetails.PotentialOrganizers.Contains(organizer.ID) == false)
            {
                EventDetails.PotentialOrganizers.Add(organizer.ID);
                RaisePropertyChngedEvent("EventDetails");
            }
        }

        public Event EventDetails
        {
            get => _EventDetails;
            set { _EventDetails = value; RaisePropertyChngedEvent("EventDetails"); }
        }
        public bool IsFree
        {
            get => _IsFree;
            set { _IsFree = value; RaisePropertyChngedEvent("IsFree"); }
        }
        public bool IsOrganizersEvent
        {
            get => _IsOrganizersEvent;
            set { _IsOrganizersEvent = value; RaisePropertyChngedEvent("IsOrganizersEvent"); }
        }

        public ICommand AddPotentialOrganizerCmd
        {
            get;
            private set;
        }

        public ICommand ConfirmRequestCmd
        {
            get;
            private set;
        }

        public ICommand CancelRequestCmd
        {
            get;
            private set;
        }
    }
}
