using Library.ViewModel;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.BillsVM
{
    public partial class BillsViewModel : BaseViewModel
    {
        private bool _IsGettingSource { get; set; }
        public bool IsGettingSource
        {
            get => _IsGettingSource;
            set
            {
                _IsGettingSource = value;
                OnPropertyChanged();
            }
        }

        public static Grid MaskName { get; set; }

        public ICommand MaskNameCF { get; set; }
        public ICommand LoadSalesInvoicePage { get; set; }
        public ICommand LoadImportInvoicePage { get; set; }

        public BillsViewModel()
        {
            SelectedDateEnd = DateTime.Now;
            SelectedDateStart = DateTime.Now.AddDays(-30);
            SelectedDateEndImport = DateTime.Now;
            SelectedDateStartImport = DateTime.Now.AddDays(-30);
            #region main page bills
            MaskNameCF = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadSalesInvoicePage = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                //ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                SalesInvoicePage w = new SalesInvoicePage();
                p.Content = w;
            });

            LoadImportInvoicePage = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                await LoadListImportInvoice(SelectedDateStartImport, SelectedDateEndImport);
                //ListImportInvoice = new ObservableCollection<ImportBillDTO>(await ImportBillServices.Ins.GetAllBill(SelectedDateStartImport, SelectedDateEndImport));
                ImportInvoicePage w = new ImportInvoicePage();
                p.Content = w;
            });
            #endregion

            #region page sales invoice
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

                        worksheet.Cells[count, 4].Style.Numberformat.Format = "dd/MM/yyyy";

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

            SelectedDateStartChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart > SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    IsGettingSource = false;
                } else
                {
                    IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    IsGettingSource = false;
                }
            });

            SelectedDateEndChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart > SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    IsGettingSource = false;
                } else
                {
                    IsGettingSource = true;
                    await LoadListSaleInvoice(SelectedDateStart, SelectedDateEnd);
                    IsGettingSource = false;
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

                    BillDetailWindow w = new BillDetailWindow();
                    w.ShowDialog();
                }
            });

            closeCF = new RelayCommand<System.Windows.Window>((p) => { return true; }, (p) =>
            {
                p.Close();

            });
            #endregion

            #region page import invoice
            SelectedDateStartImportChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStartImport > SelectedDateEndImport)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    //ListImportInvoice = new ObservableCollection<ImportBillDTO>(await ImportBillServices.Ins.GetAllBill(SelectedDateStartImport, SelectedDateEndImport));
                    await LoadListImportInvoice(SelectedDateStartImport, SelectedDateEndImport);
                    IsGettingSource = false;
                }
                else
                {
                    IsGettingSource = true;
                    //ListImportInvoice = new ObservableCollection<ImportBillDTO>(await ImportBillServices.Ins.GetAllBill(SelectedDateStartImport, SelectedDateEndImport));
                    await LoadListImportInvoice(SelectedDateStartImport, SelectedDateEndImport);
                    IsGettingSource = false;
                }
            });

            SelectedDateEndImportChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStartImport > SelectedDateEndImport)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    await LoadListImportInvoice(SelectedDateStartImport, SelectedDateEndImport);
                    IsGettingSource = false;
                }
                else
                {
                    IsGettingSource = true;
                    await LoadListImportInvoice(SelectedDateStartImport, SelectedDateEndImport);
                    IsGettingSource = false;
                }
            });

            LoadInfoImportInvoice = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedImportInvoiceItem != null)
                {
                    MaskName.Visibility = System.Windows.Visibility.Visible;
                    EmployeeImport = SelectedImportInvoiceItem.TENNHANVIEN;
                    DateBillImport = SelectedImportInvoiceItem.NGNHAPKHO.ToString("dd/MM/yyyy");
                    IsGettingSource = true;
                    ListImportDetail = new ObservableCollection<IngredientsDTO>(await ImportBillServices.Ins.GetIngredientFromBill(SelectedImportInvoiceItem.MAPHIEU));
                    IsGettingSource = false;

                    ImportIngredientDetail w = new ImportIngredientDetail();
                    w.ShowDialog();
                    MaskName.Visibility = System.Windows.Visibility.Collapsed;
                }
            });

            ExportFileImoprtInvoiceCF = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = "ImportInvoice",
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
                    worksheet.Cells[1, 1].Value = "Mã phiếu nhập";
                    worksheet.Cells[1, 2].Value = "Tên nhân viên";
                    worksheet.Cells[1, 3].Value = "Ngày nhập";
                    worksheet.Cells[1, 4].Value = "Tổng trị giá hoá đơn";

                    // Dữ liệu
                    int count = 2;
                    foreach (var item in ListImportInvoice)
                    {
                        worksheet.Cells[count, 1].Value = item.MAPHIEU;
                        worksheet.Cells[count, 2].Value = item.TENNHANVIEN;
                        worksheet.Cells[count, 3].Value = item.NGNHAPKHO.ToString("dd/MM/yyyy");
                        worksheet.Cells[count, 4].Value = item.TRIGIASTR;

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
            #endregion
        }
        private async Task LoadListSaleInvoice(DateTime _datestart, DateTime _dateend)
        {
            List<OrderBillsDTO> _listSale = await OrderBillServices.Ins.GetAllBill(_datestart, _dateend);
            _listSale.Reverse();
            ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(_listSale);
        }

        private async Task LoadListImportInvoice(DateTime _datestart, DateTime _dateend)
        {
            List<ImportBillDTO> _listImport = await ImportBillServices.Ins.GetAllBill(_datestart, _dateend);
            _listImport.Reverse();
            ListImportInvoice = new ObservableCollection<ImportBillDTO>(_listImport);
        }
    }
}
