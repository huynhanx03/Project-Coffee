using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class AdminServices
    {
        public static string TenNhanVien { get; set; }
        public static DateTime NgSinhNhanVien { get; set; }
        public static string cccdNhanVien { get; set; }
        public static string SoDT { get; set; }
        public static string DiaChiNhanVien { get; set; }
        public static string MaNhanVien { get; set; }
        public static string EmailNhanVien { get; set; }
        public static string UserNameNhanVien { get; set; }
        public static string PasswordNhanVien { get; set; }
        public static int Role { get; set; }
        public static string LuongCoBan { get; set; }  
        public static string ChucVuNhanVien { get; set; }
        public static double HeSoLuong { get; set; }

        public AdminServices() { }   
        private static AdminServices _ins;
        public static AdminServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AdminServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> Login(string username, string password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var admin = await (from s in context.NHANVIENs
                                       where s.USER.USERNAME == username && s.USER.USERPASSWORD == password && s.USER.ROLE != 3
                                       select new AdminDTO
                                       {
                                           IDNHANVIEN = s.IDNHANVIEN,
                                           USERNAME = s.USER.USERNAME,
                                           EMAIL = s.USER.EMAIL,
                                           USERPASSWORD = s.USER.USERPASSWORD,
                                           HOTEN = s.USER.HOTEN,
                                           CCCD = s.USER.CCCD,
                                           SODT = s.USER.SODT,
                                           DOB = s.USER.DOB,
                                           DCHI = s.USER.DIACHI,
                                           NGBATDAU = s.USER.NGBATDAU,
                                           ROLE = (int)s.USER.ROLE,
                                           CHUCVU = s.CHUCDANH.TENCHUCDANH,
                                           HESOLUONG = (double)s.CHUCDANH.HESOLUONG,
                                           LUONGCOBAN = (decimal)s.CHUCDANH.LUONGCOBAN,

                                       }).FirstOrDefaultAsync();

                    if (admin == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu");
                    } else
                    {
                        TenNhanVien = admin.HOTEN;
                        NgSinhNhanVien = admin.DOB;
                        cccdNhanVien = admin.CCCD;
                        SoDT = admin.SODT;
                        DiaChiNhanVien = admin.DCHI;
                        MaNhanVien = admin.IDNHANVIEN;
                        EmailNhanVien = admin.EMAIL;
                        UserNameNhanVien = admin.USERNAME;
                        PasswordNhanVien = admin.USERPASSWORD;
                        Role = admin.ROLE;
                        LuongCoBan = admin.LUONGCOBANSTR;
                        ChucVuNhanVien = admin.CHUCVU;
                        HeSoLuong = admin.HESOLUONG;
                        return (true, "");
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống");
            }
        }

        public async Task EditSetting(string _manv, string _phone, string _email, string _password, string _currentPassword)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var employee = context.NHANVIENs.Where(p => p.IDNHANVIEN == _manv).FirstOrDefault();
                    if (employee != null)
                    {
                        if (!string.IsNullOrEmpty(_password))
                        {
                            employee.USER.USERPASSWORD = _password;
                        }
                        else
                        {
                            employee.USER.USERPASSWORD = _currentPassword;
                        }
                        employee.USER.SODT = _phone;
                        employee.USER.EMAIL = _email;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LoadInfoEdit(string _manv)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var employee = context.NHANVIENs.Where(p => p.IDNHANVIEN == _manv).FirstOrDefault();
                    if (employee != null)
                    {
                        PasswordNhanVien = employee.USER.USERPASSWORD;
                        SoDT = employee.USER.SODT;
                        EmailNhanVien = employee.USER.EMAIL;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateSalary(string _tenchucdanh, decimal _basesalary, double _coefficientsalary)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var chucdanh = context.CHUCDANHs.Where(p => p.TENCHUCDANH == _tenchucdanh).FirstOrDefault();
                    if (chucdanh != null)
                    {
                        chucdanh.LUONGCOBAN = _basesalary;
                        chucdanh.HESOLUONG = _coefficientsalary;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
