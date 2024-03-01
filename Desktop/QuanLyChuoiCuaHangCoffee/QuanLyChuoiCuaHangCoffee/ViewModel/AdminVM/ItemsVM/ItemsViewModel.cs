using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Library.ViewModel;
using Microsoft.Win32;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.ItemsPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.ItemsVM
{
    public class ItemsViewModel : BaseViewModel
    {
        #region variables binding
        private string _TenSanPham { get; set; }
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }

        private string _SelectedType { get; set; }
        public string SelectedType { get => _SelectedType; set { _SelectedType = value; OnPropertyChanged(); } }

        private string _SelectedSize { get; set; }
        public string SelectedSize { get => _SelectedSize; set { _SelectedSize = value; OnPropertyChanged(); } }

        private string _Gia { get; set; }
        public string Gia { get => _Gia; set { _Gia = value; OnPropertyChanged(); } }

        private string _SelectedIngredient { get; set; }
        public string SelectedIngredient { get => _SelectedIngredient; set { _SelectedIngredient = value; OnPropertyChanged(); } }

        private int _SoLuong { get; set; }
        public int SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _DonVi { get; set; }
        public string DonVi { get => _DonVi; set { _DonVi = value; OnPropertyChanged(); } }

        private bool _IsGettingSource { get; set; }
        public bool IsGettingSource
        {
            get { return _IsGettingSource; }
            set { _IsGettingSource = value; OnPropertyChanged(); }
        }

        private bool _IsGettingSourceAdd { get; set; }
        public bool IsGettingSourceAdd
        {
            get { return _IsGettingSourceAdd; }
            set { _IsGettingSourceAdd = value; OnPropertyChanged(); }
        }

        private string _imgSource;
        public string ImgSource
        {
            get { return _imgSource; }
            set { _imgSource = value; OnPropertyChanged(); }
        }

        private string _imagesource { get; set; }
        public string imagesource { get => _imagesource; set { _imagesource = value; OnPropertyChanged(); } }

        private string _contentBtn { get; set; }
        public string contentBtn { get => _contentBtn; set { _contentBtn = value; OnPropertyChanged(); } }  


        private ObservableCollection<ProductsDTO> _ListMenu { get; set; }
        public ObservableCollection<ProductsDTO> ListMenu { get => _ListMenu; set { _ListMenu = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _productTypes { get; set; }
        public ObservableCollection<string> productTypes { get => _productTypes; set { _productTypes = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _productSizes { get; set; }
        public ObservableCollection<string> productSizes { get => _productSizes; set { _productSizes = value; OnPropertyChanged(); } }

        private ObservableCollection<IngredientsDTO> _ListNguyenLieuBase { get; set; }
        public ObservableCollection<IngredientsDTO> ListNguyenLieuBase { get => _ListNguyenLieuBase; set { _ListNguyenLieuBase = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListNguyenLieu { get; set; }
        public ObservableCollection<string> ListNguyenLieu { get => _ListNguyenLieu; set { _ListNguyenLieu = value; OnPropertyChanged(); } }

        private ObservableCollection<ImportProductIngredient> _ListImport { get; set; }
        public ObservableCollection<ImportProductIngredient> ListImport
        {
            get { return _ListImport; }
            set { _ListImport = value; OnPropertyChanged(); }
        }

        #endregion

        #region icommand
        public ICommand LoadProducts { get; set; }
        public ICommand OpenAddMenuWindow { get; set; }
        public ICommand AddIngredientToList { get; set; }
        public ICommand ImportImage { get; set; }
        public ICommand AddDish { get; set; }
        public ICommand LoadBaseData { get; set; }
        public ICommand IngredientChange { get; set; }
        public ICommand MaskNameItems { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand EditProduct { get; set; }
        public ICommand DeleteProduct { get; set; }
        public ICommand EditDTG { get; set; }
        public ICommand DeleteDTG { get; set; }
        public ICommand CommitEditDTG { get; set; }
        public ICommand CancelEditDTG { get; set; }

        #endregion

        public Grid MaskName { get; set; }
        public DataGrid import_dtg;
        public ItemsViewModel()
        {
            ListImport = new ObservableCollection<ImportProductIngredient>();
            ListNguyenLieuBase = new ObservableCollection<IngredientsDTO>();

            LoadProducts = new RelayCommand<ListView>((p) => { return true; }, async (p) =>
            {
                loadProducts();
            });

            Loaded = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                p.ItemsSource = ListImport;
                import_dtg = p;
            });

            MaskNameItems = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            //Mở cửa sổ thêm sản phẩm
            OpenAddMenuWindow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TenSanPham = SelectedType = SelectedSize = Gia = ImgSource = imagesource = DonVi = null;

                SelectedIngredient = null;
                ListImport.Clear();

                SoLuong = 0;
                MaskName.Visibility = Visibility.Visible;
                AddMenuWindow adw = new AddMenuWindow();
                contentBtn = "Thêm sản phẩm";
                adw.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            //Load dữ liệu cơ bản
            LoadBaseData = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListNguyenLieu = new ObservableCollection<string>();
                productTypes = new ObservableCollection<string>(ProductTypes.ListLoaiSanPham);
                productSizes = new ObservableCollection<string>(Sizes.ListKichThuoc);
                ListNguyenLieuBase = new ObservableCollection<IngredientsDTO>(await IngredientsServices.Ins.GetAllIngredients());

                foreach (var item in ListNguyenLieuBase)
                {
                    ListNguyenLieu.Add(item.TENNGUYENLIEU);
                }
            });

            //Thay đổi đơn vị khi thay đổi nguyên liệu
            IngredientChange = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                IngredientsDTO ingredient = ListNguyenLieuBase.Where(x => x.TENNGUYENLIEU == SelectedIngredient).FirstOrDefault();
                if (ingredient != null)
                {
                    DonVi = ingredient.DONVI;
                }
            });

            //Thêm ảnh
            ImportImage = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsGettingSourceAdd = true;
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG File (.jpg)|*.jpg";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {

                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(dlg.FileName);
                    img.EndInit();
                    AddMenuWindow.Image.Source = img;
                    Account account = new Account("dg0uneomp", "924294962494475", "Ahrb-2beUzb0TEJpKjHck2IYCGI");

                    Cloudinary cloudinary = new Cloudinary(account);
                    cloudinary.Api.Secure = true;
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(dlg.FileName)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    ImgSource = uploadResult.Url.ToString();
                }
                IsGettingSourceAdd = false;

            });

            //Thêm nguyên liệu vào danh sách
            AddIngredientToList = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                addToList();
            });

            //Thêm món ăn
            AddDish = new RelayCommand<DataGrid>((p) => { return true; }, async (p) =>
            {
                if (contentBtn == "Thêm sản phẩm")
                {
                    if (string.IsNullOrEmpty(TenSanPham) || string.IsNullOrEmpty(SelectedType) || string.IsNullOrEmpty(SelectedSize) || string.IsNullOrEmpty(Gia) || string.IsNullOrEmpty(ImgSource) || ListImport.Count == 0)
                    {
                        MessageBoxCF ms = new MessageBoxCF("Vui lòng nhập đủ thông tin", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                        return;
                    }
                    (string s, bool flag) = await ImportIngredientsProductServices.Ins.AddToMenu(TenSanPham, SelectedType, SelectedSize, decimal.Parse(Gia), ImgSource, ListImport);
                    MessageBoxCF mb = new MessageBoxCF(s, flag ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();

                    TenSanPham = SelectedIngredient = SelectedType = SelectedSize = Gia = ImgSource = imagesource = DonVi = null;
                    ListImport = null;

                    loadProducts();
                } else
                {
                    ProductsDTO pd = ListMenu.Where(x => x.TENMON.ToLower() == TenSanPham.ToLower()).FirstOrDefault();
                    if (pd != null)
                    {
                        string s = await ProductServices.Ins.UpdateProduct(pd, TenSanPham, SelectedType, SelectedSize, imagesource, Gia, ListImport);

                        MessageBoxCF mb = new MessageBoxCF(s, MessageType.Accept, MessageButtons.OK);
                        mb.ShowDialog();

                        loadProducts();
                    }
                }
                

            });

            //Sửa sản phẩm
            EditProduct = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (p is ProductsDTO product)
                {
                    ProductsDTO pd = ListMenu.Where(x => x.MAMON == product.MAMON).FirstOrDefault();
                    if (pd != null)
                    {
                        MaskName.Visibility = Visibility.Visible;
                        AddMenuWindow adw = new AddMenuWindow();
                        TenSanPham = pd.TENMON;
                        SelectedType = pd.LOAIMON;
                        SelectedSize = pd.SIZESANPHAM;
                        imagesource = pd.IMAGESOURCE;
                        Gia = pd.GIASANPHAM.ToString("F0");
                        contentBtn = "Cập nhật sản phẩm";
                        ListImport = new ObservableCollection<ImportProductIngredient>(await IngredientsServices.Ins.FindIngredients(pd.MAMON));



                        adw.ShowDialog();
                        MaskName.Visibility = Visibility.Collapsed;
                    }



                }
            });

            //xoá sản phẩm
            DeleteProduct = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (p is ProductsDTO product)
                {
                    MessageBoxCF ms = new MessageBoxCF("Bạn muốn xoá sản phẩm này?", MessageType.Waitting, MessageButtons.YesNo);
                    if (ms.ShowDialog() == true)
                    {
                        string s = await ProductServices.Ins.DeleteProduct(product.MAMON);
                        MessageBoxCF mb = new MessageBoxCF(s, MessageType.Accept, MessageButtons.OK);
                        mb.ShowDialog();

                        loadProducts();
                    }
                    
                }
            });
            #region edit datagrid
            DeleteDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                MessageBoxCF ms = new MessageBoxCF("Bạn muốn xoá nguyên liệu này?", MessageType.Waitting, MessageButtons.YesNo);
                if (ms.ShowDialog() == true)
                {
                    ImportProductIngredient ingredient = import_dtg.SelectedItems[0] as ImportProductIngredient;
                    ListImport.Remove(ingredient);
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
            });

            CancelEditDTG = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataGridRow row = (DataGridRow)import_dtg.ItemContainerGenerator.ContainerFromItem(import_dtg.CurrentItem);
                ShowCellsNormalTemplate(row);
            });
            #endregion
        }

        private async void loadProducts()
        {
            MaskName.Visibility = Visibility.Visible;
            IsGettingSource = true;
            ListMenu = new ObservableCollection<ProductsDTO>( await ProductServices.Ins.GetAllProducts());
            IsGettingSource = false;
            MaskName.Visibility = Visibility.Collapsed;
        }   

        private void addToList()
        {
            if (string.IsNullOrEmpty(SelectedIngredient) || SoLuong == 0)
            {
                MessageBoxCF mb = new MessageBoxCF("Vui lòng nhập đầy đủ thông tin", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                return;
            }
            else
            {
                ImportProductIngredient newItem = new ImportProductIngredient();
                newItem.MaNguyenLieu = ListNguyenLieuBase.Where(x => x.TENNGUYENLIEU == SelectedIngredient).FirstOrDefault().MANGUYENLIEU;
                newItem.TenNguyenLieu = SelectedIngredient;
                newItem.SoLuong = SoLuong;
                newItem.DonVi = DonVi;

                ListImport.Add(newItem);

                SelectedIngredient = null;
                SoLuong = 0;
                DonVi = null;

            }
        }

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
