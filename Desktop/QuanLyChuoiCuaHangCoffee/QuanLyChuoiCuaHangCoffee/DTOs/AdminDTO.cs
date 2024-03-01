using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class AdminDTO
    {
        public AdminDTO()
        {
            ROLE = ROLEint.Admin;
        }

        public AdminDTO(string idnhanvien, string username, string email, string password, string name, string cccd, string sodt, DateTime dob, string dchi, DateTime ngbatdau)
        {
            IDNHANVIEN = idnhanvien;
            USERNAME = username;
            EMAIL = email;
            USERPASSWORD = password;
            HOTEN = name;
            CCCD = cccd;
            SODT = sodt;
            DOB = dob;
            DCHI = dchi;
            NGBATDAU = ngbatdau;
            ROLE = ROLEint.Admin;
        }

        public string IDNHANVIEN { get; set; }
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
        public string USERPASSWORD { get; set; }
        public string HOTEN { get; set; }
        public string CCCD { get; set; }
        public string SODT { get; set; }
        public DateTime DOB { get; set; }
        public string DCHI { get; set; }
        public DateTime NGBATDAU { get; set; }
        public int ROLE { get; set; }
        public string CHUCVU { get; set; }
        public double HESOLUONG { get; set; }
        public decimal LUONGCOBAN { get; set; }
        public string LUONGCOBANSTR
        {
            get { return Utils.Helper.FormatVNMoney(LUONGCOBAN); }
        }
    }
}
