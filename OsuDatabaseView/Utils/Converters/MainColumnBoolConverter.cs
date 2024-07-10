﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;
using OsuDatabaseControl.Enums.Display;

namespace OsuDatabaseView.Utils.Converters;

public class MainColumnBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is MainColumnVisibility visibility && parameter is string visibilityMaskString)
        {
            if (Enum.TryParse<MainColumnVisibility>(visibilityMaskString, out MainColumnVisibility mask))
            {
                return (visibility & mask) == mask ? true : false;
            }
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}