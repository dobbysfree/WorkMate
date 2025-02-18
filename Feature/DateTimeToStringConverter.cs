using System;
using System.Globalization;
using System.Windows.Data;

namespace WorkMate.Feature
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;  // Return empty string if the value is null

            return ((DateTime)value).ToString("yyyy-MM-dd");  // Format the DateTime as yyyy-MM-dd
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString())) return null;  // If the input string is empty, return null

            return ((DateTime)value).ToString("yyyy-MM-dd");
        }
    }
}