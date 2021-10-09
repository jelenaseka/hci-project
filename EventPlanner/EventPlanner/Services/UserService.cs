using EventPlanner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Resources;

namespace EventPlanner.Services
{
    class UserService
    {
        private readonly string PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\users.json");
        public static UserService Singleton()
        {
            return singleton ??= new UserService();
        }
        public event EventHandler LoggedInUserChanged;
        public User CurrentUser { get { return GetCurrentUser(); } }
        public bool Login(string username, string password)
        {
            User user = GetUsers().SingleOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));
            this.username = user?.Username ?? string.Empty;
            if (this.username.Length > 0)
            {
                LoggedInUserChanged?.Invoke(this, null);
            }

            return this.username.Length != 0;
        }

        public void Logout()
        {
            username = string.Empty;
            LoggedInUserChanged?.Invoke(this, null);
        }

        private User GetCurrentUser()
        {
            User user = GetUsers().SingleOrDefault(user => user.Username.Equals(username));
            if (user != null)
            {
                user.Conversations = ConversationService.Singleton().GetUsersConversations(user);
            }

            return user;
        }
        public User GetUserInfo(int id)
        {
            return GetUsers().FirstOrDefault(user => user.ID == id);
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (StreamReader reader = new StreamReader(PATH))
            {
                string data = reader.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            return users;
        }

        public List<User> Delete(User user)
        {
            List<User> users = GetUsers();
            users.RemoveAll(el => el.ID == user.ID);
            save(users);
            return users;
        }

        public void save(List<User> users)
        {
            using (StreamWriter writer = new StreamWriter(PATH))
            {
                string data = JsonConvert.SerializeObject(users);
                writer.WriteLine(data);
            }
        }

        public List<User> Modify(User user)
        {
            bool rememberUsernameSwap = CurrentUser.ID == user.ID;
            int userId = user.ID;
            List<User> users = GetUsers();
            for (int i = 0; i < users.Count(); i++)
            {
                if (users[i].ID == user.ID)
                {
                    users[i] = user;
                }
            }
            save(users);
            if (rememberUsernameSwap)
            {
                username = GetUsers().First(u => u.ID == userId).Username;
            }
            return users;
        }

        public List<User> Add(User user)
        {
            List<User> users = GetUsers();
            users.Add(user);
            save(users);
            return users;
        }

        public int GetLastId()
        {
            List<User> users = GetUsers();
            int greatest = 1;
            foreach (var user in users)
            {
                if (greatest < user.ID)
                {
                    greatest = user.ID;
                }
            }
            return ++greatest;
        }

        public List<Organizer> GetOrganizers()
        {
            List<Organizer> organizers = new List<Organizer>();
            List<User> users = GetUsers();
            users.RemoveAll(el => !(el is Organizer));
            users.ForEach(user => organizers.Add((Organizer)user));
            return organizers;
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();
            List<User> users = GetUsers();
            users.RemoveAll(el => !(el is Admin));
            users.ForEach(user => admins.Add((Admin)user));
            return admins;
        }

        public List<User> GetAppUsers()
        {
            List<User> users = GetUsers();
            users.RemoveAll(el => ((el is Organizer) || (el is Admin)));
            return users;
        }


        private static UserService singleton = null;
        private string username;
    }
}
