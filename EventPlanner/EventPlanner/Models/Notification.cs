using EventPlanner.Services;
using EventPlanner.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public enum NotificationType { MESSAGE_SENT, EVENT_REQUEST, EVENT_CHANGED }
    
    public class Notification : ObservableObject
    {
        private int _Id;
        private int _UserFromId;
        private int _UserToId;
        private int _elementId;
        private NotificationType _Type;

        public int Id
        {
            get => _Id;
            set { _Id = value; RaisePropertyChngedEvent("Id"); }
        }
        public int ElementId
        {
            get => _elementId;
            set { _elementId = value; RaisePropertyChngedEvent("ElementId"); }
        }
        public int UserFromId
        {
            get => _UserFromId;
            set { _UserFromId = value; RaisePropertyChngedEvent("UserFromId"); }
        }
        public int UserToId
        {
            get => _UserToId;
            set { _UserToId = value; RaisePropertyChngedEvent("UserToId"); }
        }
        [JsonIgnore]
        public User UserFrom
        {
            get => UserService.Singleton().GetUserInfo(UserFromId);
        }
        [JsonIgnore]
        public User UserTo
        {
            get => UserService.Singleton().GetUserInfo(UserToId);
        }
        
        public NotificationType Type
        {
            get => _Type;
            set { _Type = value; RaisePropertyChngedEvent("Type"); }
        }

        public Notification(int id, User user, NotificationType type)
        {
            Id = id;
            UserFromId = user.ID;
            Type = type;
        }
        public Notification(int id, User user, NotificationType type, int element)
        {
            Id = id;
            UserFromId = user.ID;
            Type = type;
            _elementId = element;
        }

        public Notification(int id, User userf, User usert, NotificationType type, int element)
        {
            Id = id;
            UserFromId = userf.ID;
            UserToId = usert.ID;
            Type = type;
            _elementId = element;
        }

        [JsonConstructor]
        public Notification(int id, int userf, int usert, NotificationType type, int element)
        {
            Id = id;
            UserFromId = userf;
            UserToId = usert;
            Type = type;
            _elementId = element;
        }
        public String Description
        {
            get
            {
                if (_Type == NotificationType.MESSAGE_SENT) return UserFrom.Username + " sent you a message";
                else if (_Type == NotificationType.EVENT_REQUEST) return UserFrom.Username + " requested you to organize his/hers event.";
                else return UserFrom.Username + " changed information about the event.";
            }
        }
        
        public bool IsEventInformation
        {
            get
            {
                if(_Type == NotificationType.EVENT_CHANGED || _Type == NotificationType.EVENT_REQUEST) return true; 
                return false;
            }
        }
    }
}
