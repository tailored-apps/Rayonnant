using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Wise.Framework.Presentation.Converters
{
    public class NotNullToVisibilityConverter : IValueConverter
    {
        private static readonly Dictionary<bool, Visibility> notNullToVisibilityConverterDict = new Dictionary<bool, Visibility>
        {
            {true, Visibility.Visible},
            {false, Visibility.Collapsed}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                var invert = parameter as bool?;

                if (invert.HasValue && invert.Value)
                {
                    return notNullToVisibilityConverterDict[value != null];
                }
                return notNullToVisibilityConverterDict[value == null];
        
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}