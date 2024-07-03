using System.Globalization;
using System.Windows.Data;

namespace OsuDatabaseView.Utils.Converters;

public class LengthTimeToTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int totalTime)
        {
            int minutes = totalTime / 60000;
            int seconds = (totalTime / 1000) % 60;
            return $"{(minutes < 10 ? $"0{minutes}" : minutes.ToString())}:{seconds}";
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}