using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.BillsVM
{
    public partial class BillsViewModel : BaseViewModel
    {
        private string _tableNumber { get; set; }
        public string tableNumber
        {
            get => _tableNumber;
            set
            {
                _tableNumber = value;
                OnPropertyChanged();
            }
        }

        private string _DateBill { get; set; }
        public string DateBill
        {
            get => _DateBill;
            set
            {
                _DateBill = value;
                OnPropertyChanged();
            }
        }

        private int _VoucherPercentage { get; set; }
        public int VoucherPercentage
        {
            get => _VoucherPercentage;
            set
            {
                _VoucherPercentage = value;
                OnPropertyChanged();
            }
        }

        private string _TotalFinal { get; set; }
        public string TotalFinal
        {
            get => _TotalFinal;
            set
            {
                _TotalFinal = value;
                OnPropertyChanged();
            }
        }

        private string _Employee { get; set; }
        public string Employee
        {
            get => _Employee;
            set
            {
                _Employee = value;
                OnPropertyChanged();
            }
        }

        private string _HourBillIn { get; set; }    
        public string HourBillIn
        {
            get => _HourBillIn;
            set
            {
                _HourBillIn = value;
                OnPropertyChanged();
            }
        }

        private string _MADH { get; set; }
        public string MADH
        {
            get => _MADH;
            set
            {
                _MADH = value;
                OnPropertyChanged();
            }
        }

        private string _CusName { get; set; }
        public string CusName
        {
            get => _CusName;
            set
            {
                _CusName = value;
                OnPropertyChanged();
            }
        }

        private string _HourBillOut { get; set; }
        public string HourBillOut
        {
            get => _HourBillOut;
            set
            {
                _HourBillOut = value;
                OnPropertyChanged();
            }
        }

        private string _Note { get; set; }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged(); }
        }

        private string _Total { get; set; }
        public string Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuItemDTO> _ListProduct { get; set; }
        public ObservableCollection<MenuItemDTO> ListProduct
        {
            get { return _ListProduct; }
            set { _ListProduct = value; OnPropertyChanged(); }
        }
        public ICommand closeCF { get; set; }

    }
}
