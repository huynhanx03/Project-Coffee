using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using QuanLyChuoiCuaHangCoffee.Views.Admin.VoucherPage;
using QuanLyChuoiCuaHangCoffee.Utils;
using System.Windows.Input;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM
{
    public partial class VoucherViewModel : BaseViewModel
    {
        #region variable
        public static int NumberCustomer;
        string APP_EMAIL;
        string APP_PASSWORD;

        private ComboBoxItem perCus;
        public ComboBoxItem PerCus
        {
            get { return perCus; }
            set { perCus = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _ReleaseCustomerList;
        public ComboBoxItem ReleaseCustomerList
        {
            get { return _ReleaseCustomerList; }
            set
            {
                _ReleaseCustomerList = value;
            }
        }

        private ObservableCollection<VoucherDTO> _ReleaseVoucherList { get; set; }
        public ObservableCollection<VoucherDTO> ReleaseVoucherList
        {
            get { return _ReleaseVoucherList; }
            set { _ReleaseVoucherList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CustomerEmail> _ListCustomerEmail;
        public ObservableCollection<CustomerEmail> ListCustomerEmail
        {
            get { return _ListCustomerEmail; }
            set { _ListCustomerEmail = value; OnPropertyChanged(); }
        }

        private List<string> _WaitingMiniVoucher { get; set; }
        public List<string> WaitingMiniVoucher
        {
            get { return _WaitingMiniVoucher; }
            set { _WaitingMiniVoucher = value; OnPropertyChanged(); }
        }

        #endregion

        #region command
        public ICommand RefreshEmailListCF { get; set; }
        public ICommand CalculateNumberOfVoucherCM { get; set; }
        public ICommand ReleaseVoucherCF { get; set; }
        #endregion

        protected async Task<(bool, string)> sendHtmlEmail(List<CustomerEmail> customerList, List<List<string>> ListCodePerEmailList)
        {
            var appSettings = ConfigurationManager.AppSettings;
            APP_EMAIL = appSettings["APP_EMAIL"];
            APP_PASSWORD = appSettings["APP_PASSWORD"];

            List<Task> listSendEmailTask = new List<Task>();
            try
            {
                for (int i = 0; i < customerList.Count; i++)
                {
                    listSendEmailTask.Add(sendEmailForACustomer(customerList[i], ListCodePerEmailList[i]));
                }
                await Task.WhenAny(listSendEmailTask);
                return (true, "Gửi thành công");

            }
            catch (Exception)
            {
                return (false, "Phát sinh lỗi trong quá trình gửi mail. Vui lòng thử lại!");
            }
        }

        private Task sendEmailForACustomer(CustomerEmail customerEmail, List<string> listCode)
        {
            //SMTP CONFIG
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(APP_EMAIL, APP_PASSWORD);

            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.Subject = "Voucher từ Coffee EPSRO";
            string voucherListHtml = "<ul>";

            foreach (var code in listCode)
            {
                voucherListHtml += "<li>" + code + "</li>";
            }

            voucherListHtml += "</ul>";

            string emailBody = "<h1>Chào " + customerEmail.Name + "</h1><br>" +
                               "<p>Chúng tôi gửi đến bạn " + listCode.Count + " voucher</p><br>" +
                               "<p>Đây là danh sách voucher của bạn:</p><br>" +
                               voucherListHtml;

            mail.Body = emailBody;

            mail.From = new MailAddress(APP_EMAIL, "Coffee EPSRO");
            mail.To.Add(customerEmail.Email);
            
            return smtp.SendMailAsync(mail);
        }

        public async Task ReleaseVoucherFunc(ReleaseVoucherWindow p)
        {
            string mess = "Số voucher không chia hết cho khách hàng!";
            if (ReleaseVoucherList.Count == 0)
            {
                MessageBoxCF mb = new MessageBoxCF("Danh sách voucher đang trống!", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                return;
            }
            foreach (var item in ListCustomerEmail)
            {
                if (string.IsNullOrEmpty(item.Email))
                {
                    MessageBoxCF mb = new MessageBoxCF("Tồn tại email trống", MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
                if (!Utils.RegexUtilities.IsValidEmail(item.Email))
                {
                    MessageBoxCF mb = new MessageBoxCF("Tồn tại email không hợp lệ", MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
            }
            //top 5 customer
            if (NumberCustomer == 5)
            {
                if (ListCustomerEmail.Count == 0)
                {
                    MessageBoxCF mb = new MessageBoxCF("Danh sách khách hàng đang trống!", MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
                else
                {
                    if (ReleaseVoucherList.Count % ListCustomerEmail.Count != 0)
                    {
                        MessageBoxCF mb = new MessageBoxCF(mess, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        return;
                    }
                }
            }
            // input customer mail   // new customer
            else if (NumberCustomer == -1 || NumberCustomer == 0)
            {
                if (ListCustomerEmail.Count == 0)
                {
                    MessageBoxCF mb = new MessageBoxCF("Danh sách khách hàng đang trống!", MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
                if (ReleaseVoucherList.Count > ListCustomerEmail.Count)
                {
                    if (ReleaseVoucherList.Count % ListCustomerEmail.Count != 0)
                    {
                        MessageBoxCF mb = new MessageBoxCF(mess, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        return;
                    }
                }
                else if (ReleaseVoucherList.Count < ListCustomerEmail.Count)
                {
                    MessageBoxCF mb = new MessageBoxCF(mess, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    return;
                }
            }

            if (ReleaseVoucherList.Count != int.Parse(PerCus.Content.ToString()) * ListCustomerEmail.Count)
            {
                MessageBoxCF mb;
                int per = ReleaseVoucherList.Count / ListCustomerEmail.Count;
                mb = new MessageBoxCF($"Còn lại tối đa {per} voucher/khách hàng.\nBạn có chắc muốn gửi không?", MessageType.Error, MessageButtons.YesNo);
                mb.ShowDialog();
                if (mb.DialogResult == false)
                    return;
            }


            // Danh sách code và khách hàng
            List<string> listCode = ReleaseVoucherList.Select(v => v.CODEVOUCHER).ToList();

            //Chia danh sách code theo số lượng khách hàng
            int sizePerItem = listCode.Count / ListCustomerEmail.Count;
            List<List<string>> ListCodePerEmailList = ChunkBy(listCode, sizePerItem);

            (bool sendSuccess, string messageFromSendEmail) = await sendHtmlEmail(ListCustomerEmail.ToList(), ListCodePerEmailList);

            if (!sendSuccess)
            {
                MessageBoxCF mb = new MessageBoxCF(messageFromSendEmail, MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                return;
            } 

            WaitingMiniVoucher = new List<string>();
            foreach (var item in ReleaseVoucherList)
            {
                WaitingMiniVoucher.Add(item.CODEVOUCHER);
            }
            (bool releaseSuccess, string messageFromRelease) = await VoucherServices.Ins.ReleaseMultiVoucher(WaitingMiniVoucher);

            if (releaseSuccess)
            {
                MessageBoxCF mb = new MessageBoxCF(messageFromRelease, MessageType.Accept, MessageButtons.OK);
                mb.ShowDialog();
                p.Close();
            }
            else
            {
                MessageBoxCF mb = new MessageBoxCF(messageFromRelease, MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }
        }

        public async Task RefreshEmailList()
        {
            if (ReleaseCustomerList is null) return;
            switch (ReleaseCustomerList.Content.ToString())
            {
                case "Khách hàng kim cương":
                    {
                        List<CustomerDTO> list = await CustomerServices.Ins.GetRankCustomer(RANK.DIAMOND);
                        ListCustomerEmail = new ObservableCollection<CustomerEmail>();

                        foreach (var item in list)
                        {
                            if (item.EMAIL != null)
                                ListCustomerEmail.Add(new CustomerEmail { Email = item.EMAIL, Name = item.HOTEN, IsReadonly = true, IsEnable = false });
                        }
                        ReleaseVoucherList = new ObservableCollection<VoucherDTO>(GetRandomUnreleasedCode(ListCustomerEmail.Count * int.Parse(PerCus.Content.ToString())));
                        return;
                    }
                case "Khách hàng vàng":
                    {
                        List<CustomerDTO> list = await CustomerServices.Ins.GetRankCustomer(RANK.GOLD);
                        ListCustomerEmail = new ObservableCollection<CustomerEmail>();

                        foreach (var item in list)
                        {
                            if (item.EMAIL != null)
                                ListCustomerEmail.Add(new CustomerEmail { Email = item.EMAIL, Name = item.HOTEN, IsReadonly = true, IsEnable = false });
                        }
                        ReleaseVoucherList = new ObservableCollection<VoucherDTO>(GetRandomUnreleasedCode(ListCustomerEmail.Count * int.Parse(PerCus.Content.ToString())));
                        return;
                    }
                case "Khách hàng bạc":
                    {
                        List<CustomerDTO> list = await CustomerServices.Ins.GetRankCustomer(RANK.SILVER);
                        ListCustomerEmail = new ObservableCollection<CustomerEmail>();

                        foreach (var item in list)
                        {
                            if (item.EMAIL != null)
                                ListCustomerEmail.Add(new CustomerEmail { Email = item.EMAIL, Name = item.HOTEN, IsReadonly = true, IsEnable = false });
                        }
                        ReleaseVoucherList = new ObservableCollection<VoucherDTO>(GetRandomUnreleasedCode(ListCustomerEmail.Count * int.Parse(PerCus.Content.ToString())));
                        return;
                    }
                case "Khách hàng đồng":
                    {
                        List<CustomerDTO> list = await CustomerServices.Ins.GetRankCustomer(RANK.BRONZE);
                        ListCustomerEmail = new ObservableCollection<CustomerEmail>();

                        foreach (var item in list)
                        {
                            if (item.EMAIL != null)
                                ListCustomerEmail.Add(new CustomerEmail { Email = item.EMAIL, Name = item.HOTEN, IsReadonly = true, IsEnable = false });
                        }
                        ReleaseVoucherList = new ObservableCollection<VoucherDTO>(GetRandomUnreleasedCode(ListCustomerEmail.Count * int.Parse(PerCus.Content.ToString())));
                        return;
                    }

            }
        }

        public List<VoucherDTO> GetRandomUnreleasedCode(int quantity)
        {
            return ListVoucher.Where(v => v.STATUS == VOUCHER_STATUS.UNRELEASED).Take(quantity).ToList();
        }

        public List<List<string>> ChunkBy(List<string> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }

    public class CustomerEmail : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        private bool isReadonly;
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; OnPropertyChanged(); }

        }
        private bool isEnable;
        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; OnPropertyChanged(); }
        }

        public CustomerEmail() { }
    }
}
