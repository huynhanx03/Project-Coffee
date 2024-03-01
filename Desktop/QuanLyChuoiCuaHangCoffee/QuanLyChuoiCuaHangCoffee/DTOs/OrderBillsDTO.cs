using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class OrderBillsDTO
    {
        public string MADH { get; set; }
        public string TENKHACHHANG { get; set; }
        public string TENNV { get; set; }
        public int MABAN { get; set; }
        public DateTime NGDH { get; set; }
        public decimal TONGTIEN { get; set; }
        public string TONGTIENSTR
        {
            get { return Helper.FormatVNMoney(TONGTIEN); }
        }
        public int DISCOUNT { get; set; }
        public string GHICHU { get; set; }
        public string TIMEIN { get; set; }
        public string TIMEOUT { get; set; }
    }
}
