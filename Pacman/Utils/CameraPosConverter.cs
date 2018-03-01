using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApplication.Utils
{/// <summary>
/// Convert maze height and width to grid size
/// </summary>
    class CameraPosConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var x = (int)values[0] * 0.75-0.5;
            var y = (int)values[1] * 0.75-0.5;
            string rez = String.Format($"{x} {y} 0.0");
            return rez;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
