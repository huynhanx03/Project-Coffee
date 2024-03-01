using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Utils.ConverterValue
{
    public class BackgroundButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _Value = (bool)value;

            if (_Value == true)
            {
                return "#CEF000";
            }
            else
            {
                return "#f0b000";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
