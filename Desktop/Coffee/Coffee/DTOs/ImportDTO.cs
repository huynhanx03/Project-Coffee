using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DTOs
{
    public class ImportDTO
    {
        public string MaPhieuNhapKho { get; set; }
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string NgayTaoPhieu { get; set; } 
        public decimal TongTien { get; set; }
    }
}
