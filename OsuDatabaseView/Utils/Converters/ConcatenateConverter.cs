using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OsuDatabaseView.Utils.Converters;

public class ConcatenateConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string resultString = string.Empty;
        if (values.Length == 2 && values[0] is string or int or float or double)
        {
            resultString = $"{values[0]} - {values[1]}";
        }
        return resultString;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}