using EventPlanner.Commands;
using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace EventPlanner.ViewModels
{
    class UserViewModel : ObservableObject
    {
        private User user;
        public bool isOrganizer;
        public bool canUpdate;
        private User newUser;

        public UserViewModel()
        {
            InitData();
            InitCommands();
        }

        private void InitData()
        {
            // getting logged-in user
            newUser = new User(UserService.Singleton().GetLastId(), "","","","");
            user = UserService.Singleton().CurrentUser;
            canUpdate = false;
        }

        public User NewUser
        {
            get => newUser;
            set { newUser = value; RaisePropertyChngedEvent("NewUser"); }
        }

        public ICommand EditUserCmd
        {
            get; private set;
        }

        public ICommand RegisterUserCmd
        {
            get; private set;
        }
        private void InitCommands()
        {
            EditUserCmd = new EditUserCommand(this);
            RegisterUserCmd = new RegisterCommand(this);
        }

        internal void RegisterUser(User user)
        {
            UserService.Singleton().Add(user);
        }

        public User User
        {
            get => user;
            set
            {
                user = value;
                RaisePropertyChngedEvent("User");
                RaisePropertyChngedEvent("IsOrganizer");
                RaisePropertyChngedEvent("Rating");
            }
        }
        public int Rating
        {
            get { return (user is Organizer) ? ((Organizer)user).Rating : 0; }
        }

        public bool IsOrganizer
        {
            get => user is Organizer;
        }
        public bool CanUpdate
        {
            get => canUpdate;
            set { canUpdate = value; RaisePropertyChngedEvent("CanUpdate"); }
        }
        public void SaveChanges()
        {
            // Successfully got modified data (stored in User)
            UserService.Singleton().Modify(user);
            CanUpdate = false;
        }
    }
}
