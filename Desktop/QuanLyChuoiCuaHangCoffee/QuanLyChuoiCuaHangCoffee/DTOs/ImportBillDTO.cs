using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class ImportBillDTO
    {
        public string MAPHIEU { get; set; }
        public string IDNHANVIEN { get; set; }
        public string TENNHANVIEN { get; set; }
        public DateTime NGNHAPKHO { get; set; }
        public decimal TRIGIA { get; set; }
        public string TRIGIASTR
        {
            get { return Utils.Helper.FormatVNMoney(TRIGIA); }
        }
    }
}
