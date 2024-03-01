using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.Admin.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.CustomerPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.DashboardPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.EmployeePage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.IngredientsPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.ItemsPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.SettingPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.TablesPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.VoucherPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM
{
    public class MainAdminViewModel : BaseViewModel
    {
        public int _Role { get; set; }
        public int Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }
        public ICommand LoadMainDashboardPageCF { get; set; }
        public ICommand LoadMainTablesPageCF { get; set; }
        public ICommand LoadMainItemsPageCF { get; set; }
        public ICommand LoadMainIngredientsPageCF { get; set; }
        public ICommand LoadMainBillsPageCF { get; set; }
        public ICommand LoadMainEmployeePageCF { get; set; }
        public ICommand LoadMainCustomerPageCF { get; set; }
        public ICommand LoadMainSettingPageCF { get; set; }
        public ICommand LoadMainVoucherPageCF { get; set; }
        public ICommand LoadRole { get; set; }

        private string _optionName { get; set; }
        public string optionName
        {
            get { return _optionName; }
            set { _optionName = value; OnPropertyChanged(); }
        }

        public MainAdminViewModel()
        {
            LoadRole = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Role = AdminServices.Role;
                await VoucherServices.Ins.UpdateExpiredVoucher();
            });

            LoadMainDashboardPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainDashboardPage();
                optionName = "Trang chủ";
            });

            LoadMainTablesPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainTablesPage();
                optionName = "Bàn";
            });

            LoadMainItemsPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainItemsPage();
                optionName = "Thực đơn";
            });

            LoadMainIngredientsPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainIngredientsPage();
                optionName = "Nguyên liệu";
            });

            LoadMainBillsPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainBillsPage();
                optionName = "Hoá đơn";
            });

            LoadMainEmployeePageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainEmployeePage();
                optionName = "Nhân viên";
            });

            LoadMainCustomerPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainCustomerPage();
                optionName = "Khách hàng";
            });

            LoadMainSettingPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingPage();
                optionName = "Cài đặt";
            });

            LoadMainVoucherPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainVoucherPage();
                optionName = "Voucher";
            });
        }
    }
}
