using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Utils;
using Coffee.Utils.Helper;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class ProductService
    {
        private static ProductService _ins;
        public static ProductService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ProductService();
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
        public async Task<(string, ProductDTO)> createProduct(ProductDTO product, List<ProductSizeDetailDTO> listProductSizeDetail, List<ProductRecipeDTO> listProductRecipe)
        {
            // Tìm mã sản phẩm lớn nhất
            string maSanPhamMax = await this.getMaxMaSanPham();
            string maSanPhamNew = Helper.nextID(maSanPhamMax, "SP");

            // Tạo sản phẩm
            product.MaSanPham = maSanPhamNew;

            (string labelCreateProduct, ProductDTO productNew) = await ProductDAL.Ins.createProduct(product);

            if (productNew != null)
            {
                // Thêm chi tiết kích thước sản phẩm
                (string labelCreateProductSizeDetail, bool isCreateProductSizeDetail) = await ProductSizeDetailService.Ins.createProductSizeDetail(productNew.MaSanPham, listProductSizeDetail); ;

                // Thêm công thức sản phẩm 
                (string labelCreateProductRecipe, bool isCreateProductRecipe) = await ProductRecipeService.Ins.createProductRecipe(productNew.MaSanPham, listProductRecipe);

                if (!isCreateProductSizeDetail)
                    return (labelCreateProductSizeDetail, null);

                if (!isCreateProductRecipe)
                    return (labelCreateProductRecipe, null);

                return (labelCreateProduct, productNew);
            }
            else
                return (labelCreateProduct, null);
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
            return await ProductDAL.Ins.updateProduct(product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã sản phẩm lớn nhất
        /// </returns>
        public async Task<string> getMaxMaSanPham()
        {
            return await ProductDAL.Ins.getMaxMaSanPham();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách sản phẩm
        /// </returns>
        public async Task<(string, List<ProductDTO>)> getListProduct()
        {
            return await ProductDAL.Ins.getListProduct();
        }

        /// <summary>
        /// Xoá sản phẩm
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteProduct(ProductDTO product)
        {
            await CloudService.Ins.DeleteImage(product.HinhAnh);

            return await ProductDAL.Ins.DeleteProduct(product.MaSanPham);
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
            return await ProductDAL.Ins.increaseQuantityProduct(productID, quantity);
        }
    }
}
