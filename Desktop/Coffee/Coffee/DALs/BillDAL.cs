using Coffee.DTOs;
using Coffee.Models;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Coffee.DALs
{
    public class BillDAL
    {
        private static BillDAL _ins;
        public static BillDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillDAL();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Tạo hoá đơn mới
        /// </summary>
        /// <param name="bill">hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        public async Task<(string, bool)> createBill(BillModel bill)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("HoaDon/" + bill.MaHoaDon, bill);

                    return ("Thêm hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        ///     Lấy mã hoá đơn lớn nhất
        /// </summary>
        /// <returns>
        ///     Mã hoá đơn lớn nhất
        /// </returns>
        public async Task<string> getMaxMaHoaDon()
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("HoaDon");

                    if (response.Body != null && response.Body != "null")
                    {
                        Dictionary<string, BillModel> data = response.ResultAs<Dictionary<string, BillModel>>();

                        string MaxMaHoaDon = data.Values.Select(i => i.MaHoaDon).Max();

                        return MaxMaHoaDon;
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
        /// Tạo chi tiết hoá đơn mới
        /// </summary>
        /// <param name="BillID"> Mã của hoá đơn </param>
        /// <param name="detailList"> List chi tiết hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        ///
        public async Task<(string, bool)> createDetailBill(string BillID, List<DetailBillModel> detailList)
        {
            try
            {
                using (var context = new Firebase())
                {
                    foreach (var detail in detailList)
                    {
                        await context.Client.SetTaskAsync("HoaDon/" + BillID + "/ChiTietHoaDon/" + detail.MaSanPham + "-" + detail.MaKichThuoc, detail);
                    }

                    return ("Thêm chi tiết hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        /// Xoá hoá đơn
        /// </summary>
        /// <param name="BillID"></param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteBill(string BillID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.DeleteTaskAsync("HoaDon/" + BillID);
                    return ("Xoá hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }


        /// <summary>
        /// Tìm kiếm hoá đơn với bàn đã được đặt (chưa thanh toán)
        /// </summary>
        /// <param name="tableID">mã bàn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. Hoá đơn
        /// </returns>
        public async Task<(string, BillModel)> findBillByTableBooking(string tableID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon");
                    Dictionary<string, BillModel> billData = billResponse.ResultAs<Dictionary<string, BillModel>>();
                    BillModel bill = billData.Values.FirstOrDefault(x => x.MaBan == tableID && x.TrangThai == Constants.StatusBill.UNPAID);

                    if (bill != null)
                        return ("Tìm thành công", bill);
                    else
                        return ("Không tồn tại", null);
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
        ///     Danh sách hóa đơn
        /// </returns>
        public async Task<(string, List<BillDTO>)> getListBill()
        {
            try
            {
                using (var context = new Firebase())
                {
                    // Lấy dữ liệu từ nút "Bills" trong Firebase
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon");
                    Dictionary<string, BillDTO> billData = billResponse.ResultAs<Dictionary<string, BillDTO>>();

                    // Lấy dữ liệu từ nút "Employees" trong Firebase
                    FirebaseResponse employeeResponse = await context.Client.GetTaskAsync("NhanVien");
                    Dictionary<string, EmployeeDTO> employeeData = employeeResponse.ResultAs<Dictionary<string, EmployeeDTO>>();

                    // Lấy dữ liệu từ nút "Tables" trong Firebase
                    FirebaseResponse tableResponse = await context.Client.GetTaskAsync("Ban");
                    Dictionary<string, TableDTO> tableData = tableResponse.ResultAs<Dictionary<string, TableDTO>>();

                    var result = (from bill in billData.Values
                                  join employee in employeeData.Values on bill.MaNhanVien equals employee.MaNhanVien
                                  join table in tableData.Values on bill.MaBan equals table.MaBan
                                  select new BillDTO
                                  {
                                      MaBan = bill.MaBan,
                                      MaHoaDon = bill.MaHoaDon,
                                      MaNhanVien = bill.MaNhanVien,
                                      NgayTao = bill.NgayTao,
                                      TongTien = bill.TongTien,
                                      TrangThai = bill.TrangThai
                                  }).ToList();

                    return ("Lấy danh sách hóa đơn thành công", result);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

       
        /// <summary>
        /// lấy chi tiết hoá đơn với mã hoá đơn
        /// </summary>
        /// <param name="billID">chi tiết hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. Chi tiết hoá đơn (danh sách)
        /// </returns>
        /// 
        public async Task<(string, List<DetailBillDTO>)> getDetailBillById(string billID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon/" + billID);
                    BillDTO bill = billResponse.ResultAs<BillDTO>();
                    List<DetailBillDTO> listDetailBill = bill.ChiTietHoaDon.Values.ToList();

                    // Lấy dữ liệu từ nút "Product" trong Firebase
                    FirebaseResponse productResponse = await context.Client.GetTaskAsync("SanPham");
                    Dictionary<string, ProductDTO> productData = productResponse.ResultAs<Dictionary<string, ProductDTO>>();

                    foreach (DetailBillDTO detailBill in listDetailBill)
                    {
                        ProductDTO product = productData.Values.First(x => x.MaSanPham == detailBill.MaSanPham);

                        detailBill.TenSanPham = product.TenSanPham;
                        detailBill.DanhSachChiTietKichThuocSanPham = new ObservableCollection<ProductSizeDetailDTO>(product.ChiTietKichThuocSanPham.Values);
                        detailBill.SelectedProductSize = detailBill.DanhSachChiTietKichThuocSanPham.First(x => x.MaKichThuoc == detailBill.MaKichThuoc);
                    }

                    return ("Lấy danh sách chi tiết hoá đơn thành công", listDetailBill);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }


        /// <summary>
        /// Cập nhật hoá đơn
        /// </summary>
        /// <param name="bill"> hoá đơn </param>
        /// <returns>
        ///     1. Thông báo
        ///     2. Hoá đơn
        /// </returns>
        public async Task<(string, BillModel)> updateBill(BillModel bill)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("HoaDon/" + bill.MaHoaDon, bill);

                    return ("Cập nhật hoá đơn thành công", bill);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Cập nhật chi tiết hoá đơn
        /// </summary>
        /// <param name="billID"> mã hoá đơn </param>
        /// <param name="detailBillList"> chi tiết hoá đơn </param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> updateDetailBill(string billID, List<DetailBillModel> detailBillList)
        {
            try
            {
                using (var context = new Firebase())
                {
                    foreach (var detail in detailBillList)
                    {
                        await context.Client.SetTaskAsync("HoaDon/" + billID + "/ChiTietHoaDon/" + detail.MaSanPham + "-" + detail.MaKichThuoc, detail);
                    }

                    return ("Cập nhật chi tiết hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        /// Cập nhật mã bàn trong hoá đơn
        /// </summary>
        /// <param name="billID"> mã hoá đơn </param>
        /// <param name="tableID"> mã bàn </param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> updateTableIDInBill(string billID, string tableID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("HoaDon/" + billID, new { MaBan = tableID });

                    return ("Cập nhật hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        /// Lấy danh sách chi tiết hoá đơn
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public async Task<(string, List<DetailBillModel>)> getDetailBill(string billID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon/" + billID + "/ChiTietHoaDon");
                    Dictionary<string, DetailBillModel> billData = billResponse.ResultAs<Dictionary<string, DetailBillModel>>();
                    List<DetailBillModel> detailBillList = billData.Values.ToList();

                    if (detailBillList != null)
                        return ("Lấy danh sách chi tiết thành công", detailBillList);
                    else
                        return ("Lấy danh sách chi tiết thất bại", null);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
}
