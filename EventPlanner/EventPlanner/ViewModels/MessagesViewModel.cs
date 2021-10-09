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
    class MessagesViewModel : ObservableObject
    {
        public ObservableCollection<Message> Messages
        {
            get => messages;
        }
        public User OtherPerson
        {
            get => otherPerson;
            set { otherPerson = value; RaisePropertyChngedEvent("OtherPerson"); }
        }
        public string CurrentMessage {
            get => currentMessage;
            set { currentMessage = value; RaisePropertyChngedEvent("CurrentMessage"); }
        }
        public ICommand SendMessageCmd
        {
            get;
            private set;
        }

        public MessagesViewModel(List<Message> messages, int conversationId, User otherPerson)
        {
            this.messages = new ObservableCollection<Message>();
            messages.ForEach(this.messages.Add);
            this.conversationId = conversationId;
            this.otherPerson = otherPerson;
            SendMessageCmd = new SendMessageCommand(this);
        }
        public void SendMessage(string messageContents)
        {
            int currentUserId = UserService.Singleton().CurrentUser.ID;
            Message message = new Message(conversationId, messageContents, currentUserId, otherPerson.ID, DateTime.Now);
            ConversationService.Singleton().SaveMessage(message);
            messages.Add(message);
            // Service call to save message to db here
            CurrentMessage = string.Empty;
        }

        private ObservableCollection<Message> messages;
        private User otherPerson;
        private string currentMessage;
        private int conversationId;
    }
}
