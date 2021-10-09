using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public class Message
    {
        public int ConversationId { get; private set; }
        public string Content { get; private set; }
        public int SenderId { get; private set; }
        public int ReceiverId { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public bool IsCurrentUsersMessage { get { return UserService.Singleton().CurrentUser?.ID == SenderId; } }

        public Message(int conversationId, string content, int senderId, int receiverId, DateTime timeStamp)
        {
            ConversationId = conversationId;
            Content = content;
            SenderId = senderId;
            ReceiverId = receiverId;
            TimeStamp = timeStamp;
        }
    }
}
