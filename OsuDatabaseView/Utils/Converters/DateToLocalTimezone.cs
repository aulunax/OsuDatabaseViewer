using System.Globalization;
using System.Windows.Data;

namespace OsuDatabaseView.Utils.Converters;

public class DateToLocalTimezone : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}