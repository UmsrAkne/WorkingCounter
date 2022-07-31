namespace WorkingCounter.Models
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime.ToString("yyyy/MM/dd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.TryParse((string)value, out DateTime result) ? result : default(DateTime);
        }
    }
}