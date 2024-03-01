using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class OrderBillServices
    {
        public OrderBillServices() { }
        private static OrderBillServices _ins;
        public static OrderBillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new OrderBillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<OrderBillsDTO>> GetAllBill(DateTime _dateStart, DateTime _dateEnd)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = (from dh in context.DONHANGs
                                    where DbFunctions.TruncateTime(dh.NGAYDH) >= _dateStart.Date &&
                                          DbFunctions.TruncateTime(dh.NGAYDH) <= _dateEnd.Date
                                    select new OrderBillsDTO
                                    {
                                        MADH = dh.MADH,
                                        TENKHACHHANG = dh.KHACHHANG.USER.HOTEN,
                                        TENNV = dh.NHANVIEN.USER.HOTEN,
                                        MABAN = dh.BAN.MABAN,
                                        NGDH = dh.NGAYDH,
                                        TONGTIEN = (decimal)dh.TONGGIATRIDONHANG,
                                        DISCOUNT = (int)dh.DISCOUNT,
                                        GHICHU = dh.GHICHU,
                                        TIMEIN = dh.TIME_IN.ToString(),
                                        TIMEOUT = dh.TIME_OUT.ToString()
                                    }).ToListAsync();
                    
                    return await billList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderBillsDTO>> GetAllBillByCus(string _makh, DateTime _dateStart, DateTime _dateEnd)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = (from dh in context.DONHANGs
                                    where DbFunctions.TruncateTime(dh.NGAYDH) >= _dateStart.Date &&
                                          DbFunctions.TruncateTime(dh.NGAYDH) <= _dateEnd.Date &&
                                          dh.IDKHACHHANG == _makh
                                    select new OrderBillsDTO
                                    {
                                        MADH = dh.MADH,
                                        TENKHACHHANG = dh.KHACHHANG.USER.HOTEN,
                                        TENNV = dh.NHANVIEN.USER.HOTEN,
                                        MABAN = dh.BAN.MABAN,
                                        NGDH = dh.NGAYDH,
                                        TONGTIEN = (decimal)dh.TONGGIATRIDONHANG,
                                        DISCOUNT = (int)dh.DISCOUNT,
                                        GHICHU = dh.GHICHU,
                                        TIMEIN = dh.TIME_IN.ToString(),
                                        TIMEOUT = dh.TIME_OUT.ToString()
                                    }).ToListAsync();

                    return await billList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MenuItemDTO>> GetProductFromBill(string _madh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var productList = context.DONHANGs.Where(p => p.MADH == _madh)
                        .Join(
                            context.CTDHs,
                            dh => dh.MADH,
                            ct => ct.MADH,
                            (dh, ct) => new MenuItemDTO
                            {
                                TENMON = ct.MON.TENMON,
                                SOLUONG = ct.SOLUONG.ToString(),
                                DONGIA = (decimal)(ct.TONGTIEN / ct.SOLUONG),
                                THANHTIEN = (decimal)ct.TONGTIEN,
                            }).ToListAsync();

                    return await productList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
