using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Customer.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.Customer.DashboardPage;
using QuanLyChuoiCuaHangCoffee.Views.Customer.SettingPage;
using QuanLyChuoiCuaHangCoffee.Views.Customer.VoucherPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM
{
    public class MainCustomerViewModel : BaseViewModel
    {
        private string _optionName { get; set; }
        public string optionName
        {
            get { return _optionName; }
            set { _optionName = value; OnPropertyChanged(); }
        }
        public ICommand LoadMainDashboardPageCF { get; set; }
        public ICommand LoadMainBillsPageCF { get; set; }
        public ICommand LoadMainVoucherPageCF { get; set; }
        public ICommand LoadMainSettingPageCF { get; set; }

        public MainCustomerViewModel()
        {
            LoadMainDashboardPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainDashboardCusPage();
                optionName = "Trang chủ";
            });

            LoadMainBillsPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainBillsCusPage();
                optionName = "Hóa đơn";
            });

            LoadMainVoucherPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainVoucherCusPage();
                optionName = "Mã giảm giá";
            });

            LoadMainSettingPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingCusPage();
                optionName = "Cài đặt";
            });
        }
    }
}
