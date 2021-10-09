using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EventPlanner.Models
{
    public class Table : ObservableObject
    {
        private string _Name;
        private List<string> _Invites;

        public String Name
        {
            get => _Name;
            set { _Name = value; }
        }
        public List<string> Invites
        {
            get => _Invites;
            set { _Invites = value; }
        }

        public Table(string name, List<string> invites)
        {
            Name = name;
            Invites = invites;
        }
    }
}
