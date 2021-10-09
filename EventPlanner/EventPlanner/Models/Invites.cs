using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    class Invites : ObservableObject
    {
        private List<string> _AllInvites;
        private int _TaskId;

        public List<string> AllInvites
        {
            get => _AllInvites;
            set { _AllInvites = value; RaisePropertyChngedEvent("AllInvites"); }
        }

        public int TaskId
        {
            get => _TaskId;
            set { _TaskId = value; RaisePropertyChngedEvent("TaskId"); }
        }
    }
}
