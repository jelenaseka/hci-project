using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EventPlanner.ViewModels
{
    public class TableViewModel : ObservableObject
    {
        private string _Name;
        private ObservableCollection<string> _Invites;
        public TableViewModel(Table table)
        {
            _Invites = new ObservableCollection<string>();
            (table.Invites).ForEach(_Invites.Add);
            _Name = table.Name;
        }
        public String Name
        {
            get => _Name;
            set { _Name = value; RaisePropertyChngedEvent("Name"); }
        }
        public ObservableCollection<string> Invites
        {
            get => _Invites;
            set { _Invites = value; RaisePropertyChngedEvent("Invites"); }
        }
    }
}
