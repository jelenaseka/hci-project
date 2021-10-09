using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace EventPlanner.Converters
{
    public class EventTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is EventType type)
            {
                return GetString(type);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string s)
            {
                if(s == "Birthday")
                {
                    return EventType.BIRTHDAY;
                }else if(s == "Wedding")
                {
                    return EventType.WEDDING;
                }
                else
                {
                    return EventType.BIRTHDAY;
                }
            }
            return null;
        }

        public string[] Strings => GetStrings();

        public static string GetString(EventType type)
        {
            return type.GetType().GetMember(type.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
        }

        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (EventType type in Enum.GetValues(typeof(EventType)))
            {
                list.Add(GetString(type));
            }

            return list.ToArray();
        }

    }
}
