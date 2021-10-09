using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using EventPlanner.Commands;
using System.Windows.Input;
using EventPlanner.Services;

namespace EventPlanner.ViewModels
{
    class UserMessagesViewModel : ObservableObject
    {
        public ObservableCollection<MessagesViewModel> MessagesViewModels
        {
            get => messagesViewModels;
        }
        public MessagesViewModel SelectedViewModel
        {
            get => selectedViewModel;
            set { selectedViewModel = value; RaisePropertyChngedEvent("SelectedViewModel"); }
        }
        public ICommand SelectUsersMessageCmd
        {
            get; private set;
        }
        public UserMessagesViewModel()
        {
            InitData(UserService.Singleton().CurrentUser);
            InitCommands();
        }

        private void InitData(User user)
        {
            messagesViewModels = new ObservableCollection<MessagesViewModel>();

            user.Conversations.ForEach(conversation =>
            {
                messagesViewModels.Add(new MessagesViewModel(conversation.Messages, conversation.ID, conversation.OtherPerson ));
            });

            if (messagesViewModels.Count > 0)
            {
                SelectedViewModel = messagesViewModels[0];
            }
        }

        private void InitCommands()
        {
            SelectUsersMessageCmd = new SelectUsersMessageCommand(this);
        }

        private ObservableCollection<MessagesViewModel> messagesViewModels;
        private MessagesViewModel selectedViewModel;
    }
}
