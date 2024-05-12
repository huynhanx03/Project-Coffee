using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Utils.Helper;
using Coffee.Views.MessageBox;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coffee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            c();
        }
        public async void t()
        {
            string baseID = "SP0010";

            

            List<ProductSizeDetailDTO> productSize = new List<ProductSizeDetailDTO>
            {
                new ProductSizeDetailDTO
                {
                    MaKichThuoc = "KT0001",
                    Gia = 10000,
                    TenKichThuoc = "Nhỏ"
                },
                new ProductSizeDetailDTO
                {
                    MaKichThuoc = "KT0002",
                    Gia = 20000,
                    TenKichThuoc = "Vừa"
                },
                new ProductSizeDetailDTO
                {
                    MaKichThuoc = "KT0003",
                    Gia = 30000,
                    TenKichThuoc = "Lớn"
                },
            };

            List<ProductRecipeDTO> productRecipe = new List<ProductRecipeDTO>
            {
                new ProductRecipeDTO
                {
                    MaNguyenLieu = "NL0003",
                    TenDonVi = "g",
                    MaDonVi = "DV0002",
                    SoLuong = 20,
                    TenNguyenLieu = "Cà phê"
                }
            };

            string filePath = "C:\\Users\\tinho\\Downloads\\coffee.xlsx";


            var context = new Firebase();
            FirebaseResponse response = await context.Client.GetTaskAsync("LoaiSanPham");

            Dictionary<string, ProductTypeDTO> rData = response.ResultAs<Dictionary<string, ProductTypeDTO>>();

            var result = rData.Values.ToList();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên trong file Excel

                int row = 2;

                for (int i = 0; i < 182; ++i)
                {
                    ProductDTO product = new ProductDTO
                    {
                        MaSanPham = Helper.nextID(baseID, "SP"),
                        HinhAnh = "https://res.cloudinary.com/dev9hnuhw/image/upload/v1715357953/coffee/jidfceqt6pbovf1ztnjc.png",
                        TenSanPham = worksheet.Cells[row, 2].Value.ToString(),
                        LoaiSanPham = worksheet.Cells[row, 3].Value.ToString()
                    };

                    ProductTypeDTO size = result.FirstOrDefault(x => x.LoaiSanPham == product.LoaiSanPham);
                    product.MaLoaiSanPham = size == null ? "LS0009" : size.MaLoaiSanPham;
                    product.Mota = product.TenSanPham + " siêu ngon";
                    
                    row += 1;
                    await ProductService.Ins.createProduct(product, productSize, productRecipe);
                }

            }
        }

        public async void c()
        {
            string baseID = "SP0050";
            int x = 0;
            while (baseID != "SP0192")
            {
                string nextID = Helper.nextID(baseID, "SP");

                ProductDTO product = new ProductDTO
                {
                    MaSanPham = nextID,
                    HinhAnh = "https://res.cloudinary.com/dev9hnuhw/image/upload/v1713571244/coffee/hq87nnd9am5tfgdgkni6.jpg"
                };

                await ProductService.Ins.DeleteProduct(product);

                baseID = nextID;
            }

            MessageBox.Show(x.ToString());
        }
    }
}
