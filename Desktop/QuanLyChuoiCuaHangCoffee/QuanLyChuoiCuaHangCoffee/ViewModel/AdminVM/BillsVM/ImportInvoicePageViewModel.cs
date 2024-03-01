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
        private string _EmployeeImoprt { get; set; }
        public string EmployeeImport
        {
            get { return _EmployeeImoprt; }
            set { _EmployeeImoprt = value; OnPropertyChanged(); }
        }

        private string _DateBillImport { get; set; }
        public string DateBillImport
        {
            get { return _DateBillImport; }
            set { _DateBillImport = value; OnPropertyChanged(); }
        }
        private DateTime _SelectedDateStartImport { get; set; }
        public DateTime SelectedDateStartImport
        {
            get => _SelectedDateStartImport;
            set
            {
                _SelectedDateStartImport = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedDateEndImport { get; set; }
        public DateTime SelectedDateEndImport
        {
            get => _SelectedDateEndImport;
            set
            {
                _SelectedDateEndImport = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImportBillDTO> _ListImportInvoice { get; set; }
        public ObservableCollection<ImportBillDTO> ListImportInvoice
        {
            get => _ListImportInvoice;
            set
            {
                _ListImportInvoice = value;
                OnPropertyChanged();
            }
        }

        private ImportBillDTO _SelectedImportInvoiceItem { get; set; }
        public ImportBillDTO SelectedImportInvoiceItem
        {
            get => _SelectedImportInvoiceItem;
            set
            {
                _SelectedImportInvoiceItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<IngredientsDTO> _ListImportDetail { get; set; }
        public ObservableCollection<IngredientsDTO> ListImportDetail
        {
            get { return _ListImportDetail; }
            set { _ListImportDetail = value; OnPropertyChanged(); }
        }

        public ICommand SelectedDateStartImportChanged { get; set; }
        public ICommand SelectedDateEndImportChanged { get; set; }
        public ICommand ExportFileImoprtInvoiceCF { get; set; }
        public ICommand LoadInfoImportInvoice { get; set; }
    }
}
