using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WorkMate.Models;
using WorkMate.Views;

namespace WorkMate.Converters
{
    public class TaskTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = MainView.DicKeyValues.FirstOrDefault(x => x.Value.iValue == (string)parameter).Key;

            if (value is ObservableCollection<IssueTicket> tickets && key != 0)
            {                                                
                return tickets.Count(x => x.TaskType == key);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            throw new NotImplementedException();
        }
    }
}