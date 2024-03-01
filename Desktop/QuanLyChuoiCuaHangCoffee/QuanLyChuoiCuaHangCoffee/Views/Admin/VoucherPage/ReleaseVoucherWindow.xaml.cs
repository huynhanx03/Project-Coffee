using QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyChuoiCuaHangCoffee.Views.Admin.VoucherPage
{
    /// <summary>
    /// Interaction logic for ReleaseVoucherWindow.xaml
    /// </summary>
    public partial class ReleaseVoucherWindow : Window
    {
        public ReleaseVoucherWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;

            btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFA5B9D6");
            btn.Background = new SolidColorBrush(Colors.OrangeRed);
        }
        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            Label lb = sender as Label;

            lb.Content = DateTime.Today.ToShortDateString();
        }
    }
}
