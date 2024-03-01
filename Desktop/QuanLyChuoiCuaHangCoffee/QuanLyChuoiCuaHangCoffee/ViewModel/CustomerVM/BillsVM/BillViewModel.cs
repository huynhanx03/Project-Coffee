using Library.ViewModel;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.Customer.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM.BillsVM
{
    public partial class BillViewModel : BaseViewModel
    {
        private DateTime _SelectedDateStart { get; set; }
        public DateTime SelectedDateStart { get => _SelectedDateStart; set { _SelectedDateStart = value; OnPropertyChanged(); } }

        private DateTime _SelectedDateEnd { get; set; }
        public DateTime SelectedDateEnd { get => _SelectedDateEnd; set { _SelectedDateEnd = value; OnPropertyChanged(); } }

        private ObservableCollection<OrderBillsDTO> _ListSaleInvoice { get; set; }
        public ObservableCollection<OrderBillsDTO> ListSaleInvoice { get => _ListSaleInvoice; set { _ListSaleInvoice = value; OnPropertyChanged(); } }

        private OrderBillsDTO _SelectedSaleInvoiceItem { get; set; }
        public OrderBillsDTO SelectedSaleInvoiceItem { get => _SelectedSaleInvoiceItem; set { _SelectedSaleInvoiceItem = value; OnPropertyChanged(); } }

        public ICommand LoadBillsList { get; set; }
        public ICommand SelectedDateStartChanged { get; set; }
        public ICommand SelectedDateEndChanged { get; set; }
        public ICommand ExportFileSaleInvoiceCF { get; set; }
        public ICommand LoadInforSaleInvoice { get; set; }

        public BillViewModel()
        {
            SelectedDateEnd = DateTime.Now;
            SelectedDateStart = DateTime.Now.AddDays(-30);
            LoadBillsList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
            });

            SelectedDateStartChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart > SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    //IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    //IsGettingSource = false;
                }
                else
                {
                    //IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    //IsGettingSource = false;
                }
            });

            SelectedDateEndChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart > SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    //IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    //IsGettingSource = false;
                }
                else
                {
                    //IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    //IsGettingSource = false;
                }
            });

            LoadInforSaleInvoice = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedSaleInvoiceItem != null)
                {
                    tableNumber = SelectedSaleInvoiceItem.MABAN.ToString();
                    DateBill = SelectedSaleInvoiceItem.NGDH.ToString("dd/MM/yyyy");
                    Employee = SelectedSaleInvoiceItem.TENNV;
                    HourBillIn = DateTime.Parse(SelectedSaleInvoiceItem.TIMEIN).ToString("HH:mm:ss");
                    MADH = SelectedSaleInvoiceItem.MADH;
                    CusName = SelectedSaleInvoiceItem.TENKHACHHANG;
                    HourBillOut = SelectedSaleInvoiceItem.TIMEOUT != null ? DateTime.Parse(SelectedSaleInvoiceItem.TIMEOUT).ToString("HH:mm:ss") : "";
                    Note = SelectedSaleInvoiceItem.GHICHU;
                    VoucherPercentage = SelectedSaleInvoiceItem.DISCOUNT;
                    double total = (double)SelectedSaleInvoiceItem.TONGTIEN / (1 - SelectedSaleInvoiceItem.DISCOUNT / 100.0);
                    Total = Helper.FormatVNMoney(Convert.ToDecimal(total));
                    TotalFinal = Helper.FormatVNMoney(SelectedSaleInvoiceItem.TONGTIEN);

                    ListProduct = new ObservableCollection<MenuItemDTO>(await OrderBillServices.Ins.GetProductFromBill(SelectedSaleInvoiceItem.MADH));

                    BillDetailCusWindow w = new BillDetailCusWindow();
                    w.ShowDialog();
                }
            });

            ExportFileSaleInvoiceCF = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = "SaleInvoice",
                    Filter = "Excel |*.xlsx",
                    ValidateNames = true
                };

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                    // Tạo một đối tượng ExcelPackage
                    ExcelPackage package = new ExcelPackage();

                    // Tạo một đối tượng ExcelWorksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã đơn hàng";
                    worksheet.Cells[1, 2].Value = "Tên khách hàng";
                    worksheet.Cells[1, 3].Value = "Mã bàn";
                    worksheet.Cells[1, 4].Value = "Ngày mua hàng";
                    worksheet.Cells[1, 5].Value = "Tổng trị giá hoá đơn";

                    // Dữ liệu
                    int count = 2;
                    foreach (var item in ListSaleInvoice)
                    {
                        worksheet.Cells[count, 1].Value = item.MADH;
                        worksheet.Cells[count, 2].Value = item.TENKHACHHANG;
                        worksheet.Cells[count, 3].Value = item.MABAN;
                        worksheet.Cells[count, 4].Value = item.NGDH;
                        worksheet.Cells[count, 5].Value = item.TONGTIENSTR;

                        count++;
                    }

                    // Lưu file Excel
                    FileInfo fileInfo = new FileInfo(sf.FileName);
                    package.SaveAs(fileInfo);

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                    MessageBoxCF mb = new MessageBoxCF("Xuất file thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });

            closeCF = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }

        private async Task LoadListSaleInvoice(DateTime _datestart, DateTime _dateend)
        {
            List<OrderBillsDTO> _listSale = await OrderBillServices.Ins.GetAllBillByCus(CustomerServices.IDKHACHHANG, _datestart, _dateend);
            _listSale.Reverse();
            ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(_listSale);
        }
    }
}
