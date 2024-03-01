using Library.ViewModel;
using Microsoft.Win32;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.IngredientsPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM;
using System.Windows.Media;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.IngredientsVM
{
    public class IngredientsViewModel : BaseViewModel
    {
        #region variable
        private ComboBoxItem _SelectStockFilter { get; set; }
        public ComboBoxItem SelectStockFilter
        {
            get { return _SelectStockFilter; }
            set { _SelectStockFilter = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IngredientsDTO> _ListIngredients { get; set; }
        public ObservableCollection<IngredientsDTO> ListIngredients
        {
            get { return _ListIngredients; }
            set { _ListIngredients = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ImportIngredientsDTO> _ListImport { get; set; }
        public ObservableCollection<ImportIngredientsDTO> ListImport
        {
            get { return _ListImport; }
            set { _ListImport = value; OnPropertyChanged(); }
        }

        private bool _IsGettingSource { get; set; }
        public bool IsGettingSource
        {
            get { return _IsGettingSource; }
            set { _IsGettingSource = value; OnPropertyChanged(); }
        }

        private string _TenNguyenLieu { get; set; }
        public string TenNguyenLieu
        {
            get { return _TenNguyenLieu; }
            set { _TenNguyenLieu = value; OnPropertyChanged(); }
        }

        private string _SoLuong { get; set; }
        public string SoLuong
        {
            get { return _SoLuong; }
            set { _SoLuong = value; OnPropertyChanged(); }
        }

        private string _DonVi { get; set; }
        public string DonVi
        {
            get { return _DonVi; }
            set { _DonVi = value; OnPropertyChanged(); }
        }

        private string _Gia { get; set; }
        public string Gia
        {
            get { return _Gia; }
            set { _Gia = value; OnPropertyChanged(); }
        }

        private int _TriGia { get; set; }
        public int TriGia
        {
            get { return _TriGia; }
            set { _TriGia = value; OnPropertyChanged(); }
        }

        private string _TriGiaStr { get; set; }
        public string TriGiaStr
        {
            get { return _TriGiaStr; }
            set { _TriGiaStr = value; OnPropertyChanged(); }
        }
        #endregion


        #region Icommand
        public ICommand LoadIngredients { get; set; }
        public ICommand CheckSelectStockFilterCF { get; set; }
        public ICommand ImportIngredients { get; set; }
        public ICommand MaskNameIngredients { get; set; }
        public ICommand AddIngredientToList { get; set; }
        public ICommand SaveIngredient { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand importExcel { get; set; }
        public ICommand EditDTG { get; set; }
        public ICommand DeleteDTG { get; set; }
        public ICommand CommitEditDTG { get; set; }
        public ICommand CancelEditDTG { get; set; }
        #endregion

        public Grid MaskName { get; set; }
        public DataGrid import_dtg;
        public IngredientsViewModel()
        {
            //ImportIngredientsWindow iiw = new ImportIngredientsWindow();
            //iiw.employeeName.Content = AdminServices.TenNhanVien;

            ListImport = new ObservableCollection<ImportIngredientsDTO>();

            Loaded = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                p.ItemsSource = ListImport;
                import_dtg = p;
            });

            LoadIngredients = new RelayCommand<ListView>((p) => { return true; }, async (p) =>
            {
                IsGettingSource = true;
                await GetIngredientsListSource("");
                IsGettingSource = false;
            });

            CheckSelectStockFilterCF = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await checkCombobox();
            });

            MaskNameIngredients = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            ImportIngredients = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                ImportIngredientsWindow w = new ImportIngredientsWindow();
                w.employeeId.Content = AdminServices.MaNhanVien;
                w.employeeName.Content = AdminServices.TenNhanVien;
                w.dateCreated.Content = DateTime.Now;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            AddIngredientToList = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                AddToList();
            });

            SaveIngredient = new RelayCommand<DataGrid>((p) => { return true; }, async (p) =>
            {
                string s = await ImportIngredientsServices.Ins.Import(AdminServices.MaNhanVien, DateTime.Now, TriGia, ListImport);
                MessageBoxCF ms = new MessageBoxCF(s, MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
                ListImport.Clear();
                UpdateTotal();
                await GetIngredientsListSource("");
            });

            importExcel = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                AddToListByExcel();
                MessageBoxCF ms = new MessageBoxCF("Thêm nguyên liệu từ excel thành công", MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            });

            DeleteDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                MessageBoxCF ms = new MessageBoxCF("Bạn muốn xoá nguyên liệu này?", MessageType.Waitting, MessageButtons.YesNo);
                if (ms.ShowDialog() == true)
                {
                    ImportIngredientsDTO ingredient = import_dtg.SelectedItems[0] as ImportIngredientsDTO;
                    ListImport.Remove(ingredient);
                    UpdateTotal();
                }
            });

            EditDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataGridRow row = (DataGridRow)import_dtg.ItemContainerGenerator.ContainerFromItem(import_dtg.CurrentItem);
                ShowCellsEditingTemplate(row);
            });

            CommitEditDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataGridRow row = (DataGridRow)import_dtg.ItemContainerGenerator.ContainerFromItem(import_dtg.CurrentItem);
                ShowCellsNormalTemplate(row, true);
                UpdateTotal();
            });

            CancelEditDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataGridRow row = (DataGridRow)import_dtg.ItemContainerGenerator.ContainerFromItem(import_dtg.CurrentItem);
                ShowCellsNormalTemplate(row);
            });
        }

        private void AddToList()
        {
            if (string.IsNullOrEmpty(TenNguyenLieu) || string.IsNullOrEmpty(Gia) || string.IsNullOrEmpty(SoLuong) || string.IsNullOrEmpty(DonVi))
            {
                MessageBoxCF ms = new MessageBoxCF("Vui lòng điền đầy đủ thông tin", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            } else
            {
                ImportIngredientsDTO item = new ImportIngredientsDTO();
                item.TenNguyenLieu = TenNguyenLieu;
                item.SoLuong = int.Parse(SoLuong);
                item.DonVi = DonVi;
                item.Gia = decimal.Parse(Gia);

                ListImport.Add(item);
                UpdateTotal();

                TenNguyenLieu = SoLuong = DonVi = Gia = null;
            }
        }

        private void AddToListByExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files|*.xlsx;*xls";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string tennguyenlieu = worksheet.Cells[row, 1].Value?.ToString();
                        string soluong = worksheet.Cells[row, 2].Value?.ToString();
                        string donvi = worksheet.Cells[row, 3].Value?.ToString();
                        decimal gia = decimal.Parse(worksheet.Cells[row, 4].Value?.ToString());

                        ImportIngredientsDTO item = new ImportIngredientsDTO();
                        item.TenNguyenLieu = tennguyenlieu;
                        item.SoLuong = int.Parse(soluong);
                        item.DonVi = donvi;
                        item.Gia = gia;

                        ListImport.Add(item);
                    }

                    UpdateTotal();
                }
            }
        }

        private void UpdateTotal()
        {
            TriGia = 0;
            foreach(var item in ListImport)
            {
                TriGia += (int)(item.SoLuong * item.Gia);
            }

            TriGiaStr = Helper.FormatVNMoney(TriGia);
        }

        #region load ingredient list
        public async Task GetIngredientsListSource(string s = "")
        {

            ListIngredients = new ObservableCollection<IngredientsDTO>();
            switch (s)
            {
                case "":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListIngredients = new ObservableCollection<IngredientsDTO>(await IngredientsServices.Ins.GetAllIngredients());
                            IsGettingSource = false;
                            return;
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
                case "InStock":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListIngredients = new ObservableCollection<IngredientsDTO>(await IngredientsServices.Ins.GetIngredientsInStock());
                            IsGettingSource = false;
                            return;
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
                case "OutOfStock":
                    {
                        try
                        {
                            IsGettingSource = true;
                            ListIngredients = new ObservableCollection<IngredientsDTO>(await IngredientsServices.Ins.GetIngredientsOutOfStock());
                            IsGettingSource = false;
                            return;
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

        public async Task checkCombobox()
        {
            switch (SelectStockFilter.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetIngredientsListSource("");
                        return;
                    }
                case "Còn hàng":
                    {
                        await GetIngredientsListSource("InStock");
                        return;
                    }
                case "Hết hàng":
                    {
                        await GetIngredientsListSource("OutOfStock");
                        return;
                    }
            }
        }
        #endregion

        #region custom datagrid
        private void ShowCellsEditingTemplate(DataGridRow row)
        {
            foreach (DataGridColumn col in import_dtg.Columns)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(col.GetCellContent(row));
                while (parent.GetType().Name != "DataGridCell")
                    parent = VisualTreeHelper.GetParent(parent);

                DataGridCell cell = ((DataGridCell)parent);
                DataGridTemplateColumn c = (DataGridTemplateColumn)col;
                if (c.CellEditingTemplate != null)
                    cell.Content = ((DataGridTemplateColumn)col).CellEditingTemplate.LoadContent();
            }
        }

        private void ShowCellsNormalTemplate(DataGridRow row, bool canCommit = false)
        {
            foreach (DataGridColumn col in import_dtg.Columns)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(col.GetCellContent(row));
                while (parent.GetType().Name != "DataGridCell")
                    parent = VisualTreeHelper.GetParent(parent);

                DataGridCell cell = ((DataGridCell)parent);
                DataGridTemplateColumn c = (DataGridTemplateColumn)col;
                if (col.DisplayIndex != 0)
                {
                    if (canCommit == true)
                    {
                        TextBox textBox = cell.Content as TextBox;
                        if (textBox != null)
                        {
                            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        }
                    }
                    else
                    {
                        TextBox textBox = cell.Content as TextBox;
                        if (textBox != null)
                        {
                            textBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    }
                }
                cell.Content = c.CellTemplate.LoadContent();
            }
        }
        #endregion
    }
}
