using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyChuoiCuaHangCoffee.Views.Admin.ItemsPage
{
    /// <summary>
    /// Interaction logic for AddMenuWindow.xaml
    /// </summary>
    public partial class AddMenuWindow : Window
    {
        public static Image Image;
        public AddMenuWindow()
        {
            InitializeComponent();
            Image = img;
        }

        private void tbgia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbSoLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSoLuong.Text))
            {
                tbSoLuong.Text = "0";
            }   
        }
    }
}
