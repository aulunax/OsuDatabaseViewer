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
    public class ARtoTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float ar)
            {
                return $"{DifficultyValueConverter.GetApproachRateValue(ar)}ms";
            }
            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}