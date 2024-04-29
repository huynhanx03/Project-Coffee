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

namespace Coffee.ViewModel.AdminVM.Statistic
{
    public partial class StatisticViewModel : BaseViewModel
    {
        #region variable
        private ObservableCollection<ProductDTO> _ListTopMenu;
        public ObservableCollection<ProductDTO> ListTopMenu
        {
            get { return _ListTopMenu; }
            set { _ListTopMenu = value; OnPropertyChanged(); }
        }
        public Frame MainFrame;
        #endregion

        #region Icommand
        public ICommand loadMainFrame { get; set; }
        public ICommand loadSaleHistoryIC { get; set; }
        public ICommand loadImportHistoryIC { get; set; }
        public ICommand loadStatisticIC { get; set; }
        public ICommand loadTopMenuIC { get; set; }
        #endregion

        public StatisticViewModel()
        {
            loadMainFrame = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
            });
            loadSaleHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new SaleHistoryPage();
            });
            loadImportHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ImportHistoryPage();
            });
            loadStatisticIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new StatisticPage();
            });

            #region
            loadBillListIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                loadBillList();
            });
            loadWarehouseListIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                loadWareHouseList();
            });
            loadTopMenuIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
            });
            #endregion
        }
        #region operation

        #endregion
        
    }
}
