using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
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
    public class ImportIngredientsServices
    {
        private static ImportIngredientsServices _ins;
        public static ImportIngredientsServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportIngredientsServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<string> Import(string _idNhanVien, DateTime _ngayNhapKho, int _triGia, ObservableCollection<ImportIngredientsDTO> _listImport)
        {
            using (var context = new CoffeeManagementEntities())
            {
                string currentMaxId = await context.NHAPKHOes.MaxAsync(c => c.MAPHIEU);
                string id = CreateNextId(currentMaxId, "PN");

                var nhapkho = new NHAPKHO();
                nhapkho.MAPHIEU = id;
                nhapkho.IDNHANVIEN = _idNhanVien;
                nhapkho.NGNHAPKHO = _ngayNhapKho;
                nhapkho.TRIGIA = (decimal)(_triGia);

                context.NHAPKHOes.Add(nhapkho);

                foreach (var item in _listImport)
                {
                    var existingNguyenLieu = await context.NGUYENLIEUx.FirstOrDefaultAsync(nl => nl.TENNGUYENLIEU.ToLower() == item.TenNguyenLieu.ToLower());

                    if (existingNguyenLieu != null)
                    {
                        var newCtnhap = new CTNHAPKHO();
                        newCtnhap.MAPHIEU = id;
                        newCtnhap.MANGUYENLIEU = existingNguyenLieu.MANGUYENLIEU;
                        newCtnhap.SOLUONG = item.SoLuong;
                        newCtnhap.GIA = item.Gia;

                        existingNguyenLieu.SOLUONGTRONGKHO += item.SoLuong;

                        context.CTNHAPKHOes.Add(newCtnhap);
                    }
                    else
                    {
                        string currentMaxIdNL = await context.NGUYENLIEUx.MaxAsync(c => c.MANGUYENLIEU);
                        string idNL = CreateNextId(currentMaxIdNL, "NL");

                        var newNguyenLieu = new NGUYENLIEU();
                        newNguyenLieu.MANGUYENLIEU = idNL;
                        newNguyenLieu.TENNGUYENLIEU = item.TenNguyenLieu;
                        newNguyenLieu.DONVI = item.DonVi;
                        newNguyenLieu.SOLUONGTRONGKHO = item.SoLuong;

                        var newCtnhap = new CTNHAPKHO();
                        newCtnhap.MAPHIEU = id;
                        newCtnhap.MANGUYENLIEU = idNL;
                        newCtnhap.SOLUONG = item.SoLuong;
                        newCtnhap.GIA = item.Gia;

                        context.NGUYENLIEUx.Add(newNguyenLieu);
                        context.CTNHAPKHOes.Add(newCtnhap);
                        context.SaveChanges();
                    }
                }

                context.SaveChanges();
            }
            return ("Nhập kho thành công");
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
    }
}
