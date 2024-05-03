using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Models
{
    public class BillModel
    {
        public string MaHoaDon { get; set; }
        public string MaBan { get; set; }
        public string MaNhanVien { get; set; }
        public string MaKhachHang { get; set; }
        public string NgayTao { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
    }
}
