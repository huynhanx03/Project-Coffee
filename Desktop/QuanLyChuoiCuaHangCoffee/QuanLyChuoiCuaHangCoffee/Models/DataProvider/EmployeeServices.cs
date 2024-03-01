using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class EmployeeServices
    {
        public EmployeeServices() { }
        private static EmployeeServices _ins;
        public static EmployeeServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new EmployeeServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> CreateNewEmployee(string _name, DateTime _dob, string _email, string _phone, string _cccd, string _chucvu, string _address, string _username, string _password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    USER newUser = new USER();
                    newUser.IDUSER = CreateNextId(context.NHANVIENs.Max(p => p.IDNHANVIEN), "NV");
                    newUser.HOTEN = _name;
                    newUser.DOB = _dob;
                    newUser.EMAIL = _email;
                    newUser.SODT = _phone;
                    newUser.CCCD = _cccd;
                    newUser.DIACHI = _address;
                    newUser.USERNAME = _username;
                    newUser.USERPASSWORD = _password;
                    newUser.NGBATDAU = DateTime.Now;
                    newUser.ROLE = 2;
                    context.USERS.Add(newUser);

                    var existPosition = context.CHUCDANHs.Where(p => p.TENCHUCDANH == _chucvu).FirstOrDefault();

                    NHANVIEN newEmployee = new NHANVIEN();
                    newEmployee.IDNHANVIEN = newUser.IDUSER;
                    newEmployee.MACD = existPosition.MACD;
                    context.NHANVIENs.Add(newEmployee);

                    context.SaveChanges();

                    return (true, "Thêm nhân viên thành công");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreateNextId(string maxId, string name)
        {
            if (maxId is null)
            {
                return name + "0001";
            }
            string newIdString = $"000{int.Parse(maxId.Substring(2)) + 1}";
            return name + newIdString.Substring(newIdString.Length - 4, 4);
        }

        public async Task EditEmployee(string _manv, string _name, DateTime _dob, string _email, string _phone, string _cccd, string _chucvu, string _address, string _username, string _password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var employee = context.NHANVIENs.Where(p => p.IDNHANVIEN == _manv).FirstOrDefault();
                    if (employee != null)
                    {
                        var user = context.USERS.Where(p => p.IDUSER == employee.IDNHANVIEN).FirstOrDefault();
                        user.HOTEN = _name;
                        user.DOB = _dob;
                        user.EMAIL = _email;
                        user.SODT = _phone;
                        user.CCCD = _cccd;
                        user.DIACHI = _address;
                        user.USERNAME = _username;
                        user.USERPASSWORD = _password;
                        var existPosition = context.CHUCDANHs.Where(p => p.TENCHUCDANH == _chucvu).FirstOrDefault();
                        employee.MACD = existPosition.MACD;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteEmployee(string _manv)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var employee = context.NHANVIENs.Where(p => p.IDNHANVIEN == _manv).FirstOrDefault();
                    if (employee != null)
                    {
                        var user = context.USERS.Where(p => p.IDUSER == employee.IDNHANVIEN).FirstOrDefault();
                        user.USERNAME = "";
                        user.USERPASSWORD = "";
                        user.EMAIL = "";
                        user.SODT = "0";
                        user.CCCD = "";
                        user.DIACHI = "";
                        user.HOTEN = "Nhân viên cũ";
                        user.ROLE = 2;

                        employee.MACD = "CD0006";
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeDTO>> GetAllEmployee()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var employeeList = (from e in context.NHANVIENs
                                        select new EmployeeDTO
                                        {
                                            IDNHANVIEN = e.IDNHANVIEN,
                                            HOTEN = e.USER.HOTEN,
                                            DIACHI = e.USER.DIACHI,
                                            SODT = e.USER.SODT,
                                            CHUCVU = e.CHUCDANH.TENCHUCDANH,
                                            HESOLUONG = (double)e.CHUCDANH.HESOLUONG,
                                            LUONGCOBAN = (decimal)e.CHUCDANH.LUONGCOBAN,
                                            NGAYBATDAU = e.USER.NGBATDAU,
                                            ROLE = (int)e.USER.ROLE,
                                            USERNAME = e.USER.USERNAME,
                                            USERPASSWORD = e.USER.USERPASSWORD,
                                            EMAIL = e.USER.EMAIL,
                                            CCCD = e.USER.CCCD,
                                            DOB = e.USER.DOB
                                        }).ToListAsync();

                    return await employeeList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
