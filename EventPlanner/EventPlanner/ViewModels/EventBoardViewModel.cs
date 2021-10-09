using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    class EventBoardViewModel : ObservableObject
    {
        private ObservableCollection<Task> _ToDoTasks;
        private ObservableCollection<Task> _InProgressTasks;
        private ObservableCollection<Task> _DoneTasks;
        private Event _Event;
        private ObservableCollection<Event> _AllUsersEvents;
        public EventBoardViewModel()
        {
            InitData();
            InitCommands();
        }
        public ObservableCollection<Task> ToDoTasks
        {
            get => _ToDoTasks;
            set { _ToDoTasks = value; RaisePropertyChngedEvent("ToDoTasks"); }
        }


        public ObservableCollection<Task> InProgressTasks
        {
            get => _InProgressTasks;
            set { _InProgressTasks = value; RaisePropertyChngedEvent("InProgressTasks"); }
        }
        public ObservableCollection<Task> DoneTasks
        {
            get => _DoneTasks;
            set { _DoneTasks = value; RaisePropertyChngedEvent("DoneTasks"); }
        }
        public Event Event
        {
            get => _Event;
            set {
                _Event = value;
                _ToDoTasks.Clear();
                _InProgressTasks.Clear();
                _DoneTasks.Clear();
                if (value != null)
                {
                    _Event.Tasks.FindAll(task => task.Level == TaskLevel.TO_DO).ForEach(_ToDoTasks.Add);
                    _Event.Tasks.FindAll(task => task.Level == TaskLevel.IN_PROGRESS).ForEach(_InProgressTasks.Add);
                    _Event.Tasks.FindAll(task => task.Level == TaskLevel.DONE).ForEach(_DoneTasks.Add);
                }
                RaisePropertyChngedEvent("Event");
            }
        }
        public ObservableCollection<Event> AllUsersEvents
        {
            get => _AllUsersEvents;
            set { _AllUsersEvents = value; RaisePropertyChngedEvent("AllUsersEvents"); }
        }
        public bool IsOrganizer
        {
            get => UserService.Singleton().CurrentUser is Organizer;
        }
        
        private void InitData()
        {
            _ToDoTasks = new ObservableCollection<Task>();
            _InProgressTasks = new ObservableCollection<Task>();
            _DoneTasks = new ObservableCollection<Task>();
            _AllUsersEvents = new ObservableCollection<Event>();
            
            AddOriginalData();
        }
        private void InitCommands()
        {
            SelectionChangedCmd = new SelectionChangedCommand(this);
            OpenCreateTaskModalCmd = new OpenCreateTaskModalCommand(this);
            OpenViewItemModalCmd = new OpenViewItemModalCommand();
            DeleteTaskCmd = new DeleteTaskCommand(this);
            AcceptTaskCmd = new AcceptTaskCommand(this);
            RejectTaskCmd = new RejectTaskCommand(this);
            MoveTaskCmd = new MoveTaskCommand(this);
        }
        public ICommand SelectionChangedCmd
        {
            get;
            private set;
        }
        public ICommand OpenCreateTaskModalCmd
        {
            get;
            private set;
        }
        public ICommand OpenViewItemModalCmd
        {
            get; private set;
        }
        public ICommand DeleteTaskCmd
        {
            get; private set;
        }
        public ICommand AcceptTaskCmd
        {
            get; private set;
        }
        public ICommand RejectTaskCmd
        {
            get; private set;
        }
        public ICommand MoveTaskCmd
        {
            get; private set;
        }
        private void AddOriginalData()
        {
            _ToDoTasks.Clear(); _InProgressTasks.Clear(); _DoneTasks.Clear();

            User user = UserService.Singleton().CurrentUser;
            EventService es = EventService.Singleton();
            List<Event> allUsersEvents = (user is Organizer) ? es.GetEventsForOrganizer(user.ID) : es.GetUsersEvents(user.ID);
            allUsersEvents.ForEach(_AllUsersEvents.Add);

            Event = allUsersEvents.Count > 0 ? allUsersEvents[0] : null;
        }
        public void ChangeCurrentEvent(Event _event)
        {
            foreach (var e in AllUsersEvents)
            {
                if (e.Id == _event.Id)
                {
                    Event = e;
                    break;
                }
            }
        }
        public void DeleteTask(Task task)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (_ToDoTasks.Contains(task)) _ToDoTasks.Remove(task);
                else if (_InProgressTasks.Contains(task)) _InProgressTasks.Remove(task);
                else _DoneTasks.Remove(task);
                if (Event.Tasks.Contains(task))
                {
                    Event.Tasks.RemoveAll(el => task.Id == el.Id);
                }
                EventService.Singleton().Modify(Event);
            }
        }
        public void AcceptTask(Task task)
        {
            task.Status = TaskStatus.ACCEPTED;
            EventService.Singleton().Modify(Event);
        }
        public void RejectTask(Task task)
        {
            task.Status = TaskStatus.REJECTED;
            EventService.Singleton().Modify(Event);
        }
        public void MoveTask(dynamic moveTask)
        {
            if (moveTask.ListFromLevel == TaskLevel.TO_DO) _ToDoTasks.Remove(moveTask.Task);
            else if (moveTask.ListFromLevel == TaskLevel.IN_PROGRESS) _InProgressTasks.Remove(moveTask.Task);
            else _DoneTasks.Remove(moveTask.Task);

            if (moveTask.ListToLevel == TaskLevel.TO_DO) _ToDoTasks.Add(moveTask.Task);
            else if (moveTask.ListToLevel == TaskLevel.IN_PROGRESS) _InProgressTasks.Add(moveTask.Task);
            else _DoneTasks.Add(moveTask.Task);

            moveTask.Task.Level = moveTask.ListToLevel;
            Task task = Event.Tasks.Find(t => t.Id == moveTask.Task.Id);
            task.Level = moveTask.ListToLevel;
            // update task
            EventService.Singleton().Modify(Event);
        }
    }
}
