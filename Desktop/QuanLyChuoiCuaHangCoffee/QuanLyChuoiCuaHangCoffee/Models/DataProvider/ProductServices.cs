using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class ProductServices
    {
        private static ProductServices _ins;
        public static ProductServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ProductServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<ProductsDTO>> GetAllProducts()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    await UpdateQuantity();
                    var allProductsList = (from sp in context.MONs
                                           select new ProductsDTO
                                           {
                                               MAMON = sp.MAMON,
                                               MASIZE = sp.MASIZE,
                                               TENMON = sp.TENMON,
                                               LOAIMON = sp.LOAIMON,
                                               IMAGESOURCE = sp.IMAGESOURCE,
                                               SOLUONG = (int)sp.SIZE.SOLUONG,
                                               SIZESANPHAM = sp.SIZE.SIZEMON,
                                               GIASANPHAM = (decimal)sp.SIZE.GIABAN
                                           }).ToListAsync();

                    return await allProductsList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ProductsDTO>> GetInStockProducts()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    await UpdateQuantity();
                    var inStockProducts = (from sp in context.MONs
                                           where sp.SIZE.SOLUONG > 0
                                           select new ProductsDTO
                                           {
                                               MAMON = sp.MAMON,
                                               MASIZE = sp.MASIZE,
                                               TENMON = sp.TENMON,
                                               LOAIMON = sp.LOAIMON,
                                               IMAGESOURCE = sp.IMAGESOURCE,
                                               SOLUONG = (int)sp.SIZE.SOLUONG,
                                               SIZESANPHAM = sp.SIZE.SIZEMON,
                                               GIASANPHAM = (decimal)sp.SIZE.GIABAN
                                           }).ToListAsync();

                    return await inStockProducts;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<ProductsDTO>> GetOutStockProducts()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    await UpdateQuantity();
                    var inStockProducts = (from sp in context.MONs
                                           where sp.SIZE.SOLUONG <= 0
                                           select new ProductsDTO
                                           {
                                               MAMON = sp.MAMON,
                                               MASIZE = sp.MASIZE,
                                               TENMON = sp.TENMON,
                                               LOAIMON = sp.LOAIMON,
                                               IMAGESOURCE = sp.IMAGESOURCE,
                                               SOLUONG = (int)sp.SIZE.SOLUONG,
                                               SIZESANPHAM = sp.SIZE.SIZEMON,
                                               GIASANPHAM = (decimal)sp.SIZE.GIABAN
                                           }).ToListAsync();

                    return await inStockProducts;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> UpdateProduct (ProductsDTO _pd, string _tensanpham, string _SelectedType, string _SelectedSize, string _imagesource, string _Gia, ObservableCollection<ImportProductIngredient> _listImport)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var product = context.MONs.Where(x => x.MAMON == _pd.MAMON).FirstOrDefault();
                    
                    if (product != null)
                    {
                        product.MAMON = _pd.MAMON;
                        product.TENMON = _tensanpham;
                        product.LOAIMON = _SelectedType;
                        product.IMAGESOURCE = _imagesource;

                        var size = context.SIZEs.Where(x => x.MASIZE == _pd.MASIZE).FirstOrDefault();

                        if (size != null)
                        {
                            size.MASIZE = _pd.MASIZE;
                            size.SIZEMON = _SelectedSize;
                            size.GIABAN = decimal.Parse(_Gia);
                            size.SOLUONG = size.SOLUONG;
                        }

                        foreach (var item in _listImport)
                        {
                            var ingredient = context.CTMONs.Where(x => x.MAMON == _pd.MAMON && x.MANGUYENLIEU == item.MaNguyenLieu).FirstOrDefault();
                            if (ingredient != null)
                            {
                                ingredient.MAMON = _pd.MAMON;
                                ingredient.MANGUYENLIEU = item.MaNguyenLieu;
                                ingredient.SLNGUYENLIEU = item.SoLuong;
                            } else
                            {
                                CTMON newCt = new CTMON();
                                newCt.MAMON = _pd.MAMON;
                                newCt.MANGUYENLIEU = item.MaNguyenLieu;
                                newCt.SLNGUYENLIEU = item.SoLuong;

                                context.CTMONs.Add(newCt);
                                context.SaveChanges();
                            }
                        }

                        int sl = await CalQuantityProduct(product);
                        size.SOLUONG = sl;

                        context.SaveChanges();
                    }
                    return "Cập nhật thành công";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task <string> DeleteProduct(string _masp)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var product = context.MONs.Where(x => x.MAMON == _masp).FirstOrDefault();
                    context.MONs.Remove(product);
                    await context.SaveChangesAsync();

                    return("Xoá thành công");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<MON> FindProduct(string _mamon, string _size)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var product = await context.MONs.Where(x => x.MAMON == _mamon && x.SIZE.SIZEMON == _size).FirstOrDefaultAsync();
                    return product;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateQuantity()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var productList = await context.MONs.ToListAsync();
                    foreach (var item in productList)
                    {
                        int sl = await CalQuantityProduct(item);
                        item.SIZE.SOLUONG = sl;
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //tính toán số lượng lớn nhất có thể tạo ra sản phẩm từ nguyên liệu trong kho.
        public async Task<int> CalQuantityProduct(MON pd)
        {
            ObservableCollection<ImportProductIngredient> listIngredients = new ObservableCollection<ImportProductIngredient>();
            //lấy sản phẩm => lấy ra số lượng cần thiết tạo ra sản phẩm.
            listIngredients = new ObservableCollection<ImportProductIngredient>(await IngredientsServices.Ins.FindIngredients(pd.MAMON));
            ObservableCollection<IngredientsDTO> allIngredients = new ObservableCollection<IngredientsDTO>(await IngredientsServices.Ins.GetAllIngredients());

            int min = int.MaxValue;

            foreach (var item in listIngredients)
            {
                int tempMin = allIngredients.Where(x => x.TENNGUYENLIEU == item.TenNguyenLieu).FirstOrDefault().SOLUONGTRONGKHO / item.SoLuong;
                if (tempMin < min)
                {
                    min = tempMin;
                }
            }


            return min;
        }
    }
}
