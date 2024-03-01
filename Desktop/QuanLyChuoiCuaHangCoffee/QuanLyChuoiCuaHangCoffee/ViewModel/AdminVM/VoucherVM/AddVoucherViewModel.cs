using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM
{
    public partial class VoucherViewModel : BaseViewModel
    {
        #region variable
        private int _Quantity { get; set; }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }

        private int _Percentage { get; set; }   
        public int Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; OnPropertyChanged(); }
        }

        private int _Length { get; set; }
        public int Length
        {
            get { return _Length; }
            set { _Length = value; OnPropertyChanged(); }
        }

        private string _FirstChar { get; set; }
        public string FirstChar
        {
            get { return _FirstChar; }
            set { _FirstChar = value; OnPropertyChanged(); }
        }

        private string _LastChar { get; set; }
        public string LastChar
        {
            get { return _LastChar; }
            set { _LastChar = value; OnPropertyChanged(); }
        }

        private string _ReleaseName { get; set; }
        public string ReleaseName
        {
            get { return _ReleaseName; }
            set { _ReleaseName = value; OnPropertyChanged(); }
        }

        private DateTime _ExpiredDate { get; set; }
        public DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; OnPropertyChanged(); }
        }
        #endregion

        #region command
        public ICommand SaveListVoucherCF { get; set; }
        #endregion

        public async Task SaveListVoucher()
        {
            if (Quantity == 0 || Length == 0 || string.IsNullOrEmpty(FirstChar) || string.IsNullOrEmpty(LastChar))
            {
                MessageBoxCF mb = new MessageBoxCF("Không được để trống!", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                return;
            }

            List<string> ListCode = getListCode(Quantity, Length, FirstChar, LastChar);

            if (ListCode == null)
            {
                MessageBoxCF ms = new MessageBoxCF("Độ dài code không hợp lệ", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            } else
            {
                await VoucherServices.Ins.CreateVoucher(ListCode, Percentage, ExpiredDate, ReleaseName);
                MessageBoxCF ms = new MessageBoxCF("Tạo voucher thành công", MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();

            }
        }

        public List<string> getListCode(int quantity, int length, string firstChars, string lastChars)
        {
            List<string> ListCode = new List<string>();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            int randomLength = length - firstChars.Length - lastChars.Length;
            if (randomLength <= 0)
            {
                return null;
            }
            if (randomLength < 4)
            {
                return null;
            }
            for (int i = 0; i < quantity; i++)
            {

                var stringChars = new char[randomLength];
                for (int j = 0; j < stringChars.Length; j++)
                {
                    stringChars[j] = chars[random.Next(chars.Length)];
                }
                string newCode = new String(stringChars);
                var isExist = ListCode.Any(code => code == newCode);
                if (isExist)
                {
                    i--;
                    continue;
                }
                ListCode.Add(firstChars + newCode + lastChars);
            }

            return (ListCode);
        }
    }
}
