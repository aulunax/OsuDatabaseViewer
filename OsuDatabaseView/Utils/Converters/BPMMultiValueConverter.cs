using System.Globalization;
using System.Windows.Data;

namespace OsuDatabaseView.Utils.Converters;

public class BPMMultiValueConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length >= 3 &&
            values[0] is double avgNumber &&
            values[1] is double minNumber &&
            values[2] is double maxNumber)
        {
            if (minNumber == maxNumber)
            {
                return $"{(avgNumber < (double)int.MaxValue ? avgNumber.ToString("0.##") : "\u221e")}";
            }
            else
            {
                return
                    $"{(minNumber < (double)int.MaxValue ? minNumber.ToString("0.##") : "\u221e")}-{(maxNumber < (double)int.MaxValue ? maxNumber.ToString("0.##") : "\u221e")} ({(avgNumber < (double)int.MaxValue ? avgNumber.ToString("0.##") : "\u221e")})";
            }
        }

        return "?";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}