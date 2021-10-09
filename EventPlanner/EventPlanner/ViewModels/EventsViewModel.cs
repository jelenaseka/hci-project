using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    class EventsViewModel : ObservableObject, ISearchable
    {
        public EventsViewModel()
        {
            InitData();
            InitCommands();

            EventService.Singleton().EventsChanged += EventsViewModel_EventsChanged;
        }

        private void EventsViewModel_EventsChanged(object sender, EventArgs e)
        {
            AddOriginalData();
        }

        private ObservableCollection<Event> organizerEvents;
        private ObservableCollection<Event> upcomingEvents;

        internal void AcceptEvent(Event e)
        {
            e.OrganizerId = UserService.Singleton().CurrentUser.ID;
            e.PotentialOrganizers.Clear();
            e.PotentialOrganizers.Add(UserService.Singleton().CurrentUser.ID);
            EventService.Singleton().Modify(e);
            ConversationService.Singleton().StartNewConversation(e.UserId, e.OrganizerId);
        }

        private ObservableCollection<Event> pastEvents;
        private Event selectedEvent;
        private Event newEvent;
        
        public ObservableCollection<Event> OrganizerEvents
        {
            get => organizerEvents;
            set { organizerEvents = value; RaisePropertyChngedEvent("OrganizerEvents"); }
        }
        public ObservableCollection<Event> UpcomingEvents
        {
            get => upcomingEvents;
            set { upcomingEvents = value; RaisePropertyChngedEvent("UpcomingEvents"); }
        }
        public ObservableCollection<Event> PastEvents
        {
            get => pastEvents;
            set { pastEvents = value; RaisePropertyChngedEvent("PastEvents"); }
        }
        public Event SelectedEvent
        {
            get => selectedEvent;
            set { selectedEvent = value; RaisePropertyChngedEvent("SelectedEvent"); }
        }
        public Event NewEvent
        {
            get => newEvent;
            set { newEvent = value; RaisePropertyChngedEvent("NewEvent"); }
        }

        public ICommand SearchCmd
        {
            get;
            private set;
        }
        public ICommand ShowEventModalCommand
        {
            get;
            private set;
        }
        public ICommand GoToBoardCmd
        {
            get;
            private set;
        }
        public ICommand CancelReqCmd
        {
            get;
            private set;
        }
        public ICommand AcceptEventCmd
        {
            get;
            private set;
        }

        private void InitCommands()
        {
            ShowEventModalCommand = new ShowEventModalCommand();
            SearchCmd = new SearchCommand(this);
            GoToBoardCmd = new GoToBoardCommand();
            CancelReqCmd = new CancelRequestCommand();
            AcceptEventCmd = new AcceptEventCommand(this);
        }
        private void InitData()
        {
            organizerEvents = new ObservableCollection<Event>();
            upcomingEvents = new ObservableCollection<Event>();
            pastEvents = new ObservableCollection<Event>();
            AddOriginalData();
        }
        private void AddOriginalData()
        {
            this.organizerEvents.Clear();
            List<Event> organizerEvents = new List<Event>();
            User user = UserService.Singleton().CurrentUser;

            EventService es = EventService.Singleton();
            organizerEvents = es.GetPotentialEventsForOrganizer(user.ID);
            organizerEvents.ForEach(this.organizerEvents.Add);

            this.upcomingEvents.Clear();
            List<Event> upcomingEvents = new List<Event>();

            upcomingEvents = user is Organizer ? es.GetUpcomingEventsForOrganizer(user.ID) : es.GetUpcomingEventsForUser(user.ID);
            upcomingEvents.ForEach(this.upcomingEvents.Add);

            this.pastEvents.Clear();
            List<Event> pastEvents = new List<Event>();
            pastEvents = user is Organizer ? es.GetPastEventsForOrganizer(user.ID) : es.GetPastEventsForUser(user.ID);
            pastEvents.ForEach(this.pastEvents.Add);
        }

        public void Search(string search)
        {
            // TODO: Implement search of events
            AddOriginalData();
            if (search.Length > 0)
            {
                List<Event> organizerEvents = new List<Event>(this.organizerEvents);
                List<Event> upcomingEvents = new List<Event>(this.upcomingEvents);
                List<Event> pastEvents = new List<Event>(this.pastEvents);
                this.organizerEvents.Clear();
                this.upcomingEvents.Clear();
                this.pastEvents.Clear();
                organizerEvents.FindAll(e => e.Title.Contains(search) || e.Description.Contains(search)).ForEach(this.organizerEvents.Add);
                upcomingEvents.FindAll(e => e.Title.Contains(search) || e.Description.Contains(search)).ForEach(this.upcomingEvents.Add);
                pastEvents.FindAll(e => e.Title.Contains(search) || e.Description.Contains(search)).ForEach(this.pastEvents.Add);
            }
        }
    }
}
