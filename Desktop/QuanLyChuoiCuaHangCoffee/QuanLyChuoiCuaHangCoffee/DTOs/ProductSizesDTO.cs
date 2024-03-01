using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class ProductSizesDTO
    {
        public string MASIZE { get; set; }
        public string SIZE { get; set; }
        public decimal GIA { get; set; }
        public string GIASTR
        {
            get { return Utils.Helper.FormatVNMoney(GIA); }
        }
        public int SOLUONG { get;set; }
    }
}
