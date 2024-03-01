using Library.ViewModel;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.EmployeePage;
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

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.EmployeeVM
{
    public partial class EmployeeViewModel : BaseViewModel
    {
        #region variable
        private string _NameEmployee { get; set; }
        public string NameEmployee { get => _NameEmployee; set { _NameEmployee = value; OnPropertyChanged(); } }

        private string _AddressEmployee { get; set; }
        public string AddressEmployee { get => _AddressEmployee; set { _AddressEmployee = value; OnPropertyChanged(); } }

        private string _PhoneEmployee { get; set; }
        public string PhoneEmployee { get => _PhoneEmployee; set { _PhoneEmployee = value; OnPropertyChanged(); } }

        private string _JobEmployee { get; set; }   
        public string JobEmployee { get => _JobEmployee; set { _JobEmployee = value; OnPropertyChanged(); } }

        private string _SalaryEmployee { get; set; }
        public string SalaryEmployee { get => _SalaryEmployee; set { _SalaryEmployee = value; OnPropertyChanged(); } }

        private string _NumberSalaryEmployee { get; set; }
        public string NumberSalaryEmployee { get => _NumberSalaryEmployee; set { _NumberSalaryEmployee = value; OnPropertyChanged(); } }

        private bool isAdd { get; set; }

        private EmployeeDTO _SelectedEmployeeItem { get; set; }
        public EmployeeDTO SelectedEmployeeItem { get => _SelectedEmployeeItem; set { _SelectedEmployeeItem = value; OnPropertyChanged(); } }

        private ObservableCollection<EmployeeDTO> _ListEmployee { get; set; }
        public ObservableCollection<EmployeeDTO> ListEmployee { get => _ListEmployee; set { _ListEmployee = value; OnPropertyChanged(); } }

        public Grid MaskName { get; set; }

        #endregion

        #region command
        public ICommand EditEmployee { get; set; }
        public ICommand DelEmployee { get; set; }
        public ICommand AddEmployee { get; set; }
        public ICommand LoadInfoEmployee { get; set; }
        public ICommand LoadEmployeeList { get; set; }
        public ICommand MaskNameCustomer { get; set; }
        public ICommand ExportFileCF { get; set; }
        #endregion

        public EmployeeViewModel()
        {
            ListPosition = new ObservableCollection<string>(Positions.ListChucVu);
            isAdd = true;

            MaskNameCustomer = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadEmployeeList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadListEmployee();
            });

            LoadInfoEmployee = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NameEmployee = SelectedEmployeeItem.HOTEN;
                AddressEmployee = SelectedEmployeeItem.EMAIL;
                PhoneEmployee = SelectedEmployeeItem.SODT;
                JobEmployee = SelectedEmployeeItem.CHUCVU;
                SalaryEmployee = SelectedEmployeeItem.LUONGCOBANSTR;
                NumberSalaryEmployee = SelectedEmployeeItem.HESOLUONG.ToString();
            });

            AddEmployee = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedDate = DateTime.Now;
                MaskName.Visibility = Visibility.Visible;
                isAdd = true;
                AddEmployeeWindow w = new AddEmployeeWindow();
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            EditEmployee = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedEmployeeItem == null)
                {
                    MessageBoxCF ms = new MessageBoxCF("Vui lòng chọn nhân viên để sửa thông tin", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }
                MaskName.Visibility = Visibility.Visible;
                isAdd = false;
                AddEmployeeWindow w = new AddEmployeeWindow();
                NameAdd = SelectedEmployeeItem.HOTEN;
                AddressAdd = SelectedEmployeeItem.DIACHI;
                PhoneAdd = SelectedEmployeeItem.SODT;
                EmailAdd = SelectedEmployeeItem.EMAIL;
                CCCDAdd = SelectedEmployeeItem.CCCD;
                SelectedDate = SelectedEmployeeItem.DOB;
                DOBAdd = SelectedDate;
                UsernameAdd = SelectedEmployeeItem.USERNAME;
                PasswordAdd = SelectedEmployeeItem.USERPASSWORD;
                SelectedPosition = SelectedEmployeeItem.CHUCVU;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            DelEmployee = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedEmployeeItem == null)
                {
                    MessageBoxCF ms = new MessageBoxCF("Vui lòng chọn nhân viên để xóa", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }
                MessageBoxCF mb = new MessageBoxCF("Xác nhận xóa nhân viên", MessageType.Waitting, MessageButtons.YesNo);
                if (mb.ShowDialog() == true)
                {
                    await EmployeeServices.Ins.DeleteEmployee(SelectedEmployeeItem.IDNHANVIEN);
                    await LoadListEmployee();
                    NameEmployee = string.Empty;
                    AddressEmployee = string.Empty;
                    PhoneEmployee = string.Empty;
                    JobEmployee = string.Empty;
                    SalaryEmployee = string.Empty;
                    NumberSalaryEmployee = string.Empty;
                }
            });

            ExportFileCF = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = "Employee",
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
                    worksheet.Cells[1, 1].Value = "Mã nhân viên";
                    worksheet.Cells[1, 2].Value = "Tên nhân viên";
                    worksheet.Cells[1, 3].Value = "Số điện thoại";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "Địa chỉ";
                    worksheet.Cells[1, 6].Value = "Chức vụ";

                    // Dữ liệu
                    int count = 2;
                    foreach (var item in ListEmployee)
                    {
                        worksheet.Cells[count, 1].Value = item.IDNHANVIEN;
                        worksheet.Cells[count, 2].Value = item.HOTEN;
                        worksheet.Cells[count, 3].Value = item.SODT;
                        worksheet.Cells[count, 4].Value = item.EMAIL;
                        worksheet.Cells[count, 5].Value = item.DIACHI;
                        worksheet.Cells[count, 6].Value = item.CHUCVU;

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

            //page add employee
            SelectedDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                if (p.SelectedDate.Value > DateTime.Now.AddYears(-18))
                {
                    MessageBoxCF ms = new MessageBoxCF("Nhân viên phải lớn hơn 18 tuổi", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    p.SelectedDate = SelectedDate;
                    return;
                }
                DOBAdd = p.SelectedDate.Value;
            });

            ConfirmAddEmployee = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (isAdd)
                {
                    IsNullNameAdd = IsNullEmailAdd = IsNullPhoneAdd = false;
                    if (string.IsNullOrEmpty(NameAdd))
                    {
                        IsNullNameAdd = true;
                    }
                    if (string.IsNullOrEmpty(EmailAdd))
                    {
                        IsNullEmailAdd = true;
                    }
                    if (string.IsNullOrEmpty(PhoneAdd))
                    {
                        IsNullPhoneAdd = true;
                    }

                    if (IsNullNameAdd || IsNullEmailAdd || IsNullPhoneAdd)
                    {
                        return;
                    }
                    MessageBoxCF ms = new MessageBoxCF("Xác nhận thêm nhân viên", MessageType.Waitting, MessageButtons.YesNo);
                    if (ms.ShowDialog() == true)
                    {
                        (bool f, string s) = await EmployeeServices.Ins.CreateNewEmployee(NameAdd, DOBAdd, EmailAdd, PhoneAdd, CCCDAdd, SelectedPosition, AddressAdd, UsernameAdd, PasswordAdd);
                        MessageBoxCF mb = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        ResetProperty();
                        await LoadListEmployee();
                    }
                } 
                else
                {
                    IsNullNameAdd = IsNullEmailAdd = IsNullPhoneAdd = false;
                    if (string.IsNullOrEmpty(NameAdd))
                    {
                        IsNullNameAdd = true;
                    }
                    if (string.IsNullOrEmpty(EmailAdd))
                    {
                        IsNullEmailAdd = true;
                    }
                    if (string.IsNullOrEmpty(PhoneAdd))
                    {
                        IsNullPhoneAdd = true;
                    }

                    if (IsNullNameAdd || IsNullEmailAdd || IsNullPhoneAdd)
                    {
                        return;
                    }
                    await EmployeeServices.Ins.EditEmployee(SelectedEmployeeItem.IDNHANVIEN, NameAdd, DOBAdd, EmailAdd, PhoneAdd, CCCDAdd, SelectedPosition, AddressAdd, UsernameAdd, PasswordAdd);
                    MessageBoxCF mb = new MessageBoxCF("Sửa thông tin nhân viên thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();
                    await LoadListEmployee();
                }
            });


        }

        private async Task LoadListEmployee()
        {
            List<EmployeeDTO> employeeList = await EmployeeServices.Ins.GetAllEmployee();
            employeeList.Reverse();
            ListEmployee = new ObservableCollection<EmployeeDTO>(employeeList);
        }
        
        private void ResetProperty()
        {
            _AddressAdd = string.Empty;
            _CCCDAdd = string.Empty;
            _DOBAdd = DateTime.Now;
            _EmailAdd = string.Empty;
            _NameAdd = string.Empty;
            _PasswordAdd = string.Empty;
            _PhoneAdd = string.Empty;
            _UsernameAdd = string.Empty;
            _SelectedPosition = string.Empty;
        }
    }
}
