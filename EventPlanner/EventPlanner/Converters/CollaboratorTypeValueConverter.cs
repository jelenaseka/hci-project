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
    public class CollaboratorTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is CollaboratorType type)
            {
                return GetString(type);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string s)
            {
                if(s == "Flower Shop")
                {
                    return CollaboratorType.FLOWER_SHOP;
                }else if(s == "Restaurant")
                {
                    return CollaboratorType.RESTAURANT;
                }else if(s == "Balloons")
                {
                    return CollaboratorType.BALLOONS;
                }else if(s == "Drink Store")
                {
                    return CollaboratorType.DRINK_STORE;
                }
            }
            return null;
        }

        public string[] Strings => GetStrings();

        public static string GetString(CollaboratorType type)
        {
            return type.GetType().GetMember(type.ToString())[0].GetCustomAttribute<DescriptionAttribute>().Description;
        }

        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (CollaboratorType type in Enum.GetValues(typeof(CollaboratorType)))
            {
                list.Add(GetString(type));
            }

            return list.ToArray();
        }

    }
}
