using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OsuDatabaseView.Utils.Converters;

public class ImagePathToBitmapImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var path = value as string;
        if (string.IsNullOrEmpty(path))
            return null;

        return new BitmapImage(new Uri(path));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
