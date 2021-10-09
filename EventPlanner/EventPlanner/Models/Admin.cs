using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public class Admin : User
    {
        protected override string role { get; } = "admin";
        public Admin(int id, string username, string password, string firstName, string lastName)
            : base(id, username, password, firstName, lastName)
        {
        }
    }
}
