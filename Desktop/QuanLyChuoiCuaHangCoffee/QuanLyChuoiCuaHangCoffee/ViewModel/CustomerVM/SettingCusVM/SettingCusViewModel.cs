using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Library.ViewModel;
using Microsoft.Win32;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Forms;
using QuanLyChuoiCuaHangCoffee.Views.Customer.SettingPage;
using QuanLyChuoiCuaHangCoffee.Properties;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using QuanLyChuoiCuaHangCoffee.Views.Admin;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using QuanLyChuoiCuaHangCoffee.Views.Customer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM.SettingCusVM
{
    public class SettingCusViewModel : BaseViewModel
    {
        #region variable
        private string _ImgSource;
        public string ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; OnPropertyChanged(); }
        }
        private string _NameCus { get; set; }
        public string NameCus { get => _NameCus; set { _NameCus = value; OnPropertyChanged(); } }

        private string _RankCus { get; set; }
        public string RankCus { get => _RankCus; set { _RankCus = value; OnPropertyChanged(); } }

        private string _PhoneCus { get; set; }
        public string PhoneCus { get => _PhoneCus; set { _PhoneCus = value; OnPropertyChanged(); } }

        private string _AddressCus { get; set; }
        public string AddressCus { get => _AddressCus; set { _AddressCus = value; OnPropertyChanged(); } }

        private string _EmailCus { get; set; }
        public string EmailCus { get => _EmailCus; set { _EmailCus = value; OnPropertyChanged(); } }

        private DateTime _DOBCus { get; set; }
        public DateTime DOBCus { get => _DOBCus; set { _DOBCus = value; OnPropertyChanged(); } }

        private string _CCCDCus { get; set; }
        public string CCCDCus { get => _CCCDCus; set { _CCCDCus = value; OnPropertyChanged(); } }

        private string _UsernameCus { get; set; }
        public string UsernameCus { get => _UsernameCus; set { _UsernameCus = value; OnPropertyChanged(); } }

        private string _NewPassword { get; set; }
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }

        private string _CurrentPassword { get; set; }
        public string CurrentPassword { get => _CurrentPassword; set { _CurrentPassword = value; OnPropertyChanged(); } }

        private string _lbButton { get; set; }
        public string lbButton { get => _lbButton; set { _lbButton = value; OnPropertyChanged(); } }

        private string _lbButtonLogout { get; set; }
        public string lbButtonLogout { get => _lbButtonLogout; set { _lbButtonLogout = value; OnPropertyChanged(); } }

        private bool _IsEdit { get; set; }
        public bool IsEdit { get => _IsEdit; set { _IsEdit = value; OnPropertyChanged(); } }
        #endregion

        #region command
        public ICommand LoadInformation { get; set; }
        public ICommand Edit { get; set; }
        public ICommand LogOut { get; set; }
        public ICommand ChangeAvatar { get; set; }
        public ICommand PasswordChanged { get; set; }
        public ICommand CurrentPasswordChanged { get; set; }
        public ICommand SelectedDateChanged { get; set; }
        #endregion

        public SettingCusViewModel()
        {
            LoadInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadInfor();
                IsEdit = false;
                lbButton = "Sửa thông tin";
                lbButtonLogout = "Đăng xuất";
            });

            ChangeAvatar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "JPG File (.jpg)|*.jpg";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {

                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(dlg.FileName);
                    img.EndInit();
                    MainSettingCusPage.Image.ImageSource = img;
                    Account account = new Account("dg0uneomp", "924294962494475", "Ahrb-2beUzb0TEJpKjHck2IYCGI");

                    Cloudinary cloudinary = new Cloudinary(account);
                    cloudinary.Api.Secure = true;
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(dlg.FileName)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    ImgSource = uploadResult.Url.ToString();

                    (bool b, string s) = await CustomerServices.Ins.UpdateAvatar(CustomerServices.IDKHACHHANG, ImgSource);
                    await CustomerServices.Ins.LoadAvatar(CustomerServices.IDKHACHHANG);
                    await CustomerServices.Ins.LoadInforEdit(CustomerServices.IDKHACHHANG);
                    MessageBoxCF ms = new MessageBoxCF(s, b ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    LoadInfor();
                    
                }
            });

            PasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPassword = p.Password;
            });

            CurrentPasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                CurrentPassword = p.Password;
            });

            Edit = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (IsEdit == false)
                {
                    IsEdit = true;
                    lbButton = "Lưu thông tin";
                    lbButtonLogout = "Huỷ";
                }
                else
                {
                    if (CurrentPassword != CustomerServices.PASSWORD)
                    {
                        MessageBoxCF ms = new MessageBoxCF("Mật khẩu hiện tại không khớp", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                        IsEdit = false;
                        lbButton = "Sửa thông tin";
                        lbButtonLogout = "Đăng xuất";
                        return;
                    }
                    await CustomerServices.Ins.EditSetting(CustomerServices.IDKHACHHANG, PhoneCus, EmailCus, DOBCus, AddressCus, CCCDCus, NewPassword, CurrentPassword);
                    await CustomerServices.Ins.LoadInforEdit(CustomerServices.IDKHACHHANG);
                    MessageBoxCF mb = new MessageBoxCF("Sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();

                    lbButton = "Sửa thông tin";
                    lbButtonLogout = "Đăng xuất";
                    IsEdit = false;
                    LoadInfor();
                }
            });

            SelectedDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                DOBCus = p.SelectedDate.Value;
            });

            LogOut = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsEdit == false)
                {
                    MessageBoxCF ms = new MessageBoxCF("Bạn muốn đăng xuất?", MessageType.Waitting, MessageButtons.YesNo);
                    if (ms.ShowDialog() == true)
                    {
                        MainCustomerWindow w = System.Windows.Application.Current.Windows.OfType<MainCustomerWindow>().FirstOrDefault();
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
        }

        private void LoadInfor()
        {
            NameCus = CustomerServices.TENKH;
            RankCus = CustomerServices.RANKKH;
            PhoneCus = CustomerServices.SDT;
            AddressCus = CustomerServices.DIACHI;
            EmailCus = CustomerServices.EMAIL;
            DOBCus = CustomerServices.DOB;
            CCCDCus = CustomerServices.CCCD;
            UsernameCus = CustomerServices.USERNAME;
            ImgSource = CustomerServices.IMAGESOURCE;
        }
    }
}
