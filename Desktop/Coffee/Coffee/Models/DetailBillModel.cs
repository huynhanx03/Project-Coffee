using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Models
{
    public class DetailBillModel
    {
        public string MaHoaDon {  get; set; }
        public string MaSanPham { get; set; }
        public string MaKichThuoc { get; set; }
        public int SoLuong {  get; set; }
        public decimal ThanhTien { get; set; }
    }
}
