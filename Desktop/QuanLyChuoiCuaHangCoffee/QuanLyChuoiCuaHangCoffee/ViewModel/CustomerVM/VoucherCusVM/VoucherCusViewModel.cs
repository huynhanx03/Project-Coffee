using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM.VoucherCusVM
{
    public class VoucherCusViewModel : BaseViewModel
    {
        private ObservableCollection<VoucherDTO> _ListVoucher { get; set; }
        public ObservableCollection<VoucherDTO> ListVoucher
        {
            get { return _ListVoucher; }
            set { _ListVoucher = value; OnPropertyChanged(); }
        }

        public ICommand LoadListVoucher { get; set; }

        public VoucherCusViewModel()
        {
            LoadListVoucher = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await loadListVoucher();
            });
        }

        private async Task loadListVoucher()
        {
            List<VoucherDTO> _listVoucher = await VoucherServices.Ins.GetListVoucherCus();
            _listVoucher.Reverse();
            ListVoucher = new ObservableCollection<VoucherDTO>(_listVoucher);
        }
    }
}
