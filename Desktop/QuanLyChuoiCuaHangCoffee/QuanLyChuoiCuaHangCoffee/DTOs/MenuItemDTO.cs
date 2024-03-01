using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class MenuItemDTO
    {
        public string MAMON { get; set; }
        public string TENMON { get; set; }
        public string SIZE { get; set; }
        public string SOLUONG { get; set; }
        public decimal DONGIA { get; set; }
        public string DONGIASTR
        {
            get { return Helper.FormatVNMoney(DONGIA);}
        }
        public decimal THANHTIEN { get; set; }
        public string THANHTIENSTR
        {
            get { return Helper.FormatVNMoney(THANHTIEN); }
        }
    }
}
