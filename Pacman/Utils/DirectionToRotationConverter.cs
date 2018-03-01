using GameCore.EnumsAndConstant;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApplication.Utils
{
    [ValueConversion(typeof(Direction), typeof(double))]
    class DirectionToRotationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dir = (Direction)value;
            double angle;
            switch (dir)
            {
                case Direction.Up:
                    {
                        angle = -90.0;
                    }
                    break;
                case Direction.Down:
                    {
                        angle = 90.0;
                    }
                    break;
                case Direction.Left:
                    {
                        angle = 180.0;
                    }
                    break;
                default:
                    {
                        angle = 0.0;
                    }
                    break;
            }
            return angle;
            }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    
}
