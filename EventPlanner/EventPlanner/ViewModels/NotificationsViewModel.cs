using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    class NotificationsViewModel : ObservableObject
    {
        private ObservableCollection<Notification> _Notifications;

        public NotificationsViewModel()
        {
            InitData();
            InitCommands();
        }

        private void InitData()
        {
            _Notifications = new ObservableCollection<Notification>();
            AddOriginalData();
        }
        private void InitCommands()
        {
            DeleteNotificationCmd = new DeleteNotificationCommand(this);
            GoToEventBoardCmd = new GoToBoardCommand();
        }
        private void AddOriginalData()
        {
            List<Notification> notifications = NotificationService.Singleton().GetNotificationsUserTo(UserService.Singleton().CurrentUser.ID);
            notifications.ForEach(_Notifications.Add);
        }
        
        public ObservableCollection<Notification> Notifications
        {
            get => _Notifications;
            set { _Notifications = value; RaisePropertyChngedEvent("NotificationViewModels"); }
        }
        public ICommand DeleteNotificationCmd
        {
            get; private set;
        }

        public ICommand GoToEventBoardCmd
        {
            get; private set;
        }
       
        public void DeleteNotification(Notification notification)
        {
            _Notifications.Remove(notification);
            NotificationService.Singleton().Delete(notification);
        }
    }
}
