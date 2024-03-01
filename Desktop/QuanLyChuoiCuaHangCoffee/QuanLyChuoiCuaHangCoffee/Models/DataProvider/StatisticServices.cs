using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class StatisticServices
    {
        public StatisticServices() { }
        private static StatisticServices _ins;
        public static StatisticServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new StatisticServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        #region profit
        public async Task<(List<decimal>, decimal)> GetRevenueByYear(int year)
        {
            decimal totalRevenue = 0;
            List<decimal> revenueMonth = new List<decimal>(new decimal[12]);
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = context.DONHANGs.Where(x => x.NGAYDH.Year == year);

                    if (billList.ToList().Count != 0)
                    {
                        totalRevenue = (decimal)billList.Sum(x => x.TONGGIATRIDONHANG);
                    }

                    var billListByMonth = billList.GroupBy(x => x.NGAYDH.Month).Select(gr => new {Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TONGGIATRIDONHANG) ?? 0}).ToList();

                    foreach(var item in billListByMonth)
                    {
                        revenueMonth[item.Month - 1] = decimal.Truncate(item.Income);
                    }

                    return (revenueMonth, totalRevenue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(List<decimal>, decimal)> GetRevenueByMonth(int month, int year)
        {
            decimal totalRevenue = 0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> revenueDay = new List<decimal>(new decimal[days]);
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = context.DONHANGs.Where(x => x.NGAYDH.Year == year && x.NGAYDH.Month == month);

                    if (billList.ToList().Count != 0)
                    {
                        totalRevenue = (decimal)billList.Sum(x => x.TONGGIATRIDONHANG);
                    }

                    var billListByDay = billList.GroupBy(x => x.NGAYDH.Day).Select(gr => new { Day = gr.Key, Income = gr.Sum(b => (decimal?)b.TONGGIATRIDONHANG) ?? 0 }).ToList();

                    foreach (var item in billListByDay)
                    {
                        revenueDay[item.Day - 1] = decimal.Truncate(item.Income);
                    }

                    return (revenueDay, totalRevenue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(List<decimal>, decimal)> GetExpenseByYear(int year)
        {
            try
            {
                decimal totalExpense = 0;
                List<decimal> expenseMonth = new List<decimal>(new decimal[12]);
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = context.NHAPKHOes.Where(x => x.NGNHAPKHO.Year == year);

                    if (billList.ToList().Count != 0)
                    {
                        totalExpense = (decimal)billList.Sum(x => x.TRIGIA);
                    }

                    var billListByMonth = billList.GroupBy(x => x.NGNHAPKHO.Month).Select(gr => new { Month = gr.Key, Expense = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();

                    foreach (var item in billListByMonth)
                    {
                        expenseMonth[item.Month - 1] = decimal.Truncate(item.Expense);
                    }

                    return (expenseMonth, totalExpense);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(List<decimal>, decimal)> GetExpenseByMonth(int month, int year)
        {
            decimal totalExpense = 0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> expenseDay = new List<decimal>(new decimal[days]);
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var billList = context.NHAPKHOes.Where(x => x.NGNHAPKHO.Year == year && x.NGNHAPKHO.Month == month);

                    if (billList.ToList().Count != 0)
                    {
                        totalExpense = (decimal)billList.Sum(x => x.TRIGIA);
                    }

                    var billListByDay = billList.GroupBy(x => x.NGNHAPKHO.Day).Select(gr => new { Day = gr.Key, Expense = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();

                    foreach (var item in billListByDay)
                    {
                        expenseDay[item.Day - 1] = decimal.Truncate(item.Expense);
                    }

                    return (expenseDay, totalExpense);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region product

        public async Task<List<ProductsDTO>> GetTopBeverage(int year)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var beverageList = context.CTDHs.Where(x => x.DONHANG.NGAYDH.Year == year && x.MON.LOAIMON != "Ăn vặt")
                        .GroupBy(x => x.MAMON)
                        .Select(gr => new
                        {
                            MAMON = gr.Key,
                            SOLUONG = gr.Sum(b => (int?)b.SOLUONG) ?? 0
                        })
                        .Join(
                            context.MONs,
                            s => s.MAMON,
                            m => m.MAMON,
                            (s, m) => new ProductsDTO
                            {
                                MAMON = s.MAMON,
                                TENMON = m.TENMON,
                                SOLUONG = s.SOLUONG
                            }
                         )
                        .OrderByDescending(m => m.SOLUONG).Take(5)
                        .ToList();

                    return beverageList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductsDTO>> GetTopFood(int year)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var foodList = context.CTDHs.Where(x => x.DONHANG.NGAYDH.Year == year && x.MON.LOAIMON == "Ăn vặt")
                        .GroupBy(x => x.MAMON)
                        .Select(gr => new
                        {
                            MAMON = gr.Key,
                            SOLUONG = gr.Sum(b => (int?)b.SOLUONG) ?? 0
                        })
                        .Join(
                            context.MONs,
                            s => s.MAMON,
                            m => m.MAMON,
                            (s, m) => new ProductsDTO
                            {
                                MAMON = s.MAMON,
                                TENMON = m.TENMON,
                                SOLUONG = s.SOLUONG
                            }
                         )
                        .OrderByDescending(m => m.SOLUONG).Take(5)
                        .ToList();

                    return foodList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region customer

        public async Task<List<CustomerDTO>> GetTopCus()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cusList = context.KHACHHANGs.GroupBy(x => x.IDKHACHHANG)
                        .Select(gr => new
                        {
                            MAKH = gr.Key,
                            CHITIEU = gr.Sum(b => b.DONHANGs.Sum(dh => (decimal?)dh.TONGGIATRIDONHANG) ?? 0)
                        })
                        .Join(
                            context.KHACHHANGs,
                            s => s.MAKH,
                            k => k.IDKHACHHANG,
                            (s, k) => new CustomerDTO
                            {
                                IDKHACHHANG = s.MAKH,
                                HOTEN = k.USER.HOTEN,
                                CHITIEU = s.CHITIEU
                            }
                        )
                        .OrderByDescending(k => k.CHITIEU).Take(5)
                        .ToList();

                    return cusList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
