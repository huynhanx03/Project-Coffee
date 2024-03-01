using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class ProductsDTO
    {
        public string MAMON { get; set; }
        public string MASIZE { get; set;}
        public string TENMON { get; set; }
        public string LOAIMON { get; set; }
        public string IMAGESOURCE { get; set; }
        public int SOLUONG { get; set; }
        public string SIZESANPHAM { get; set; }
        public decimal GIASANPHAM { get; set; }
        public String GIASANPHAMStr
        {
            get { return Helper.FormatVNMoney(GIASANPHAM); }
        }
    }
}
