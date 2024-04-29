using Coffee.DTOs;
using Coffee.Utils;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Coffee.DALs
{
    public class ProductDAL
    {
        private static ProductDAL _ins;
        public static ProductDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ProductDAL();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Thêm sản phẩm
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        /// <returns>
        ///     1: Lỗi khi thêm dữ liệu
        ///     2: Sản phẩm
        /// </returns>
        public async Task<(string, ProductDTO)> createProduct(ProductDTO product)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("SanPham/" + product.MaSanPham, product);

                    return ("Thêm sản phẩm thành công", product);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Sản phẩm
        /// </returns>
        public async Task<(string, ProductDTO)> updateProduct(ProductDTO product)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("SanPham/" + product.MaSanPham, product);

                    return ("Cập nhật sản phẩm thành công", product);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã sản phẩm lớn nhất
        /// </returns>
        public async Task<string> getMaxMaSanPham()
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("SanPham");

                    if (response.Body != null && response.Body != "null")
                    {
                        Dictionary<string, ProductDTO> data = response.ResultAs<Dictionary<string, ProductDTO>>();

                        string MaxMaSanPham = data.Values.Select(p => p.MaSanPham).Max();

                        return MaxMaSanPham;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách sản phẩm
        /// </returns>
        public async Task<(string, List<ProductDTO>)> getListProduct()
        {
            try
            {
                using (var context = new Firebase())
                {
                    // Lấy dữ liệu từ nút "Product" trong Firebase
                    FirebaseResponse productResponse = await context.Client.GetTaskAsync("SanPham");
                    Dictionary<string, ProductDTO> productData = productResponse.ResultAs<Dictionary<string, ProductDTO>>();

                    // Lấy dữ liệu từ nút "ProductType" trong Firebase
                    FirebaseResponse productTypeResponse = await context.Client.GetTaskAsync("LoaiSanPham");
                    Dictionary<string, ProductTypeDTO> productTypeData = productResponse.ResultAs<Dictionary<string, ProductTypeDTO>>();
                    List<ProductTypeDTO> listProductType = productTypeData.Values.ToList();

                    var result = (from product in productData.Values
                                  select new ProductDTO
                                  {
                                      MaSanPham = product.MaSanPham,
                                      TenSanPham = product.TenSanPham,
                                      MaLoaiSanPham = product.MaLoaiSanPham,
                                      HinhAnh = product.HinhAnh,
                                      SoLuong = product.SoLuong,
                                      LoaiSanPham = listProductType.First(x => x.MaLoaiSanPham == product.MaLoaiSanPham).LoaiSanPham,
                                      DanhSachCongThuc = product.CongThuc.Values.ToList(),
                                      DanhSachChiTietKichThuocSanPham = product.ChiTietKichThuocSanPham.Values.ToList(),
                                  }).ToList();

                    return ("Lấy danh sách nhân viên thành công", result);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Xoá sản phẩm
        /// </summary>
        /// <param name="ProductID"> Mã sản phẩm </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteProduct(string ProductID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.DeleteTaskAsync("SanPham/" + ProductID);
                    return ("Xoá sản phẩm thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        /// Tăng số lượng sản phẩm
        /// </summary>
        /// <param name="productID"> mã sản phẩm </param>
        /// <param name="quantity"> số lượng </param>
        /// <returns>\
        ///     1. Thông báo
        ///     2. True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> increaseQuantityProduct(string productID, int quantity)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("SanPham/" + productID, new { SoLuong = quantity });

                    return ("Cập nhật sản phẩm thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
