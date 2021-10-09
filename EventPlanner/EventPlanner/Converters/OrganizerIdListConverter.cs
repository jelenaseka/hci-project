using EventPlanner.Models;
using EventPlanner.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace EventPlanner.Converters
{
    public class OrganizerIdListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is List<int> type)
            {
                List<Organizer> users = UserService.Singleton().GetOrganizers();

                string result = "";
                foreach (int organizerId in value as List<int>)
                {
                    Organizer organizer = users.Find(o => o.ID == organizerId);
                    result += result.Length != 0 ? $", {organizer.FullName}" : $"{organizer.FullName}";
                }
                return result;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}
