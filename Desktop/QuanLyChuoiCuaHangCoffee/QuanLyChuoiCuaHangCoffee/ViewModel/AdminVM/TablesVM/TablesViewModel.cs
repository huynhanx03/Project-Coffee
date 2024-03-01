using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.TablesPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.TablesVM
{
    public partial class TablesViewModel : BaseViewModel
    {
        #region variable
        public Frame MainFrame { get; set; }
        private string _tableNumber { get; set; }
        public string tableNumber
        {
            get { return _tableNumber; }
            set { _tableNumber = value; OnPropertyChanged(); }
        }

        private string _Employee { get; set; }
        public string Employee
        {
            get { return _Employee; }
            set { _Employee = value; OnPropertyChanged(); }
        }

        private string _CusPhone { get; set; }
        public string CusPhone
        {
            get { return _CusPhone; }
            set { _CusPhone = value; OnPropertyChanged(); }
        }

        private string _CusName { get; set; }
        public string CusName
        {
            get { return _CusName; }
            set { _CusName = value; OnPropertyChanged(); }
        }

        private string _CusID { get; set; } 
        public string CusID
        {
            get { return _CusID; }
            set { _CusID = value; OnPropertyChanged(); }
        }

        private int _CusPoint { get; set; }
        public int CusPoint
        {
            get { return _CusPoint; }
            set { _CusPoint = value; OnPropertyChanged(); }
        }

        private string _DateBill { get; set; }
        public string DateBill
        {
            get { return _DateBill; }
            set { _DateBill = value; OnPropertyChanged(); }
        }

        private string _HourBillIn { get; set; }
        public string HourBillIn
        {
            get { return _HourBillIn; }
            set { _HourBillIn = value; OnPropertyChanged(); }
        }

        private string _HourBillOut { get; set; }
        public string HourBillOut
        {
            get { return _HourBillOut; }
            set { _HourBillOut = value; OnPropertyChanged(); }
        }

        private string _MADH { get; set; }
        public string MADH
        {
            get { return _MADH; }
            set { _MADH = value; OnPropertyChanged(); }
        }

        private decimal _TotalDec { get; set; }
        public decimal TotalDec
        {
            get { return _TotalDec; }
            set { _TotalDec = value; OnPropertyChanged(); }
        }

        private string _VoucherCode { get; set; }
        public string VoucherCode
        {
            get { return _VoucherCode; }
            set { _VoucherCode = value; OnPropertyChanged(); }
        }

        private int _VoucherPercentage { get; set; }
        public int VoucherPercentage
        {
            get { return _VoucherPercentage; }
            set { _VoucherPercentage = value; OnPropertyChanged(); }
        }

        private decimal _TotalFinalDec { get; set; }
        public decimal TotalFinalDec
        {
            get { return _TotalFinalDec; }
            set { _TotalFinalDec = value; OnPropertyChanged(); }
        }

        private string _TotalFinal { get; set; }
        public string TotalFinal
        {
            get { return _TotalFinal; }
            set { _TotalFinal = value; OnPropertyChanged(); }
        }

        private string _Total { get; set; }
        public string Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged(); }
        }
        private ComboBoxItem _SelectedStatus { get; set; }
        public ComboBoxItem SelectedStatus
        {
            get { return _SelectedStatus; }
            set { _SelectedStatus = value; OnPropertyChanged(); }
        }

        private string _SelectedTableNum { get; set; }
        public string SelectedTableNum
        {
            get { return _SelectedTableNum; }
            set { _SelectedTableNum = value; OnPropertyChanged(); }
        }

        private string _NewNumTable { get; set; }
        public string NewNumTable
        {
            get { return _NewNumTable; }
            set { _NewNumTable = value; OnPropertyChanged(); }
        }

        private string _CodeVoucher { get; set; }
        public string CodeVoucher
        {
            get { return _CodeVoucher; }
            set { _CodeVoucher = value; OnPropertyChanged(); }
        }

        private string _Note { get; set; }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged(); }
        }

        public static Grid MaskName { get; set; }

        private TablesDTO _SelectedTableItem { get; set; }
        public TablesDTO SelectedTableItem
        {
            get { return _SelectedTableItem; }
            set { _SelectedTableItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TablesDTO> _ListTable { get; set; }
        public ObservableCollection<TablesDTO> ListTable
        {
            get { return _ListTable; }
            set { _ListTable = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListTableNum { get; set; }
        public ObservableCollection<string> ListTableNum
        {
            get { return _ListTableNum; }
            set { _ListTableNum = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuOfTableDTO> _ListTableMenu { get; set; }
        public ObservableCollection<MenuOfTableDTO> ListTableMenu
        {
            get { return _ListTableMenu; }
            set { _ListTableMenu = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuOfTableDTO> _MenuOfTable { get; set; }
        public ObservableCollection<MenuOfTableDTO> MenuOfTable
        {
            get { return _MenuOfTable; }
            set { _MenuOfTable = value; OnPropertyChanged(); }
        }

        #endregion

        #region icommand
        public ICommand LoadTablesPage { get; set; }
        public ICommand LoadTable { get; set; }
        public ICommand CheckSelectedStatusCF { get; set; }
        public ICommand ManageTable { get; set; }
        public ICommand LoadMenu { get; set; }
        public ICommand BackTable { get; set; }
        public ICommand LoadTableNum { get; set; }
        public ICommand AddTable { get; set; }
        public ICommand DeleteTable { get; set; }
        public ICommand CheckOut { get; set; }
        public ICommand closeCF { get; set; }
        public ICommand MaskNameCF { get; set; }
        public ICommand LoadUser { get; set; }
        public ICommand LoadNote { get; set; }
        public ICommand LoadCodeVoucher { get; set; }

        #endregion

        public TablesViewModel()
        {
            MaskNameCF = new RelayCommand<Grid>((p) => { return true; }, (p) => { MaskName = p; });
            ListProduct = new ObservableCollection<MenuItemDTO>();
            MenuOfTable = new ObservableCollection<MenuOfTableDTO>();
            CusName = "Khách vãng lai";

            LoadTablesPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new TablesPage();
                MainFrame = p;
            });

            LoadTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                loadListTables();
            });

            CheckSelectedStatusCF = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                CheckCombobox();
            });

            LoadMenu = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new MenuPage();
                tableNumber = SelectedTableItem.MABAN.ToString();
                Employee = AdminServices.TenNhanVien;
                //Employee = "Ngo Nam";
                DateBill = DateTime.Now.Date.ToString("dd/MM/yyyy");

                var existTable = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                if (existTable != null)
                {
                    ListProduct = new ObservableCollection<MenuItemDTO>(existTable.Products);
                    HourBillIn = existTable.TIMEIn;
                    Note = existTable.GHICHU;
                } 
                else
                {
                    ListProduct = new ObservableCollection<MenuItemDTO>();
                    MenuOfTableDTO mot = new MenuOfTableDTO();
                    mot.MABAN = SelectedTableItem.MABAN;
                    mot.TIMEIn = DateTime.Now.ToString("HH:mm:ss");
                    mot.GHICHU = "";
                    mot.Products = new ObservableCollection<MenuItemDTO>();

                    MenuOfTable.Add(mot);
                    HourBillIn = DateTime.Now.ToString("HH:mm:ss");
                }

                CalTotalBill();
            });

            LoadUser = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                CustomerDTO cus = await CustomerServices.Ins.FindCus(CusPhone);
                if (cus != null)
                {
                    CusID = cus.IDKHACHHANG;
                    CusName = cus.HOTEN;
                    CusPoint = cus.TICHDIEM;
                }
                else
                {
                    CusID = "-1";
                    CusName = "Khách vãng lai";
                    CusPoint = 0;
                }
            });

            //LoadNote = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    var currentTable = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
            //    if (currentTable != null)
            //    {
            //        currentTable.GHICHU = Note;
            //    }
            //});

            BackTable = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                var table = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                if (table != null)
                {
                    if (table.Products.Count == 0)
                    {
                        await TablesServices.Ins.SetStatusAvailableTable((SelectedTableItem.MABAN));
                        table.GHICHU = "";
                    }
                    else
                    {
                        table.GHICHU = Note;
                    }
                }
                CodeVoucher = "";
                VoucherPercentage = 0;
                MainFrame.Content = new TablesPage();
                ResetProperty();
            });

            ManageTable = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddTableWindow wd = new AddTableWindow();
                wd.ShowDialog();
                loadListTables();

            });

            LoadTableNum = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetAllTables());
                ListTableNum = new ObservableCollection<string>();
                foreach (var item in ListTable)
                {
                    ListTableNum.Add(item.MABAN.ToString());
                }

            });

            AddTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                (string s, bool f) = await TablesServices.Ins.AddTable(int.Parse(NewNumTable));
                MessageBoxCF ms = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                NewNumTable = null;
                loadListTables();
            });

            DeleteTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                MessageBoxCF mb = new MessageBoxCF("Bạn muốn xoá bàn này?", MessageType.Waitting, MessageButtons.YesNo);
                if (mb.ShowDialog() == true)
                {
                    (string s, bool f) = await TablesServices.Ins.DeleteTable(int.Parse(SelectedTableNum));
                    MessageBoxCF ms = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    loadListTables();
                }
            });

            LoadCodeVoucher = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                (VoucherDTO voucher, string s) = await VoucherServices.Ins.FindVoucher(CodeVoucher);

                if (voucher == null)
                {
                    MessageBoxCF mb = new MessageBoxCF(s, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                    TotalFinalDec = TotalDec;
                    TotalFinal = Helper.FormatVNMoney(TotalFinalDec);
                    VoucherPercentage = 0;
                    return;
                }

                VoucherCode = voucher.CODEVOUCHER;
                VoucherPercentage = voucher.DISCOUNT;

                
                TotalFinalDec = TotalDec - TotalDec * voucher.DISCOUNT / 100;
                TotalFinal = Helper.FormatVNMoney(TotalFinalDec);
            });

            CheckOut = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                var table = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                if (table != null)
                {
                    if (table.Products.Count == 0)
                    {
                        MessageBoxCF ms = new MessageBoxCF("Bạn chưa chọn món!", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                        return;
                    }
                    HourBillOut = DateTime.Now.ToString("HH:mm:ss");
                    if (TotalFinalDec == 0 || TotalFinalDec != TotalDec)
                    {
                        TotalFinalDec = TotalDec;
                    }
                    //check số lượng nguyên liệu cho tất cả món đã chọn
                    bool f = await IngredientsServices.Ins.CheckIngredients(table.Products);
                    if (!f)
                    {
                        MessageBoxCF mb = new MessageBoxCF("Số lượng nguyên liệu không đủ!", MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        return;
                    }

                    string madh = await CheckOutServices.Ins.CheckOut(CusID, AdminServices.MaNhanVien, SelectedTableItem.MABAN, _DateBill, TotalFinalDec, VoucherPercentage, Note, table.Products, table.TIMEIn, HourBillOut, VoucherCode);
                    MADH = madh;

                    MaskName.Visibility = Visibility.Visible;
                    await TablesServices.Ins.SetStatusAvailableTable(SelectedTableItem.MABAN);
                    BillWindow bw = new BillWindow();
                    bw.ShowDialog();


                    //Xoá danh sách món ăn của bàn khi thanh toán
                    table.Products = new ObservableCollection<MenuItemDTO>();
                    table.TIMEIn = "";
                    table.GHICHU = "";

                    MainFrame.Content = new TablesPage();
                    ResetProperty();
                    MenuOfTable.Remove(table);
                }
                
            });

            closeCF = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #region page menu
            CheckSelectedStatusMenuCF = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                CheckComboboxMenu();
            });

            LoadMenuProducts = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await GetListMenuSource("");
            });

            AddToMenuTableList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                addToListMenuItem();
                await TablesServices.Ins.SetStatusNotAvailableTable((SelectedTableItem.MABAN));

                CalTotalBill();
                TotalFinalDec = TotalDec;
                TotalFinal = Helper.FormatVNMoney(TotalFinalDec);
            });

            IncreaseProduct = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p is MenuItemDTO item)
                {
                    var currentTable = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                    if (currentTable != null)
                    {
                        var product = ListProduct.Where(x => x.TENMON == item.TENMON).FirstOrDefault();
                        if (product != null)
                        {
                            var productInMenu = ListMenu.Where(x => x.TENMON == product.TENMON).FirstOrDefault();
                            if (productInMenu != null)
                            {
                                if (int.Parse(product.SOLUONG) + 1 > productInMenu.SOLUONG)
                                {
                                    MessageBoxCF mb = new MessageBoxCF("Số lượng sản phẩm không đủ!", MessageType.Error, MessageButtons.OK);
                                    mb.ShowDialog();
                                    return;
                                }
                            }

                            ObservableCollection<MenuItemDTO> temporaryList = new ObservableCollection<MenuItemDTO>(ListProduct);

                            ListProduct.Remove(product);
                            product.SOLUONG = (int.Parse(product.SOLUONG) + 1).ToString();
                            product.THANHTIEN = product.DONGIA * int.Parse(product.SOLUONG);
                            ListProduct.Add(product);

                            // Cập nhật lại ListProduct từ danh sách tạm thời
                            ListProduct = new ObservableCollection<MenuItemDTO>(temporaryList);

                            CalTotalBill();
                            TotalFinalDec = TotalDec;
                            TotalFinal = Helper.FormatVNMoney(TotalFinalDec);

                            currentTable.Products = ListProduct;
                        }
                    }
                }
            });

            DecreaseProduct = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p is MenuItemDTO item)
                {
                    var currentTable = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                    if (currentTable != null)
                    {

                        var product = ListProduct.Where(x => x.TENMON == item.TENMON).FirstOrDefault();
                        if (product != null)
                        {
                            if (int.Parse(product.SOLUONG) > 1)
                            {
                                // Tạo một danh sách tạm thời để lưu trữ các phần tử theo thứ tự ban đầu
                                ObservableCollection<MenuItemDTO> temporaryList = new ObservableCollection<MenuItemDTO>(ListProduct);

                                ListProduct.Remove(product);
                                product.SOLUONG = (int.Parse(product.SOLUONG) - 1).ToString();
                                product.THANHTIEN = product.DONGIA * int.Parse(product.SOLUONG);
                                ListProduct.Add(product);

                                // Cập nhật lại ListProduct từ danh sách tạm thời
                                ListProduct = new ObservableCollection<MenuItemDTO>(temporaryList);
                            }
                            else
                            {
                                ListProduct.Remove(product);
                            }

                            CalTotalBill();
                            TotalFinalDec = TotalDec;
                            TotalFinal = Helper.FormatVNMoney(TotalFinalDec);

                            currentTable.Products = ListProduct;
                        }
                    }
                }
            });
            #endregion
        }

        private void ResetProperty()
        {
            tableNumber = "";
            Employee = "";
            DateBill = "";
            ListProduct = null;
            Total = 0.ToString();
            CusName = "";
            CusPhone = "";
            CusPoint = 0;
            HourBillIn = "";
            HourBillOut = "";
            VoucherPercentage = 0;
            VoucherCode = "";
            TotalFinal = "";
            Note = "";
        }

        private void CalTotalBill()
        {
            TotalDec = 0;
            foreach (var i in ListProduct)
            {
                TotalDec += i.THANHTIEN;
            }
            Total = Helper.FormatVNMoney(TotalDec);
        }

        #region Lấy danh sách bàn
        private async Task GetListTableSource(string s = "")
        {
            ListTable = new ObservableCollection<TablesDTO>();
            switch (s)
            {
                case "":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetAllTables());
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
                case "Có khách":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetTablesNotAvailable());
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
                case "Còn trống":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetTablesAvailable());
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

        private async void loadListTables()
        {
            await GetListTableSource("");
        }

        private async void CheckCombobox()
        {
            switch(SelectedStatus.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetListTableSource("");
                        return;
                    }
                case "Có khách":
                    {
                        await GetListTableSource("Có khách");
                        return;
                    }
                case "Còn trống":
                    {
                        await GetListTableSource("Còn trống");
                        return;
                    }
            }
        }
        #endregion
    }
}
