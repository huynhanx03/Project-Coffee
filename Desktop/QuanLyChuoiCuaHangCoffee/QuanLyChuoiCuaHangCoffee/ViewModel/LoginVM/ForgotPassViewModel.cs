using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM
{
    public class ForgotPassViewModel : BaseViewModel
    {
        public ICommand CancelForgotPass { get; set; }
        public ICommand TypingYourEmail { get; set; }
        public ICommand TypingVerificationNum { get; set; }
        public ICommand CreateNewPass { get; set; }
        public ICommand NewpassChanged { get; set; }
        public ICommand ComfirmNewPassChanged { get; set; }

        private int Number;
        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; OnPropertyChanged(); }
        }

        private string _verificationnumber;
        public string Verificationnumber
        {
            get { return _verificationnumber; }
            set { _verificationnumber = value; OnPropertyChanged(); }
        }

        private string _newpass;
        public string Newpass
        {
            get { return _newpass; }
            set { _newpass = value; OnPropertyChanged(); }
        }

        private string _confirmnewpass;
        private string Confirmnewpass
        {
            get { return _confirmnewpass; }
            set { _confirmnewpass = value; OnPropertyChanged(); }
        }

        public ForgotPassViewModel()
        {
            CancelForgotPass = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginViewModel.MainFrame.Content = new LoginPage();
            });

            TypingYourEmail = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                using (var context = new CoffeeManagementEntities())
                {
                    // Kiểm tra email có tồn tại không
                    var email = await(from s in context.KHACHHANGs
                                      where s.USER.EMAIL == Mail
                                      select new CustomerDTO
                                      {
                                          IDKHACHHANG = s.IDKHACHHANG
                                      }).FirstOrDefaultAsync();



                    if (Mail == null)
                        p.Content = "*Điền địa chỉ email liên kết tài khoản";
                    else if (email == null)
                        p.Content = "*Email chưa được đăng ký tài khoản";
                    else
                    {
                        Random random = new Random();
                        Number = random.Next(1, 999999);
                        SendEmail(Number.ToString());
                        LoginViewModel.MainFrame.Content = new VerificationPage();
                    }
                }
            });

            TypingVerificationNum = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                if (Number.ToString() == null)
                    p.Content = "*Vui lòng điền mã xác nhận";
                else if (Number.ToString() != Verificationnumber)
                    p.Content = "*Mã xác nhận không chính xác";
                else
                {
                    LoginViewModel.MainFrame.Content = new CreateNewPassPage();
                }
            });

            NewpassChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Newpass = p.Password;
            });

            ComfirmNewPassChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Confirmnewpass = p.Password;
            });

            CreateNewPass = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                if (Newpass == null || Confirmnewpass == null)
                    p.Content = "*Vui lòng nhập đủ thông tin";
                else if (Newpass != Confirmnewpass)
                    p.Content = "*Mật khẩu xác nhận không chính xác";
                else if (Newpass == Confirmnewpass)
                {
                    using (var context = new CoffeeManagementEntities())
                    {
                        var changepass = context.KHACHHANGs.Where(x => x.USER.EMAIL == Mail).FirstOrDefault();
                        changepass.USER.USERPASSWORD = Newpass;
                        context.SaveChanges();
                        LoginViewModel.MainFrame.Content = new LoginPage();
                    }
                }
            });
        }
        public void SendEmail(string content)
        {
            MailAddress fromAddress = new MailAddress("onlynam102003@gmail.com", "EPSRO");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = fromAddress;
            mailMessage.To.Add(Mail);
            mailMessage.Subject = "Khôi phục mật khẩu EPSRO";
            mailMessage.Body = "Mã xác nhận của bạn là: " + content;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("onlynam102003@gmail.com", "mgii caoj snut xadg");
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mailMessage);
        }
    }
}
