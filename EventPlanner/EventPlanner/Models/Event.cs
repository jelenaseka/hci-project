using EventPlanner.Services;
using EventPlanner.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EventPlanner.Models
{
    public enum EventType {[Description("Wedding")] WEDDING, [Description("Birthday")] BIRTHDAY }
    public class Event : ObservableObject
    {
        private int _Id;
        private string _Title;
        private EventType _Type;
        private string _Description;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        private int _UserId;
        private int _OrganizerId;
        private List<int> _PotentialOrganizers;
        private List<Task> _Tasks;

        public int Id
        {
            get => _Id;
            set { _Id = value; RaisePropertyChngedEvent("Id"); }
        }
        public string Title
        {
            get => _Title;
            set { _Title = value; RaisePropertyChngedEvent("Title"); }
        }
        public EventType Type
        {
            get => _Type;
            set { _Type = value; RaisePropertyChngedEvent("Type"); }
        }
        public string Description
        {
            get => _Description;
            set { _Description = value; RaisePropertyChngedEvent("Description"); }
        }
        public DateTime DateFrom
        {
            get => _DateFrom;
            set { _DateFrom = value; RaisePropertyChngedEvent("DateFrom"); }
        }
        public DateTime DateTo
        {
            get => _DateTo;
            set { _DateTo = value; RaisePropertyChngedEvent("DateTo"); }
        }

        public int UserId
        {
            get => _UserId;
            set { _UserId = value; RaisePropertyChngedEvent("UserId"); }
        }

        public int OrganizerId
        {
            get => _OrganizerId;
            set { _OrganizerId = value; RaisePropertyChngedEvent("OrganizerId"); }
        }

        public List<int> PotentialOrganizers
        {
            get => _PotentialOrganizers;
            set { _PotentialOrganizers = value; RaisePropertyChngedEvent("PotentialOrganizers"); }
        }

        public List<Task> Tasks
        {
            get => _Tasks;
            set { _Tasks = value; RaisePropertyChngedEvent("Tasks"); }
        }

        [JsonIgnore]
        public User User
        {
            get => UserService.Singleton().GetUserInfo(UserId);
        }

        public Event(int id, string title, EventType type, string description, DateTime dateFrom, DateTime dateTo, int user, int organizer, List<int> organizers
            , List<Task> t)
        {
            Id = id;
            Title = title;
            Type = type;
            Description = description;
            DateFrom = dateFrom;
            DateTo = dateTo;
            UserId = user;
            OrganizerId = organizer;
            PotentialOrganizers = organizers;
            Tasks = t;
        }
    }
}
