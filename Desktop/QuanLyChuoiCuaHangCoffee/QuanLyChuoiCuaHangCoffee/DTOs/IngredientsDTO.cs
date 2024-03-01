using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class IngredientsDTO
    {
        public string MANGUYENLIEU { get; set; }
        public string TENNGUYENLIEU { get; set; }
        public string DONVI { get; set; }
        public int SOLUONGTRONGKHO { get;set; }
        public DateTime NGAYNHAP { get; set; }
        public string IMAGESOURCE {  get; set; }
        public decimal GIANHAP { get; set; }
        public string GIANHAPSTR
        {
            get { return Helper.FormatVNMoney(GIANHAP); }
        }
    }
}
