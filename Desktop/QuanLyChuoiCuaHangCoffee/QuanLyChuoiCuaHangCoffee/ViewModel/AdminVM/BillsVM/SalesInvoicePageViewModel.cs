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
        private ObservableCollection<OrderBillsDTO> _ListSaleInvoice { get; set; }
        public ObservableCollection<OrderBillsDTO> ListSaleInvoice
        {
            get => _ListSaleInvoice;
            set
            {
                _ListSaleInvoice = value;
                OnPropertyChanged();
            }
        }

        private OrderBillsDTO _SelectedSaleInvoiceItem { get; set; }
        public OrderBillsDTO SelectedSaleInvoiceItem
        {
            get => _SelectedSaleInvoiceItem;
            set
            {
                _SelectedSaleInvoiceItem = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedDateStart { get; set; }
        public DateTime SelectedDateStart
        {
            get => _SelectedDateStart;
            set
            {
                _SelectedDateStart = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedDateEnd { get; set; }
        public DateTime SelectedDateEnd
        {
            get => _SelectedDateEnd;
            set
            {
                _SelectedDateEnd = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExportFileSaleInvoiceCF { get; set; }
        public ICommand SelectedDateStartChanged { get; set; }
        public ICommand SelectedDateEndChanged { get; set; }
        public ICommand LoadInforSaleInvoice { get; set; }
    }
}
