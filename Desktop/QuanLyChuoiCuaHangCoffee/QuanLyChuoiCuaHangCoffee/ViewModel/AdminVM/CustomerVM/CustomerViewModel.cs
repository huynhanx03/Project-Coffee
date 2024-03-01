using Library.ViewModel;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.Admin.CustomerPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.CustomerVM
{
    public partial class CustomerViewModel : BaseViewModel
    {
        #region variable

        public Grid MaskName { get; set; }

        private ObservableCollection<CustomerDTO> _ListCustomer { get; set; }
        public ObservableCollection<CustomerDTO> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }

        private CustomerDTO _SelectedCusItem { get; set; }
        public CustomerDTO SelectedCusItem { get => _SelectedCusItem; set { _SelectedCusItem = value; OnPropertyChanged(); } }

        private string _NameCus { get; set; }
        public string NameCus { get => _NameCus; set { _NameCus = value; OnPropertyChanged(); } }

        private string _PhoneCus { get; set; }
        public string PhoneCus { get => _PhoneCus; set { _PhoneCus = value; OnPropertyChanged(); } }

        private string _AddressCus { get; set; }
        public string AddressCus { get => _AddressCus; set { _AddressCus = value; OnPropertyChanged(); } }

        private string _RankCus { get; set; }
        public string RankCus { get => _RankCus; set { _RankCus = value; OnPropertyChanged(); } }

        private string _PointCus { get; set; }
        public string PointCus { get => _PointCus; set { _PointCus = value; OnPropertyChanged(); } }

        private DateTime _DateCreatedCus { get; set; }
        public DateTime DateCreatedCus { get => _DateCreatedCus; set { _DateCreatedCus = value; OnPropertyChanged(); } }

        #endregion

        #region command 

        public ICommand MaskNameCustomer { get; set; }
        public ICommand ExportFileCF { get; set; }
        public ICommand LoadInfoCus { get; set; }
        public ICommand LoadCusList { get; set; }
        public ICommand AddCus { get; set; }
        public ICommand EditCus { get; set; }
        public ICommand DeleteCus { get; set; }

        #endregion

        public CustomerViewModel()
        {
            MaskNameCustomer = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadCusList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadListCus();
            });

            DeleteCus = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedCusItem == null)
                {
                    MessageBoxCF mb = new MessageBoxCF("Vui lòng chọn khách hàng để xoá", MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
                MessageBoxCF ms = new MessageBoxCF("Bạn muốn xoá khách hàng này?", MessageType.Waitting, MessageButtons.YesNo);
                if (ms.ShowDialog() == true)
                {
                    await CustomerServices.Ins.DeleteCus(SelectedCusItem.IDKHACHHANG);
                    MessageBoxCF mb = new MessageBoxCF("Xoá thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();
                    await LoadListCus();
                    PhoneCus = "";
                    AddressCus = "";
                    PointCus = "";
                    RankCus = "";
                    DateCreatedCus = DateTime.Parse("01/01/0001");
                }
            });

            LoadInfoCus = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                NameCus = SelectedCusItem.HOTEN;
                PhoneCus = SelectedCusItem.SODT;
                AddressCus = SelectedCusItem.EMAIL;
                PointCus = SelectedCusItem.TICHDIEM.ToString();
                RankCus = SelectedCusItem.HANGTHANHVIEN;
                DateCreatedCus = SelectedCusItem.NGBATDAU;
            });

            AddCus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            EditCus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedCusItem == null)
                {
                    MessageBoxCF ms = new MessageBoxCF("Vui lòng chọn khách hàng để sửa thông tin", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }
                MaskName.Visibility = Visibility.Visible;
                EditCusWindow wd = new EditCusWindow();
                NameEdit = SelectedCusItem.HOTEN;
                DOBEdit = SelectedCusItem.DOB;
                SelectedDate = SelectedCusItem.DOB;
                EmailEdit = SelectedCusItem.EMAIL;
                PhoneEdit = SelectedCusItem.SODT;
                CCCDEdit = SelectedCusItem.CCCD;
                AddressEdit = SelectedCusItem.DCHI;
                UsernameEdit = SelectedCusItem.USERNAME;
                PasswordEdit = SelectedCusItem.USERPASSWORD;
                wd.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            ExportFileCF = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = "Customer",
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
                    worksheet.Cells[1, 1].Value = "Mã khách hàng";
                    worksheet.Cells[1, 2].Value = "Tên khách hàng";
                    worksheet.Cells[1, 3].Value = "Số điện thoại";
                    worksheet.Cells[1, 4].Value = "Địa chỉ";
                    worksheet.Cells[1, 5].Value = "Thân thiết";

                    // Dữ liệu
                    int count = 2;
                    foreach (var item in ListCustomer)
                    {
                        worksheet.Cells[count, 1].Value = item.IDKHACHHANG;
                        worksheet.Cells[count, 2].Value = item.HOTEN;
                        worksheet.Cells[count, 3].Value = item.SODT;
                        worksheet.Cells[count, 4].Value = item.DCHI;
                        worksheet.Cells[count, 5].Value = item.HANGTHANHVIEN;

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

            //page add cus
            ConfirmAddCus = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                CustomerServices.Ins.Register(NameRe, EmailRe, PhoneRe, UsernameRe, PasswordRe);
                var cus = CustomerServices.Ins.FindCus(PhoneRe);
                if (cus != null)
                {
                    await LoadListCus();
                } else
                {
                    MessageBoxCF ms = new MessageBoxCF("Thêm thất bại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });

            //page edit cus
            SelectedDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                if (p.SelectedDate.Value > DateTime.Now.AddYears(-15))
                {
                    MessageBoxCF ms = new MessageBoxCF("Khách hàng phải lớn hơn 15 tuổi", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    p.SelectedDate = SelectedDate;
                    return;
                }
                DOBEdit = p.SelectedDate.Value;
            });

            ConfirmEditCus = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsNullNameEdit = IsNullEmailEdit = IsNullPhoneEdit = false;
                if (string.IsNullOrEmpty(NameEdit))
                {
                    IsNullNameEdit = true;
                }
                if (string.IsNullOrEmpty(EmailEdit))
                {
                    IsNullEmailEdit = true;
                }
                if (string.IsNullOrEmpty(PhoneEdit))
                {
                    IsNullPhoneEdit = true;
                }

                if (IsNullNameEdit || IsNullEmailEdit || IsNullPhoneEdit)
                {
                    return;
                }

                (bool f, string s) = await CustomerServices.Ins.EditCustomer(SelectedCusItem.IDKHACHHANG, NameEdit, DOBEdit, EmailEdit, PhoneEdit, CCCDEdit, AddressEdit, UsernameEdit, PasswordEdit);
                MessageBoxCF ms = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                ResetProperty();
                await LoadListCus();
            });
        }

        private async Task LoadListCus()
        {
            //ListCustomer = new ObservableCollection<CustomerDTO>(await CustomerServices.Ins.GetAllCus());
            List<CustomerDTO> customerList = await CustomerServices.Ins.GetAllCus();
            customerList.Reverse();
            ListCustomer = new ObservableCollection<CustomerDTO>(customerList);
        }
    }
}
