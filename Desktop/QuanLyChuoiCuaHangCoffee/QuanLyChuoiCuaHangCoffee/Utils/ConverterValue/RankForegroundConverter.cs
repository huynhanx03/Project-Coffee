using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyChuoiCuaHangCoffee.Utils.ConverterValue
{
    public class RankForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _Value = (string)value;

            if (_Value == RANK.DIAMOND)
            {
                return "#d8e1e8";
            }
            else if (_Value == RANK.GOLD)
            {
                return "#f2cf1f";
            }
            else if (_Value == RANK.SILVER)
            {
                return "#91908c";
            }
            else if (_Value == RANK.BRONZE)
            {
                return "#b57738";
            }
            else
            {
                return "Gray";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
