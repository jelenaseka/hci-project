using EventPlanner.Services;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public enum TaskLevel { TO_DO, IN_PROGRESS, DONE }
    public enum TaskType { GENERIC, SEATING }
    public enum TaskStatus { WAITING, ACCEPTED, REJECTED }
    public class Task : ObservableObject
    {
        private int _Id;
        private string _Title;
        private TaskStatus _Status;
        private int _EventId;
        private string _Description;
        private Collaborator _Collaborator;
        private TaskType _Type;
        private TaskLevel _Level;

        public int Id
        {
            get => _Id;
            set { _Id = value; RaisePropertyChngedEvent("Id"); }
        }
        public String Title
        {
            get => _Title;
            set { _Title = value; RaisePropertyChngedEvent("Title"); }
        }
        public TaskStatus Status
        {
            get => _Status;
            set { _Status = value; RaisePropertyChngedEvent("Status"); RaisePropertyChngedEvent("CanAcceptOrReject"); }
        }
        public TaskLevel Level
        {
            get => _Level;
            set { _Level = value; RaisePropertyChngedEvent("TaskLevel"); }
        }
        public int EventId
        {
            get => _EventId;
            set { _EventId = value; RaisePropertyChngedEvent("EventId"); }
        }
        public String Description
        {
            get => _Description;
            set { _Description = value; RaisePropertyChngedEvent("Description"); }
        }
        public Collaborator Collaborator
        {
            get => _Collaborator;
            set { _Collaborator = value; RaisePropertyChngedEvent("Collaborator"); }
        }
        public TaskType Type
        {
            get => _Type;
            set { _Type = value; RaisePropertyChngedEvent("Type"); }
        }
        public Task(int id, string title, TaskStatus status, TaskLevel level, int eventId, string description, Collaborator collaborator, TaskType type)
        {
            Id = id;
            Title = title;
            Status = status;
            Level = level;
            EventId = eventId;
            Description = description;
            Collaborator = collaborator;
            Type = type;
        }
        public Task() { }

        public Task(Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Status = task.Status;
            Level = task.Level;
            EventId = task.EventId;
            Description = task.Description;
            Collaborator = task.Collaborator;
            Type = task.Type;
        }
        public bool CanAcceptOrReject
        {
            get => !(UserService.Singleton().CurrentUser is Organizer || _Status == TaskStatus.ACCEPTED || _Status == TaskStatus.REJECTED);
        }
    }
}
