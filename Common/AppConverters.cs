using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DuelUI.Common
{
    public class StringToGeometry : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;

            if (String.IsNullOrEmpty(stringValue))
                return DependencyProperty.UnsetValue;

            return Geometry.Parse(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var geometry = value as Geometry;
            if (geometry != null)
                return geometry.ToString();
            return value;
        }
    }

}
