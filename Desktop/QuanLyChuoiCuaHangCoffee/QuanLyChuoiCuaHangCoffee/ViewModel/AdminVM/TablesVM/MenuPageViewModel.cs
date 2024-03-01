using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.TablesVM
{
    public partial class TablesViewModel : BaseViewModel
    {
        private ComboBoxItem _SelectedStatusMenu { get; set; }
        public ComboBoxItem SelectedStatusMenu
        {
            get { return _SelectedStatusMenu; }
            set { _SelectedStatusMenu = value; OnPropertyChanged(); }
        }

        private ProductsDTO _SelectedMenuItem { get; set; }
        public ProductsDTO SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set { _SelectedMenuItem = value; OnPropertyChanged(); }
        }

        private decimal _TOTALPERPRODUCT { get; set; }
        public decimal TOTALPERPRODUCT
        {
            get { return _TOTALPERPRODUCT; }
            set { _TOTALPERPRODUCT = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProductsDTO> _ListMenu { get; set; }
        public ObservableCollection<ProductsDTO> ListMenu
        {
            get { return _ListMenu; }
            set { _ListMenu = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuItemDTO> _ListProduct { get; set; }
        public ObservableCollection<MenuItemDTO> ListProduct
        {
            get { return _ListProduct; }
            set { _ListProduct = value; OnPropertyChanged(); }
        }

        public ICommand CheckSelectedStatusMenuCF { get; set; }
        public ICommand AddToMenuTableList { get; set; }
        public ICommand LoadMenuProducts { get; set; }
        public ICommand IncreaseProduct { get; set; }
        public ICommand DecreaseProduct { get; set; }


        private void addToListMenuItem()
        {
            if (SelectedMenuItem.SOLUONG <= 0)
            {
                MessageBoxCF mb = new MessageBoxCF("Món bạn chọn đã hết rồi!", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                return;
            }

            var existProduct = ListProduct.FirstOrDefault(x => x.TENMON == SelectedMenuItem.TENMON);
            if (existProduct != null)
            {
                // Tạo một danh sách tạm thời để lưu trữ các phần tử theo thứ tự ban đầu
                List<MenuItemDTO> temporaryList = new List<MenuItemDTO>(ListProduct);

                existProduct.SOLUONG = (int.Parse(existProduct.SOLUONG) + 1).ToString();
                ListProduct.Remove(existProduct);
                existProduct.THANHTIEN = existProduct.DONGIA * int.Parse(existProduct.SOLUONG);
                ListProduct.Add(existProduct);

                // Cập nhật lại ListProduct từ danh sách tạm thời
                ListProduct = new ObservableCollection<MenuItemDTO>(temporaryList);
            }
            else
            {
                MenuItemDTO product = new MenuItemDTO();
                product.MAMON = SelectedMenuItem.MAMON;
                product.TENMON = SelectedMenuItem.TENMON;
                product.SIZE = SelectedMenuItem.SIZESANPHAM;
                product.SOLUONG = "1";
                product.DONGIA = SelectedMenuItem.GIASANPHAM;
                product.THANHTIEN = product.DONGIA * int.Parse(product.SOLUONG);
                ListProduct.Add(product);

                
            }
            var existTable = MenuOfTable.FirstOrDefault(x => x.MABAN == int.Parse(tableNumber));
            existTable.Products = ListProduct;
        }

        private async Task GetListMenuSource(string s = "")
        {
            ListMenu = new ObservableCollection<ProductsDTO>();
            switch (s)
            {
                case "":
                    {
                        try
                        {
                            ListMenu = new ObservableCollection<ProductsDTO>(await ProductServices.Ins.GetAllProducts());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "Còn hàng":
                    {
                        try
                        {
                            ListMenu = new ObservableCollection<ProductsDTO>(await ProductServices.Ins.GetInStockProducts());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "Hết hàng":
                    {
                        try
                        {
                            ListMenu = new ObservableCollection<ProductsDTO>(await ProductServices.Ins.GetOutStockProducts());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
            }
        }

        private async void CheckComboboxMenu()
        {
            switch (SelectedStatusMenu.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetListMenuSource("");
                        return;
                    }
                case "Còn hàng":
                    {
                        await GetListMenuSource("Còn hàng");
                        return;
                    }
                case "Hết hàng":
                    {
                        await GetListMenuSource("Hết hàng");
                        return;
                    }
            }
        }
    }
}
