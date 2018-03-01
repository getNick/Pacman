using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApplication.Utils
{/// <summary>
/// Inversive bool to visibility
/// return Hidden if true
/// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class InversBoolToVisibilityConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool BoolValue = (bool)value;
            if (BoolValue)
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
