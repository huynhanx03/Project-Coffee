using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Views.Admin.OrderPage;
using Coffee.Views.MessageBox;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Order
{
    public partial class OrderViewModel: BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }

        private ObservableCollection<OrderDTO> _OrderList;

        public ObservableCollection<OrderDTO> OrderList
        {
            get { return _OrderList; }
            set { _OrderList = value; OnPropertyChanged(); }
        }

        private List<OrderDTO> __OrderList;

        private ObservableCollection<ProductOrderDTO> _ProductOrderList;

        public ObservableCollection<ProductOrderDTO> ProductOrderList
        {
            get { return _ProductOrderList; }
            set { _ProductOrderList = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand loadOrderListIC { get; set; }
        public ICommand searchOrderIC { get; set; }
        public ICommand cancelOrderIC { get; set; }
        public ICommand confirmOrderIC { get; set; }
        public ICommand viewDetailOrderIC { get; set; }
        public ICommand closeViewDetailOrderWindowIC { get; set; }
        #endregion

        public OrderViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            loadOrderListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadOrderList();
            });

            cancelOrderIC = new RelayCommand<OrderDTO>((p) => { return true; }, (p) =>
            {
                cancelOrder(p);
            });

            confirmOrderIC = new RelayCommand<OrderDTO>((p) => { return true; }, (p) =>
            {
                confirmOrder(p);
            });

            viewDetailOrderIC = new RelayCommand<OrderDTO>((p) => { return true; }, (p) =>
            {
                viewDetailOrder(p);
            });

            closeViewDetailOrderWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            searchOrderIC = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (__OrderList != null)
                        OrderList = new ObservableCollection<OrderDTO>(__OrderList.FindAll(x => x.TenKhachHang.ToLower().Contains(p.Text.ToLower())));
                }
            });
        }

        #region function
        /// <summary>
        /// Load danh sách đơn hàng
        /// </summary>
        private async void loadOrderList()
        {
            (string label, List<OrderDTO> Orders) = await OrderService.Ins.getListOrder();

            if (Orders != null)
            {
                OrderList = new ObservableCollection<OrderDTO>(Orders);
                __OrderList = new List<OrderDTO>(Orders);
            }
            else
                OrderList = new ObservableCollection<OrderDTO>();
        }

        /// <summary>
        /// Huỷ đơn hàng
        /// </summary>
        public async void cancelOrder(OrderDTO Order)
        {
            MaskName.Visibility = Visibility.Visible;
            MessageBoxCF ms = new MessageBoxCF("Xác nhận huỷ đơn hàng này?", MessageType.Waitting, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isCancelOrder) = await OrderService.Ins.updateStatusOrder(Order.MaDonHang, Constants.StatusOrder.CANCEL);

                if (isCancelOrder)
                {
                    MessageBoxCF msn = new MessageBoxCF("Huỷ đơn hàng thành công", MessageType.Accept, MessageButtons.OK);
                    loadOrderList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
            MaskName.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Xác nhận đơn hàng
        /// </summary>
        public async void confirmOrder(OrderDTO Order)
        {
            MaskName.Visibility = System.Windows.Visibility.Visible;
            MessageBoxCF ms = new MessageBoxCF("Xác nhận giao đơn hàng này?", MessageType.Waitting, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isCancelOrder) = await OrderService.Ins.updateStatusOrder(Order.MaDonHang, Constants.StatusOrder.CONFIRMED);

                if (isCancelOrder)
                {
                    MessageBoxCF msn = new MessageBoxCF("Xác nhận đơn hàng thành công", MessageType.Accept, MessageButtons.OK);
                    loadOrderList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
            MaskName.Visibility = System.Windows.Visibility.Collapsed;
        }

        public async void viewDetailOrder(OrderDTO Order)
        {
            ProductOrderList = new ObservableCollection<ProductOrderDTO>(Order.SanPham.Values);

            MaskName.Visibility = Visibility.Visible;

            DetailOrderWindow w = new DetailOrderWindow();
            w.ShowDialog();

            MaskName.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
