using Coffee.DTOs;
using Coffee.Utils;
using Coffee.Views.Admin.MenuPage;
using Coffee.Views.Admin.TablePage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Table
{
    public partial class MainTableViewModel: BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }
        public Frame LeftFrame { get; set; }
        public RadioButton btnMenu { get; set; }

        private int _role;

        public int role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(); }
        }


        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand loadTablePageIC { get; set; }
        public ICommand loadSellPageIC { get; set; }
        public ICommand openWindowAddTableIC { get; set; }
        public ICommand loadMenuIC { get; set; }
        public ICommand loadLeftFrameIC { get; set; }
        public ICommand loadBtnMenuIC { get; set; }

        #endregion

        public MainTableViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            loadLeftFrameIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                LeftFrame = p;
            });

            loadBtnMenuIC = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
            {
                btnMenu = p;
            });

            loadTablePageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                LeftFrame.Content = new TablePage();
            });

            loadSellPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new SalesPage();
            });

            loadMenuIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                LeftFrame.Content = new MenuPage();
            });

            clickTableIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                clickTable();
            });

            openWindowAddTableIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                openWindowAddTable();
            });

            #region Table
            loadTableListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadTableList();
            });

            openWindowChangeTableIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                openWindowChangeTable();
            });

            confirmChangeTableIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                confirmChangeTable(p);
            });

            closeChangeTableWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            openWindowMergeTableIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                openWindowMergeTable();
            });

            confirmMergeTableIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                confirmMergeTable(p);
            });

            closeMergeTableWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            #endregion

            #region operation table
            closeOperationTableWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            confirmOperationTableIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                confirmOperationTable(p);
            });

            openEditTableIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            openDeleteTableIC = new RelayCommand<TableDTO>((p) => { return true; }, async (p) =>
            {
                deleteTable(p);
            });

            #endregion

            #region menu
            loadMenuListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadMenuList();
            });

            addProductToBillIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                addProductToBill();
            });

            selectedTypeProductIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                selectedTypeProduct();
            });

            loadProductTypeListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadProductTypeList();
            });

            searchProductIC = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (__ProductList != null)
                        __ProductSearchList = new List<ProductDTO>(__ProductList.FindAll(x => x.TenSanPham.ToLower().Contains(p.Text.ToLower())));

                    selectedTypeProduct();
                }
            });
            #endregion

            #region sales
            changeSizeProductIC = new RelayCommand<DetailBillDTO>((p) => { return true; }, (p) =>
            {
                changeSizeProduct(p);
            });

            subQuantityBillIC = new RelayCommand<DetailBillDTO>((p) => { return true; }, (p) =>
            {
                subQuantityBill();
            });

            plusQuantityBillIC = new RelayCommand<DetailBillDTO>((p) => { return true; }, (p) =>
            {
                plusQuantityBill();
            });

            removeBillIC = new RelayCommand<DetailBillDTO>((p) => { return true; }, (p) =>
            {
                removeBill();
            });

            loadDateSalesIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadDateSales();
            });

            loadCustomerIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadCustomer();
            });

            bookingIC = new RelayCommand<object>((p) => 
            { 
                return (currentTable != null 
                        && currentTable.TrangThai != Constants.StatusTable.BOOKED
                        && DetailBillList.Count > 0); 
            }, 
            (p) =>
            {
                booking();
            });

            payIC = new RelayCommand<object>((p) =>
            { 
                return DetailBillList.Count > 0; 
            }, 
            (p) =>
            {
                pay();
            });
            #endregion

            #region recommend product
            closeRecommendProductWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            #endregion
        }

        /// <summary>
        /// Mở cửa sổ thêm bàn
        /// </summary>
        public void openWindowAddTable()
        {
            MaskName.Visibility = Visibility.Visible;
            loadTableType();
            resetTable();
            OperationOfTableWindow w = new OperationOfTableWindow();
            TypeOperation = 1; // Add table
            w.ShowDialog();
            MaskName.Visibility = Visibility.Collapsed;
        }
    }
}
