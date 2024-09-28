using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using OsuDatabaseControl.Common;

namespace OsuDatabaseView.Utils.Converters
{
    public class ODtoHitWindowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float od)
            {
                return $"300: {DifficultyValueConverter.GetOverallDifficultyValue(od)}ms";
            }
            return "300: ?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}