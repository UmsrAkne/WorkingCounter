using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WorkingCounter.Models
{
    class DateToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return Math.Floor((date - DateTime.Today).TotalDays);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = int.Parse((string)value);
            return DateTime.Today.AddDays(number);
        }
    }
}
