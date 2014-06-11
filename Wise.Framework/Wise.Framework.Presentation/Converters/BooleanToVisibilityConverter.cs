using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Wise.Framework.Presentation.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var val = (bool)value;
                var invert = parameter as bool?;

                if (invert.HasValue && invert.Value)
                {
                    return booleanToVisibilityDict[!val];
                }
                return booleanToVisibilityDict[val];

            }


            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return booleanToVisibilityDict.Single(x => x.Value.Equals(value));
        }


        private static readonly Dictionary<bool, Visibility> booleanToVisibilityDict = new Dictionary<bool, Visibility>()
        {
            {true, Visibility.Visible},
            {false, Visibility.Collapsed}
        };
    }
}
