﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DTOs
{
    public class DetailImportDTO
    {
        public string MaPhieuNhapKho { get; set; }
        public string MaNguyenLieu { get; set; } 
        public string TenNguyenLieu { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double SoLuong { get; set; }
        public decimal Gia { get; set; }
    }
}
