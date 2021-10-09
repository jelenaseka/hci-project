using EventPlanner.ViewModels;
using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    [JsonConverter(typeof(JsonSubtypes), "role")]
    [JsonSubtypes.KnownSubType(typeof(User), "user")]
    [JsonSubtypes.KnownSubType(typeof(Admin), "admin")]
    [JsonSubtypes.KnownSubType(typeof(Organizer), "organizer")]
    public class User : ObservableObject
    {
        private int id;
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private List<Conversation> conversations;
        public int ID => id;
        public String Username
        {
            get => username;
            set { username = value; RaisePropertyChngedEvent("Username"); }
        }
        public String Password
        {
            get => password;
            set { password = value; RaisePropertyChngedEvent("Password"); }
        }
        public String FirstName
        {
            get => firstName;
            set { firstName = value; RaisePropertyChngedEvent("FirstName"); }
        }
        public String LastName
        {
            get => lastName;
            set { lastName = value; RaisePropertyChngedEvent("LastName"); }
        }
        [JsonIgnore]
        public List<Conversation> Conversations
        {
            get => conversations;
            set { conversations = value; RaisePropertyChngedEvent("Conversations"); }
        }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        [JsonProperty]
        protected virtual string role { get; } = "user";
        public User(int id, string username, string password, string firstName, string lastName)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            conversations = new List<Conversation>();
        }

    }
}
