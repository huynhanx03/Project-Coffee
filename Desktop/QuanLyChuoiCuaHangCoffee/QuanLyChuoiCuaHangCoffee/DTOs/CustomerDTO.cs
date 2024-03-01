using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            ROLE = ROLEint.Customer;
        }

        public CustomerDTO(string idkhachhang, string username, string email, string password, string name, string cccd, string sodt, DateTime dob, string dchi, DateTime ngbatdau)
        {
            IDKHACHHANG = idkhachhang;
            USERNAME = username;
            EMAIL = email;
            USERPASSWORD = password;
            HOTEN = name;
            CCCD = cccd;
            SODT = sodt;
            DOB = dob;
            DCHI = dchi;
            NGBATDAU = ngbatdau;
            ROLE = ROLEint.Customer;
        }
        public string IDKHACHHANG { get; set; }
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
        public string USERPASSWORD { get; set; }
        public string HOTEN { get; set; }
        public string CCCD { get; set; }
        public string SODT { get; set; }
        public DateTime DOB { get; set; }
        public string DCHI { get; set; }
        public DateTime NGBATDAU { get; set; }
        public int TICHDIEM { get; set; }
        public int SODONHANG { get; set; }
        public string HANGTHANHVIEN { get; set; }
        public int ROLE { get; set; }
        public string IMAGESOURCE { get; set; }
        public decimal CHITIEU { get; set; }

    }
}
