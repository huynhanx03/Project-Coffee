using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.VoucherPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM
{
    public partial class VoucherViewModel : BaseViewModel
    {
        #region variable
        private bool _IsReleaseVoucherLoading { get; set; }
        public bool IsReleaseVoucherLoading
        {
            get { return _IsReleaseVoucherLoading; }
            set { _IsReleaseVoucherLoading = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _SelectVoucherFilter { get; set; }
        public ComboBoxItem SelectVoucherFilter
        {
            get { return _SelectVoucherFilter; }
            set { _SelectVoucherFilter = value; OnPropertyChanged(); }
        }
        private VoucherDTO _SelectedVoucherItem { get; set; }
        public VoucherDTO SelectedVoucherItem
        {
            get { return _SelectedVoucherItem; }
            set { _SelectedVoucherItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<VoucherDTO> _ListVoucher { get; set; }
        public ObservableCollection<VoucherDTO> ListVoucher
        {
            get { return _ListVoucher; }
            set { _ListVoucher = value; OnPropertyChanged(); }
        }
        #endregion

        #region command
        public ICommand LoadAddVoucher { get; set; }
        public ICommand LoadListVoucher { get; set; }
        public ICommand LoadReleaseVoucher { get; set; }
        public ICommand CheckSelectVoucherFilterCF { get; set; }
        #endregion

        public VoucherViewModel()
        {
            ExpiredDate = DateTime.Now;

            LoadListVoucher = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await loadListVoucher();
            });

            LoadAddVoucher = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddVoucherWindow wd = new AddVoucherWindow();
                wd.ShowDialog();
            });

            SaveListVoucherCF = new RelayCommand<Button>((p) => { return true; }, async (p) =>
            {
                string oldstring = p.Content.ToString();

                p.Content = "";
                p.IsHitTestVisible = false;
                await SaveListVoucher();
                await loadListVoucher();
                p.Content = oldstring;
                p.IsHitTestVisible = true;
            });

            LoadReleaseVoucher = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ReleaseVoucherWindow w = new ReleaseVoucherWindow();
                w.ShowDialog();
            });

            RefreshEmailListCF = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await RefreshEmailList();
            });

            CalculateNumberOfVoucherCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (p is null) return;
                ComboBoxItem selectedNum = (ComboBoxItem)p.SelectedItem;
                ReleaseVoucherList = new ObservableCollection<VoucherDTO>(GetRandomUnreleasedCode(ListCustomerEmail.Count * int.Parse(selectedNum.Content.ToString())));
            });

            ReleaseVoucherCF = new RelayCommand<ReleaseVoucherWindow> ((p) => { return true; }, async (p) =>
            {
                if (p is null) return;
                IsReleaseVoucherLoading = true;
                await ReleaseVoucherFunc(p);
                await loadListVoucher();
                IsReleaseVoucherLoading = false;
            });

            CheckSelectVoucherFilterCF = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await CheckCombobox();
            });
        }

        private async Task loadListVoucher()
        {
            List<VoucherDTO> listvoucher = await VoucherServices.Ins.GetListVoucher();
            listvoucher.Reverse();
            ListVoucher = new ObservableCollection<VoucherDTO>(listvoucher);
        }

        private async Task loadListVoucherCondition(string s)
        {
            List<VoucherDTO> listvoucher = await VoucherServices.Ins.GetListVoucherCondition(s);
            listvoucher.Reverse();
            ListVoucher = new ObservableCollection<VoucherDTO>(listvoucher);
        }

        private async Task CheckCombobox()
        {
            switch(SelectVoucherFilter.Content.ToString())
            {
                case "Đã phát hành":
                    {
                        await loadListVoucherCondition(VOUCHER_STATUS.RELEASED);
                        break;
                    }
                case "Chưa phát hành":
                    {
                        await loadListVoucherCondition(VOUCHER_STATUS.UNRELEASED);
                        break;
                    }
                case "Hết hạn":
                    {
                        await loadListVoucherCondition(VOUCHER_STATUS.EXPIRED);
                        break;
                    }
                default:
                    {
                        await loadListVoucher();
                        break;
                    }
            }
        }
    }
}
