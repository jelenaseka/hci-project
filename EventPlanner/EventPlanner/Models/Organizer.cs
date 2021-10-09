using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public class Organizer : User
    {
        public int Rating { get; set; }
        protected override string role { get; } = "organizer";

        public Organizer(int id, string username, string password, string firstName, string lastName, int rating)
            : base(id, username, password, firstName, lastName)
        {
            Rating = rating;
        }
    }
}
