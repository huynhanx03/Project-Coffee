using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace QuanLyChuoiCuaHangCoffee.Utils.ConverterValue
{
    public class TableStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _Value = value as string;

            if (_Value == "Có khách")
            {
                return "#FF3636";
            }
            else
            {
                return "#CEF000";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TableStatusHoverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _Value = value as string;

            if (_Value == "Có khách")
            {
                return "#f75454";
            }
            else
            {
                return "#e2fc44";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
