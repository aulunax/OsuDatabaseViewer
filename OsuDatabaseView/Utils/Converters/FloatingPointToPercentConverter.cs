using System.Globalization;
using System.Windows.Data;

namespace OsuDatabaseView.Utils.Converters;

public class FloatingPointToPercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return (doubleValue*100).ToString("F2") + "%";
        }
        if (value is float floatValue)
        {
            return (floatValue*100).ToString("F2") + "%";
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}