using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class EmployeeDTO
    {
        public string IDNHANVIEN { get; set; }
        public string HOTEN { get; set; }
        public string DIACHI { get; set; }
        public string SODT { get; set; }
        public string CHUCVU { get; set; }
        public double HESOLUONG { get; set; }
        public decimal LUONGCOBAN { get; set; }
        public string LUONGCOBANSTR
        {
            get { return Utils.Helper.FormatVNMoney(LUONGCOBAN); }
        }
        public DateTime NGAYBATDAU { get; set; }
        public int ROLE { get; set; }
        public string USERNAME { get; set; }
        public string USERPASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string CCCD { get; set; }
        public DateTime DOB { get; set; }
    }
}
