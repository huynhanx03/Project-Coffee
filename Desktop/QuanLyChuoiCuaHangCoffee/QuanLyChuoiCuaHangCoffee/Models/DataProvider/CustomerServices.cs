using Microsoft.Xaml.Behaviors.Media;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class CustomerServices
    {
        public static string IDKHACHHANG { get; set; }
        public static string TENKH { get; set; }
        public static string RANKKH { get; set; }
        public static string DIACHI { get; set; }
        public static string SDT { get; set; }
        public static DateTime DOB { get; set; }
        public static string EMAIL { get; set; }
        public static string CCCD { get; set; }
        public static string USERNAME { get; set; }
        public static string PASSWORD { get; set; }
        public static string IMAGESOURCE { get; set; }
        public static DateTime NGBATDAU { get; set; }
        public static int TICHDIEM { get; set; }
        public static int SODONHANG { get; set; }
        public CustomerServices() { }
        private static CustomerServices _ins;
        public static CustomerServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustomerServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async void Register(string fullname, string email, string phonenumber, string username, string password)
        {
            if (string.IsNullOrEmpty(fullname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phonenumber))
            {
                MessageBoxCF ms = new MessageBoxCF("Có vẻ bạn nhập thiếu thông tin rồi.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            if (await Task.Run(() => Ins.CheckUserNameCustomer(username, "-1")))
            {
                MessageBoxCF ms = new MessageBoxCF("Tên tài khoản đã tồn tại.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }
            using (var context = new CoffeeManagementEntities())
            {
                string currentMaxId = await context.KHACHHANGs.MaxAsync(c => c.IDKHACHHANG);

                string id = CreateNextCustomerId(currentMaxId);

                USER us = new USER();
                us.IDUSER = id;
                us.USERNAME = username;
                us.USERPASSWORD = password;
                us.HOTEN = fullname;
                us.EMAIL = email;
                us.CCCD = "";
                us.SODT = phonenumber;
                us.DOB = DateTime.Now;
                us.DIACHI = "";
                us.NGBATDAU = DateTime.Now;
                us.ROLE = 3;
                us.IMAGESOURCE = "Resources/cup-of-coffee-avatar.jpg";

                context.USERS.Add(us);

                KHACHHANG cus = new KHACHHANG();
                cus.IDKHACHHANG = id;
                cus.TICHDIEM = 0;
                cus.SODONHANG = 0;
                cus.HANGTHANHVIEN = RANK.NORMAL;

                context.KHACHHANGs.Add(cus);

                try
                {
                    context.SaveChanges();
                    MessageBoxCF ms = new MessageBoxCF("Chào mừng đến với Quán cafe EPSRO.", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
                catch
                {
                    MessageBoxCF ms = new MessageBoxCF("Lỗi kết nối cơ sở dữ liệu.", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }

            }
        }

        public async Task LoadAvatar(string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        IMAGESOURCE = cus.USER.IMAGESOURCE;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LoadInforEdit(string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        IDKHACHHANG = cus.IDKHACHHANG;
                        TENKH = cus.USER.HOTEN;
                        RANKKH = cus.HANGTHANHVIEN;
                        DIACHI = cus.USER.DIACHI;
                        SDT = cus.USER.SODT;
                        DOB = cus.USER.DOB;
                        EMAIL = cus.USER.EMAIL;
                        CCCD = cus.USER.CCCD;
                        USERNAME = cus.USER.USERNAME;
                        PASSWORD = cus.USER.USERPASSWORD;
                        IMAGESOURCE = cus.USER.IMAGESOURCE;
                        NGBATDAU = cus.USER.NGBATDAU;
                        TICHDIEM = (int)cus.TICHDIEM;
                        SODONHANG = (int)cus.SODONHANG;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string CreateNextCustomerId(string maxId)
        {
            if (maxId is null)
            {
                return "KH0001";
            }
            string newIdString = $"000{int.Parse(maxId.Substring(2)) + 1}";
            return "KH" + newIdString.Substring(newIdString.Length - 4, 4);
        }

        public async Task<(bool, string, CustomerDTO)> Login(string _username, string _password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.USERNAME == _username && s.USER.USERPASSWORD == _password && s.USER.ROLE == 3
                               select new CustomerDTO
                               {
                                   IDKHACHHANG = s.IDKHACHHANG,
                                   USERNAME = s.USER.USERNAME,
                                   USERPASSWORD = s.USER.USERPASSWORD,
                                   HOTEN = s.USER.HOTEN,
                                   SODT = s.USER.SODT,
                                   CCCD = s.USER.CCCD,
                                   EMAIL = s.USER.EMAIL,
                                   DCHI = s.USER.DIACHI,
                                   HANGTHANHVIEN = s.HANGTHANHVIEN,
                                   DOB = s.USER.DOB,
                                   IMAGESOURCE = s.USER.IMAGESOURCE,
                                   NGBATDAU = s.USER.NGBATDAU,
                                   TICHDIEM = (int)s.TICHDIEM,
                                   SODONHANG = (int)s.SODONHANG
                               }).FirstOrDefault();

                    if (cus == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu.", null);
                    } else
                    {
                        IDKHACHHANG = cus.IDKHACHHANG;
                        TENKH = cus.HOTEN;
                        RANKKH = cus.HANGTHANHVIEN;
                        DIACHI = cus.DCHI;
                        SDT = cus.SODT;
                        DOB = cus.DOB;
                        EMAIL = cus.EMAIL;
                        CCCD = cus.CCCD;
                        USERNAME = cus.USERNAME;
                        PASSWORD = cus.USERPASSWORD;
                        IMAGESOURCE = cus.IMAGESOURCE;
                        NGBATDAU = cus.NGBATDAU;
                        TICHDIEM = cus.TICHDIEM;
                        SODONHANG = cus.SODONHANG;
                        return (true, "", cus);
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }

        public async Task EditSetting(string _makh, string _phone, string _email, DateTime _dob, string _address, string _cccd, string _password, string _currentPassword)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {

                        if (!string.IsNullOrEmpty(_password))
                        {
                            cus.USER.USERPASSWORD = _password;
                        }
                        else
                        {
                            cus.USER.USERPASSWORD = _currentPassword;
                        }

                        cus.USER.SODT = _phone;
                        cus.USER.EMAIL = _email;
                        cus.USER.DOB = _dob;
                        cus.USER.DIACHI = _address;
                        cus.USER.CCCD = _cccd;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, string)> UpdateAvatar(string _makh, string _imageSource)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        cus.USER.IMAGESOURCE = _imageSource;
                        context.SaveChanges();
                        return (true, "Đổi ảnh đại diện thành công");
                    }
                    return (false, "Có lỗi xảy ra");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CustomerDTO> FindCus(string _sdt)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.USERS
                                where s.SODT == _sdt
                                select new CustomerDTO
                                {
                                   IDKHACHHANG = s.KHACHHANG.IDKHACHHANG,
                                   USERNAME = s.USERNAME,
                                   USERPASSWORD = s.USERPASSWORD,
                                   HOTEN = s.HOTEN,
                                   SODT = s.SODT,
                                   EMAIL = s.EMAIL,
                                   DCHI = s.DIACHI,
                                   TICHDIEM = (int)s.KHACHHANG.TICHDIEM,
                                   SODONHANG = (int)s.KHACHHANG.SODONHANG,
                                   HANGTHANHVIEN = s.KHACHHANG.HANGTHANHVIEN
                                   
                               }).FirstOrDefault();

                    return cus;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region validate
        public async Task<bool> CheckUserNameCustomer(string _username, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = (from s in context.KHACHHANGs
                                     where s.USER.USERNAME == _username && s.USER.IDUSER != _makh
                                     select new CustomerDTO
                                     {
                                         HOTEN = s.USER.HOTEN,
                                         EMAIL = s.USER.EMAIL
                                     }).FirstOrDefault();
                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Username này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }

        public async Task<bool> CheckEmailCustomer(string _email, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.EMAIL == _email && s.USER.IDUSER != _makh
                               select new CustomerDTO
                               {
                                   HOTEN = s.USER.HOTEN,
                                   EMAIL = s.USER.EMAIL
                               }).FirstOrDefault();

                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Email này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }

        public async Task<bool> CheckPhonenumberCustomer(string _phonenumber, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.EMAIL == _phonenumber && s.USER.IDUSER != _makh
                               select new CustomerDTO
                               {
                                   HOTEN = s.USER.HOTEN,
                                   EMAIL = s.USER.EMAIL
                               }).FirstOrDefault();

                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Số điện thoại này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }
        #endregion

        public async Task<List<CustomerDTO>> GetAllCus()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cusList = (from kh in context.KHACHHANGs
                                   select new CustomerDTO
                                   {
                                       IDKHACHHANG = kh.IDKHACHHANG,
                                       USERNAME = kh.USER.USERNAME,
                                       USERPASSWORD = kh.USER.USERPASSWORD,
                                       HOTEN = kh.USER.HOTEN,
                                       SODT = kh.USER.SODT,
                                       EMAIL = kh.USER.EMAIL,
                                       DCHI = kh.USER.DIACHI,
                                       TICHDIEM = (int)kh.TICHDIEM,
                                       SODONHANG = (int)kh.SODONHANG,
                                       HANGTHANHVIEN = kh.HANGTHANHVIEN,
                                       NGBATDAU = kh.USER.NGBATDAU,
                                       DOB = kh.USER.DOB
                                       
                                   }).ToList();

                    return cusList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateRankCus(KHACHHANG cus)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    if (cus != null)
                    {
                        if (cus.TICHDIEM >= 5000)
                        {
                            cus.HANGTHANHVIEN = RANK.BRONZE;
                        }
                        else if (cus.TICHDIEM >= 10000)
                        {
                            cus.HANGTHANHVIEN = RANK.SILVER;
                        }
                        else if (cus.TICHDIEM >= 15000)
                        {
                            cus.HANGTHANHVIEN = RANK.GOLD;
                        }
                        else if (cus.TICHDIEM >= 35000)
                        {
                            cus.HANGTHANHVIEN = RANK.DIAMOND;
                        }
                        else
                        {
                            cus.HANGTHANHVIEN = RANK.NORMAL;
                        }

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCus(string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        cus.USER.HOTEN = "Khách vãng lai";
                        cus.USER.USERNAME = "";
                        cus.USER.USERPASSWORD = "";
                        cus.USER.EMAIL = "";
                        cus.USER.SODT = "";
                        cus.USER.DIACHI = "";
                        cus.USER.EMAIL = "";
                        cus.TICHDIEM = 0;
                        cus.HANGTHANHVIEN = RANK.NORMAL;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, string)> EditCustomer(string _makh, string _name, DateTime _dob, string _email, string _phone, string _cccd, string _address, string _username, string _password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        cus.USER.HOTEN = _name;
                        cus.USER.DOB = _dob;
                        cus.USER.EMAIL = _email;
                        cus.USER.SODT = _phone;
                        cus.USER.CCCD = _cccd;
                        cus.USER.DIACHI = _address;
                        cus.USER.USERNAME = _username;
                        cus.USER.USERPASSWORD = _password;
                        context.SaveChanges();
                        return (true, "Sửa thành công");
                    } else
                    {
                        return (false, "Không tìm thấy khách hàng");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CustomerDTO>> GetRankCustomer(string _rank)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cusList = (from p in context.KHACHHANGs
                                   where p.HANGTHANHVIEN == _rank
                                   select new CustomerDTO
                                   {
                                       HOTEN = p.USER.HOTEN,
                                       SODT = p.USER.SODT,
                                       EMAIL = p.USER.EMAIL,
                                   }).ToListAsync();

                    return await cusList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
