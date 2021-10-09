using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EventPlanner.Models
{
    public enum CollaboratorType { [Description("Flower Shop")] FLOWER_SHOP, 
        [Description("Restaurant")]  RESTAURANT,
        [Description("Balloons")] BALLOONS,
        [Description("Drink Store")] DRINK_STORE }
    public class Collaborator : ObservableObject
    {
        private int id;
        private string _Name;
        private string _Address;
        private CollaboratorType _Type;

        public int ID => id;

        public String Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
                RaisePropertyChngedEvent("Name");
            }
        }

        public String Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                RaisePropertyChngedEvent("Address");
            }
        }

        public CollaboratorType Type {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
                RaisePropertyChngedEvent("Type");
            }
        }

        public Collaborator(int id, string name, CollaboratorType type, string address)
        {
            Name = name;
            Type = type;
            Address = address;
            this.id = id;
        }

    }
}
