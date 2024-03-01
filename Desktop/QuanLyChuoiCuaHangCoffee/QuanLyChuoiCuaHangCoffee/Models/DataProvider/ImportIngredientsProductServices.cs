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
    public class ImportIngredientsProductServices
    {
        private static ImportIngredientsProductServices _ins;
        public static ImportIngredientsProductServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportIngredientsProductServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(String, bool)> AddToMenu(string _tensanpham, string _loaisanpham, string _size, decimal _gia, string _imageSource, ObservableCollection<ImportProductIngredient> _listImport)
        {
            using (var context = new CoffeeManagementEntities())
            {
                string currentMaxId = await context.MONs.MaxAsync(c => c.MAMON);
                string id = CreateNextId(currentMaxId, "SP");

                string currentMaxIdSize = await context.SIZEs.MaxAsync(c => c.MASIZE);
                string idSize = CreateNextId(currentMaxIdSize, "SZ");

                var existingProduct = await context.MONs.FirstOrDefaultAsync(sp => sp.TENMON.ToLower() == _tensanpham.ToLower());
                if (existingProduct != null)
                {
                    return ("Sản phẩm đã tồn tại", false);
                }
                else
                {
                    MON newItem = new MON();
                    newItem.MAMON = id;
                    newItem.MASIZE = idSize;
                    newItem.TENMON = _tensanpham;
                    newItem.LOAIMON = _loaisanpham;
                    newItem.IMAGESOURCE = _imageSource;

                    context.MONs.Add(newItem);

                    SIZE newSize = new SIZE();
                    newSize.MASIZE = idSize;
                    newSize.SIZEMON = _size;
                    newSize.GIABAN = _gia;
                    newSize.SOLUONG = 0;

                    context.SIZEs.Add(newSize);

                    foreach (var item in _listImport)
                    {
                        CTMON newCt = new CTMON();
                        newCt.MAMON = id;
                        newCt.MANGUYENLIEU = item.MaNguyenLieu;
                        newCt.SLNGUYENLIEU = item.SoLuong;

                        context.CTMONs.Add(newCt);
                    }
                    context.SaveChanges();

                    //tính toán số lượng sản phẩm dựa theo số lượng nguyên liệu
                    int sl = await ProductServices.Ins.CalQuantityProduct(newItem);

                    newSize.SOLUONG = sl;


                }
                context.SaveChanges();

            }
            return ("Thêm sản phẩm thành công", true);
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
