using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class ImportBillServices
    {
        private static ImportBillServices _ins;
        public static ImportBillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportBillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<ImportBillDTO>> GetAllBill(DateTime _dateStart, DateTime _dateEnd)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var BillList = (from bl in context.NHAPKHOes
                                    where DbFunctions.TruncateTime(bl.NGNHAPKHO) >= _dateStart.Date &&
                                          DbFunctions.TruncateTime(bl.NGNHAPKHO) <= _dateEnd.Date
                                    select new ImportBillDTO
                                    {
                                        MAPHIEU = bl.MAPHIEU,
                                        IDNHANVIEN = bl.IDNHANVIEN,
                                        TENNHANVIEN = bl.NHANVIEN.USER.HOTEN,
                                        NGNHAPKHO = bl.NGNHAPKHO,
                                        TRIGIA = (decimal)bl.TRIGIA
                                    }).ToListAsync();

                    return await BillList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IngredientsDTO>> GetIngredientFromBill(string _madh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var ingredientList = context.NHAPKHOes.Where(p => p.MAPHIEU == _madh)
                        .Join(
                            context.CTNHAPKHOes,
                            nk => nk.MAPHIEU,
                            ct => ct.MAPHIEU,
                            (nk, ct) => new IngredientsDTO
                            {
                                MANGUYENLIEU = ct.MANGUYENLIEU,
                                TENNGUYENLIEU = ct.NGUYENLIEU.TENNGUYENLIEU,
                                DONVI = ct.NGUYENLIEU.DONVI,
                                SOLUONGTRONGKHO = (int)ct.SOLUONG,
                                GIANHAP = (decimal)ct.GIA,
                            }
                        ).ToListAsync();

                    return await ingredientList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
