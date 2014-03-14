﻿using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Wise.Framework.Presentation.Converters
{
    public class UriToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Uri)
            {
                var uri = value as Uri;
                return new BitmapImage(uri);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
