using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Utils
{
    public class ROLEint
    {
        public static readonly int Admin = 1;
        public static readonly int Manager = 2;
        public static readonly int Customer = 3;
    }

    public class ProductTypes
    {
        public static readonly List<string> ListLoaiSanPham = new List<string>()
        {
            "Cà phê",
            "Trà",
            "Sinh tố",
            "Nước ép",
            "Đá xay",
            "Nước ngọt",
            "Soda",
            "Ăn vặt",
        };
    }

    public class Sizes
    {
        public static readonly List<string> ListKichThuoc = new List<string>()
        {
            "Nhỏ",
            "Vừa",
            "Lớn",
        };
    }

    public class Positions
    {
        public static readonly List<string> ListChucVu = new List<string>()
        {
            "Quản lý cửa hàng",
            "Trưởng nhân viên pha chế",
            "Nhân viên pha chế",
            "Nhân viên thu ngân",
            "Nhân viên phục vụ",
        };
    }

    public class VOUCHER_STATUS
    {
        public static readonly string UNRELEASED = "Chưa phát hành";
        public static readonly string RELEASED = "Đã phát hành";
        public static readonly string USED = "Đã sử dụng";
        public static readonly string EXPIRED = "Đã hết hạn";
    }

    public class RANK
    {
        public static readonly string NORMAL = "Thường";
        public static readonly string BRONZE = "Đồng";
        public static readonly string SILVER = "Bạc";
        public static readonly string GOLD = "Vàng";
        public static readonly string DIAMOND = "Kim cương";
    }
}
