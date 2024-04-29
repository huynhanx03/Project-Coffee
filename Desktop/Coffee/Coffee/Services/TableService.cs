using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Utils;
using Coffee.Utils.Helper;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class TableService
    {
        private static TableService _ins;
        public static TableService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new TableService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Thêm bàn
        /// </summary>
        /// <param name="table"> Bàn </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Bàn
        /// </returns>
        public async Task<(string, TableDTO)> createTable(TableDTO table)
        {
            string maxMaBan = await this.getMaxMaBan();
            string newMaBan = Helper.nextID(maxMaBan, "BA");
            table.MaBan = newMaBan;
            table.TrangThai = Constants.StatusTable.FREE;

            return await TableDAL.Ins.createTable(table);
        }

        /// <summary>
        /// Cập nhật bàn
        /// </summary>
        /// <param name="table"> Bàn </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Bàn
        /// </returns>
        public async Task<(string, TableDTO)> updateTable(TableDTO table)
        {
            return await TableDAL.Ins.updateTable(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã bàn lớn nhất
        /// </returns>
        public async Task<string> getMaxMaBan()
        {
            return await TableDAL.Ins.getMaxMaBan();
        }

        /// <summary>
        /// Xoá bàn
        /// </summary>
        /// <param name="TableID"> Mã bàn </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteTable(string TableID)
        {
            return await TableDAL.Ins.DeleteTable(TableID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách bàn
        /// </returns>
        public async Task<(string, List<TableDTO>)> getListTable()
        {
            return await TableDAL.Ins.getListTable();
        }
    }
}
