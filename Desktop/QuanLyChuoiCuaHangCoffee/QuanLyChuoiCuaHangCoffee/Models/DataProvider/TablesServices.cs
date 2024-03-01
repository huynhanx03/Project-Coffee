using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class TablesServices
    {
        private static TablesServices _ins;
        public static TablesServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new TablesServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(string, bool)> AddTable(int _newNumTable)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {

                    var existTable = context.BANs.Where(x => x.MABAN == _newNumTable).FirstOrDefault();
                    if (existTable != null)
                    {
                        return ("Số bàn này đã tồn tại", false);
                    }
                    

                    BAN newBan = new BAN();
                    newBan.MABAN = _newNumTable;
                    newBan.TRANGTHAI = "Còn trống";
                    context.BANs.Add(newBan);

                    context.SaveChanges();
                }

                return ("Thêm bàn thành công", true);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (("Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu", false));
            }
            catch (Exception)
            {
                return (("Xãy ra lỗi khi thực hiện thao tác", false));
            }
        }

        public async Task<(string, bool)> DeleteTable(int _maban)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var table = context.BANs.Where(x => x.MABAN == _maban).FirstOrDefault();

                    if (table != null && table.TRANGTHAI == "Có khách")
                    {
                        return ("Bàn này đang có khách", false);
                    }

                    context.BANs.Remove(table);
                    context.SaveChanges();
                }

                return ("Xóa bàn thành công", true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TablesDTO>> GetAllTables()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listTables = (from tb in context.BANs
                                      select new TablesDTO
                                      {
                                          MABAN = tb.MABAN,
                                          TRANGTHAI = tb.TRANGTHAI
                                      }).ToListAsync();
                    return await listTables;
                }   
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<TablesDTO>> GetTablesAvailable()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listTables = (from tb in context.BANs
                                      where tb.TRANGTHAI == "Còn trống"
                                      select new TablesDTO
                                      {
                                          MABAN = tb.MABAN,
                                          TRANGTHAI = tb.TRANGTHAI
                                      }).ToListAsync();
                    return await listTables;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<TablesDTO>> GetTablesNotAvailable()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listTables = (from tb in context.BANs
                                      where tb.TRANGTHAI == "Có khách"
                                      select new TablesDTO
                                      {
                                          MABAN = tb.MABAN,
                                          TRANGTHAI = tb.TRANGTHAI
                                      }).ToListAsync();
                    return await listTables;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SetStatusNotAvailableTable(int _maban)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var table = context.BANs.Where(x => x.MABAN == _maban).FirstOrDefault();
                    if (table != null)
                    {
                        table.TRANGTHAI = "Có khách";
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SetStatusAvailableTable(int _maban)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var table = context.BANs.Where(x => x.MABAN == _maban).FirstOrDefault();
                    if (table != null)
                    {
                        table.TRANGTHAI = "Còn trống";
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
