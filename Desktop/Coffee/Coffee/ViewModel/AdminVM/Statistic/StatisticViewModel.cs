using ChartKit.Defaults;
using ChartKit.SkiaSharpView;
using ChartKit;
using Coffee.Views.Admin.StatisticPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Coffee.DTOs;
using Coffee.Views.Admin.EmployeePage;
using System.Windows;
using MaterialDesignThemes.Wpf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using ChartKit.SkiaSharpView.WPF;
using Coffee.Services;
using Coffee.Models;

namespace Coffee.ViewModel.AdminVM.Statistic
{
    public partial class StatisticViewModel : BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }
        private ObservableCollection<ProductDTO> _ListTopMenu;
        public ObservableCollection<ProductDTO> ListTopMenu
        {
            get { return _ListTopMenu; }
            set { _ListTopMenu = value; OnPropertyChanged(); }
        }

        private DateTime _FromDate;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set
            {
                if (value > ToDate)
                    _FromDate = ToDate;
                else
                    _FromDate = value;

                OnPropertyChanged();
            }
        }

        private DateTime _ToDate;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set
            {
                _ToDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Icommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand loadSaleHistoryIC { get; set; }
        public ICommand loadImportHistoryIC { get; set; }
        public ICommand loadStatisticIC { get; set; }
        public ICommand loadTopMenuIC { get; set; }
        public ICommand loadDataIC { get; set; }
        #endregion

        public StatisticViewModel()
        {
            ToDate = DateTime.Now;
            FromDate = ToDate;
            
            #region 
            
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });
            loadSaleHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SaleHistoryPage();
            });
            loadImportHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportHistoryPage();
            });

            loadStatisticIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new StatisticPage();
            });

            loadBillListTimeIC = new RelayCommand<object>(p => true, p => LoadBillList(FromDate,ToDate));
            loadBillImportListtimeIC = new RelayCommand<object>(p => true, p => loadBillImportList(FromDate, ToDate));
            loadDataIC = new RelayCommand<object>(p => true, p => loadData());

            loadTopMenuIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadTopMenu();
            });
            openWindowBillIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                openWindowBill();
            });
            openWindowBillImportIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                openWindowBillImport();
            });
            closeBillViewWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            closeBillImportViewWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            #endregion
        }
        #region operation
        //mở cửa sổ chi tiết hóa đơn
        public async void openWindowBill()
        {
            BillView b = new BillView();
            await loadBill(SelectedBill);
            b.ShowDialog();
        }

        //load dữ liệu chi tiết hóa đơn
        private async Task loadBill(BillDTO bill)
        {
            MaskName.Visibility = Visibility.Visible;
            NgayTao = bill.NgayTao;
            TongTien = Convert.ToInt32(bill.TongTien);
            await getNameEmployee(bill.MaNhanVien);
            await getListDetailBill(bill.MaHoaDon);
            MaskName.Visibility = Visibility.Collapsed;
        } 

        //mở cửa sổ chi tiết hóa đơn nhập kho
        public async void openWindowBillImport()
        {
            BillimportView b = new BillimportView();
            await loadBillImport(SelectedBillImport);
            b.ShowDialog();
        }

        //load dữ liệu chi tiết hóa đơn nhập kho
        private async Task loadBillImport(ImportDTO import)
        {
            MaskName.Visibility = Visibility.Visible;
            NgayTaoPhieu = import.NgayTaoPhieu;
            TongTienNhap = Convert.ToInt32(import.TongTien);
            await getNameEmployeeImport(import.MaNhanVien);
            await getListDetailBillImport(import.MaPhieuNhapKho);
            MaskName.Visibility = Visibility.Collapsed;
        }
        //load dữ liệu các sản phẩm bán chạy
        public async void LoadTopMenu()
        {
            (string label, List<ProductDTO> productlist) = await BillService.Ins.GetMostSoldFoods();

            if (productlist != null)
            {
                ListTopMenu = new ObservableCollection<ProductDTO>(productlist);
            }
        }
        #endregion

        public async void loadData()
        {
            await LoadBillList(FromDate, ToDate);
            await loadBillImportList(FromDate, ToDate);
            await loadCartesianChar();
        }
    }
}
