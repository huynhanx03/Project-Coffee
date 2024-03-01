using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.Admin;
using QuanLyChuoiCuaHangCoffee.Views.Customer;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public static Grid Mask { get; set; }
        public ICommand LoadLoginPage { get; set; }
        public ICommand LoadForgotPassPage { get; set; }
        public ICommand LoadRegisterPage { get; set; }
        public ICommand LoadVerificationPage { get; set; }
        public ICommand LoadMask { get; set; }
        public ICommand LoginCF { get; set; }
        public ICommand RegisterML { get; set; }
        public ICommand PasswordChangedCF { get; set; }
        public ICommand PasswordRegChangedCF { get; set; }

        #region variable
        private string _usernamelog;
        public string Usernamelog
        {
            get { return _usernamelog; }
            set { _usernamelog = value; OnPropertyChanged(); }
        }
        private string _passwordlog;

        public string Passwordlog
        {
            get { return _passwordlog; }
            set { _passwordlog = value; OnPropertyChanged(); }
        }

        private string _fullnamereg;
        public string Fullnamereg
        {
            get { return _fullnamereg; }
            set { _fullnamereg = value; OnPropertyChanged(); }
        }

        private string _phonereg;
        public string Phonereg
        {
            get { return _phonereg; }
            set { _phonereg = value; OnPropertyChanged(); }
        }

        private string _emailreg;
        public string Emailreg
        {
            get { return _emailreg; }
            set { _emailreg = value; OnPropertyChanged(); }
        }

        private string _usernamereg;
        public string Usernamereg
        {
            get { return _usernamereg; }
            set { _usernamereg = value; OnPropertyChanged(); }
        }
        private string _passwordreg;
        public string Passwordreg
        {
            get { return _passwordreg; }
            set { _passwordreg = value; OnPropertyChanged(); }
        }

        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        private bool _IsNullNameReg;
        public bool IsNullNameReg
        {
            get { return _IsNullNameReg; }
            set { _IsNullNameReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullPhoneReg;
        public bool IsNullPhoneReg
        {
            get { return _IsNullPhoneReg; }
            set { _IsNullPhoneReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullEmailReg;
        public bool IsNullEmailReg
        {
            get { return _IsNullEmailReg; }
            set { _IsNullEmailReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullUserReg;
        public bool IsNullUserReg
        {
            get { return _IsNullUserReg; }
            set { _IsNullUserReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullPasswordReg;
        public bool IsNullPasswordReg
        {
            get { return _IsNullPasswordReg; }
            set { _IsNullPasswordReg = value; OnPropertyChanged(); }
        }
        #endregion
        public LoginViewModel()
        {
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
                Passwordlog = "";
            });

            LoadForgotPassPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });

            LoadRegisterPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                RegisterWindow rgw = new RegisterWindow();
                Mask.Visibility = Visibility.Visible;
                rgw.ShowDialog();
            });

            LoadVerificationPage = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                MainFrame.Content = new VerificationPage();
            });

            LoadMask = new RelayCommand<Grid>((p) => { return true; }, (P) =>
            {
                Mask = P;
            });

            LoginCF = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string username = Usernamelog;
                string password = Passwordlog;

                IsSaving = true;
                Mask.Visibility = Visibility.Visible;

                await checkValidateAccount(username, password, p);

                IsSaving = false;
                Mask.Visibility = Visibility.Hidden;
            });

            PasswordChangedCF = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordlog = p.Password;
            });

            PasswordRegChangedCF = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordreg = p.Password;
            });

            RegisterML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                IsNullNameReg = IsNullEmailReg = IsNullUserReg = IsNullPasswordReg = IsNullPhoneReg = false;

                if (string.IsNullOrEmpty(Fullnamereg)) IsNullNameReg = true;
                if (string.IsNullOrEmpty(Emailreg)) IsNullEmailReg = true;
                if (string.IsNullOrEmpty(Usernamereg)) IsNullUserReg = true;
                if (string.IsNullOrEmpty(Passwordreg)) IsNullPasswordReg = true;
                if (string.IsNullOrEmpty(Phonereg)) IsNullPhoneReg = true;

                if (IsNullNameReg || IsNullEmailReg || IsNullUserReg || IsNullPasswordReg || IsNullPhoneReg) return;

                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                string matchPhone = @"^(0|\\+84|84)[3|5|7|8|9][0-9]{8}$";
                Regex regphone = new Regex(matchPhone);

                if (regphone.IsMatch(Phonereg) == false)
                {
                    MessageBoxCF ms = new MessageBoxCF("Số điện thoại không hợp lệ", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (await Task.Run(() => CustomerServices.Ins.CheckPhonenumberCustomer(Phonereg, "-1")))
                {
                    MessageBoxCF ms = new MessageBoxCF("Số điện thoại đã tồn tại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (reg.IsMatch(Emailreg) == false)
                {
                    MessageBoxCF ms = new MessageBoxCF("Email không hợp lệ", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (await Task.Run(() => CustomerServices.Ins.CheckEmailCustomer(Emailreg, "-1")))
                {
                    MessageBoxCF ms = new MessageBoxCF("Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                string pattern = ".{8,}$";
                Regex regPassWord = new Regex(pattern);
                if (regPassWord.IsMatch(Passwordreg) == false)
                {
                    MessageBoxCF ms = new MessageBoxCF("Mật khẩu không hợp lệ. Mật khẩu phải có ít nhất 8 kí tự.", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                string fullname = Fullnamereg;
                string phonenumber = Phonereg;
                string email = Emailreg;
                string usernamereg = Usernamereg;
                string passwordreg = Passwordreg;

                CustomerServices.Ins.Register(fullname, email, phonenumber, usernamereg, passwordreg);

                //đóng window đăng kí
                LoginViewModel.Mask.Visibility = Visibility.Collapsed;
                RegisterWindow w = Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault();
                w.Close();
            });
        }

        public async Task checkValidateAccount(string usr, string pwd, Label lbl)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                lbl.Content = "Vui lòng điền đầy đủ thông tin";
                return;
            }

            // Thực hiện Login tài khoản customer
            (bool loginCus, string messCus, CustomerDTO cus) = await Task.Run(() => CustomerServices.Ins.Login(usr, pwd));

            // thực hiện Login tài khoản admin
            (bool loginAdmin, string messAdmin) = await Task.Run(() => AdminServices.Ins.Login(usr, pwd));

            if (loginCus)
            {

                MainCustomerWindow w1 = new MainCustomerWindow();
                w1.Show();

                LoginWindow w = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                w.Close();
            }
            else if (loginAdmin)
            {
                MainAdminWindow w1 = new MainAdminWindow();
                w1.Show();

                LoginWindow w = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                w.Close();
            }
            else
            {
                lbl.Content = messCus;
            }
        }
    }
}
