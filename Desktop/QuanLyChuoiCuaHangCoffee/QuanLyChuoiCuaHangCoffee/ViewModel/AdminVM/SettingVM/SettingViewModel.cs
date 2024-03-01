using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.SettingVM
{
    public class SettingViewModel : BaseViewModel
    {
        #region variable

        private string _Username { get; set; }
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }

        private string _Name { get; set; }
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private DateTime _DOB { get; set; }
        public DateTime DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }

        private bool _IsEdit { get; set; }
        public bool IsEdit { get => _IsEdit; set { _IsEdit = value; OnPropertyChanged(); } }

        private int _Role { get; set; }
        public int Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private string _Position { get; set; }  
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }

        private string _Phone { get; set; }
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email { get; set; }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Password { get; set; }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _PasswordConfirm { get; set; }
        public string PasswordConfirm { get => _PasswordConfirm; set { _PasswordConfirm = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListPosition { get; set; }
        public ObservableCollection<string> ListPosition { get => _ListPosition; set { _ListPosition = value; OnPropertyChanged(); } }

        private string _SelectedPositionItem { get; set; }
        public string SelectedPositionItem { get => _SelectedPositionItem; set { _SelectedPositionItem = value; OnPropertyChanged(); } }

        private string _BaseSalary { get; set; }
        public string BaseSalary { get => _BaseSalary; set { _BaseSalary = value; OnPropertyChanged(); } }

        private double _CoefficientsSalary { get; set; }
        public double CoefficientsSalary { get => _CoefficientsSalary; set { _CoefficientsSalary = value; OnPropertyChanged(); } }

        private string _lbButton { get; set; }
        public string lbButton { get => _lbButton; set { _lbButton = value; OnPropertyChanged(); } }

        private string _lbButtonLogout { get; set; }
        public string lbButtonLogout { get => _lbButtonLogout; set { _lbButtonLogout = value; OnPropertyChanged(); } }

        #endregion

        #region command
        public ICommand LoadInformation { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Save { get; set; }
        public ICommand LogOut { get; set; }
        public ICommand SelectedPositionChanged { get; set; }
        public ICommand PasswordChanged { get; set;}
        public ICommand PasswordConfirmChanged { get; set; }
        #endregion

        public SettingViewModel()
        {
            LoadInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Username = AdminServices.UserNameNhanVien;
                Name = AdminServices.TenNhanVien;
                DOB = AdminServices.NgSinhNhanVien;
                Position = AdminServices.ChucVuNhanVien;
                Phone = AdminServices.SoDT;
                Email = AdminServices.EmailNhanVien;
                BaseSalary = AdminServices.LuongCoBan;
                CoefficientsSalary = AdminServices.HeSoLuong;
                SelectedPositionItem = AdminServices.ChucVuNhanVien;
                ListPosition = new ObservableCollection<string>(Positions.ListChucVu);
                Role = AdminServices.Role;
                IsEdit = false;
                lbButton = "Sửa thông tin";
                lbButtonLogout = "Đăng xuất";
            });

            SelectedPositionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var position = PositionServices.Ins.FindPosition(SelectedPositionItem);
                BaseSalary = Helper.FormatVNMoney((decimal)position.Result.LUONGCOBAN);
                CoefficientsSalary = (double)position.Result.HESOLUONG;
            });

            LogOut = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsEdit == false)
                {
                    MessageBoxCF ms = new MessageBoxCF("Bạn muốn đăng xuất?", MessageType.Waitting, MessageButtons.YesNo);
                    if (ms.ShowDialog() == true)
                    {
                        MainAdminWindow w = Application.Current.Windows.OfType<MainAdminWindow>().FirstOrDefault();
                        w.Hide();

                        LoginWindow login = new LoginWindow();
                        login.Show();

                        w.Close();
                    } 
                }
                else
                {
                    IsEdit = false;
                    lbButtonLogout = "Đăng xuất";
                    lbButton = "Sửa thông tin";
                }

            });

            Edit = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (IsEdit == false)
                {
                    IsEdit = true;
                    lbButton = "Lưu thông tin";
                    lbButtonLogout = "Huỷ";
                } else
                {
                    if (PasswordConfirm != AdminServices.PasswordNhanVien)
                    {
                        MessageBoxCF ms = new MessageBoxCF("Mật khẩu hiện tại không khớp", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                        IsEdit = false;
                        lbButton = "Sửa thông tin";
                        return;
                    }
                    await AdminServices.Ins.EditSetting(AdminServices.MaNhanVien, Phone, Email, Password, AdminServices.PasswordNhanVien);
                    await AdminServices.Ins.LoadInfoEdit(AdminServices.MaNhanVien);
                    MessageBoxCF mb = new MessageBoxCF("Sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();

                    lbButton = "Sửa thông tin";
                    lbButtonLogout = "Đăng xuất";
                    IsEdit = false;
                }
            });

            PasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });

            PasswordConfirmChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PasswordConfirm = p.Password;
            });

            Save = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                MessageBoxCF ms = new MessageBoxCF("Bạn muốn lưu thông tin lương?", MessageType.Waitting, MessageButtons.YesNo);
                if (ms.ShowDialog() == true)
                {
                    await AdminServices.Ins.UpdateSalary(SelectedPositionItem, decimal.Parse(BaseSalary), CoefficientsSalary);
                    MessageBoxCF mb = new MessageBoxCF("Lưu thông tin lương thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });
        }
    }
}
