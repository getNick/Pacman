using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApplication.Utils
{
    /// <summary>
    ///Converter Grid position to game
    /// </summary>
    [ValueConversion(typeof(int), typeof(double))]
    class GridToPosZConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ((int)value) * -WpfApplication.Resources.Models.Enums_and_Constants.ViewConstants.OneTailSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
