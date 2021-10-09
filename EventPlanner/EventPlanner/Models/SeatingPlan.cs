using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Models
{
    public class SeatingPlan : ObservableObject
    {
        private int _Id;
        private int _TaskId;
        private List<string> _UnsortedGuests;
        private List<Table> _Tables;

        public List<Table> Tables
        {
            get => _Tables;
            set { _Tables = value; RaisePropertyChngedEvent("Tables"); }
        }
        public List<string> UnsortedGuests
        {
            get => _UnsortedGuests;
            set { _UnsortedGuests = value; RaisePropertyChngedEvent("UnsortedGuests"); }
        }
        public int TaskId
        {
            get => _TaskId;
            set { _TaskId = value; RaisePropertyChngedEvent("TaskId"); }
        }
        public int Id
        {
            get => _Id;
            set { _Id = value; RaisePropertyChngedEvent("Id"); }
        }
        public SeatingPlan(int id, List<Table> tables, int taskId, List<string> unsorted)
        {
            Id = id;
            Tables = tables;
            TaskId = taskId;
            UnsortedGuests = unsorted;
        }
    }
}
