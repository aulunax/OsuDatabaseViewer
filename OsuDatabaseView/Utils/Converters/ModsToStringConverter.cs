using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using OsuDatabaseControl.DTO;
using OsuDatabaseControl.DataTypes;
using OsuDatabaseControl.DataTypes.Osu;


namespace OsuDatabaseView.Utils.Converters
{
    public class ModsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScoreAndBeatmapPrintable score)
            {
                return $"{score.Mods.ToAcronyms()}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}