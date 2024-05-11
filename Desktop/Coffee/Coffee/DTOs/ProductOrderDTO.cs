using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DTOs
{
    public class ProductOrderDTO
    {
        public string MaSanPham {  get; set; }
        public decimal Gia {  get; set; }

        private string _KichThuoc;
        public string KichThuoc
        {
            get
            {
                return _KichThuoc;
            }
            set
            {
                switch (value)
                {
                    case "S":
                        value = "Nhỏ";
                        break;

                    case "M":
                        value = "Vừa";
                        break;

                    case "L":
                        value = "Lớn";
                        break;
                }

                _KichThuoc = value;
            }
        }
        public string SoLuong {  get; set; }
        public string TenSanPham {  get; set; }
    }
}
