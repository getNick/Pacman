using GameCore.EnumsAndConstant;
using System;
using System.Windows.Data;

namespace WpfApplication.Utils
{
    [ValueConversion(typeof(Direction), typeof(Array))]
    class DirectionToRotationConverter
    {
    }
}
