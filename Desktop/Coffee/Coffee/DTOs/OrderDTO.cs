﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DTOs
{
    public class OrderDTO
    {
        public string MaDonHang {  get; set; }
        public string MaNguoiDung {  get; set; }
        public string TenKhachHang {  get; set; }
        public string NgayTaoDon {  get; set; }
        public decimal PhiVanChuyen {  get; set; }
        public decimal ThanhTien{  get; set; }
        public string TrangThai {  get; set; }
        public Dictionary<string, ProductOrderDTO> SanPham {  get; set; }
    }
}
