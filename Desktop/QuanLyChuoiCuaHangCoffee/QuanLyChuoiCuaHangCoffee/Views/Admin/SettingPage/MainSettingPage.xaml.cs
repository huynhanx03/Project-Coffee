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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChuoiCuaHangCoffee.Views.Admin.SettingPage
{
    /// <summary>
    /// Interaction logic for MainSettingPage.xaml
    /// </summary>
    public partial class MainSettingPage : Page
    {
        public MainSettingPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]{0,10}$"); // Chỉ cho phép nhập từ 0 đến 12 chữ số

            TextBox textBox = (TextBox)sender;
            string inputText = textBox.Text;

            if (!regex.IsMatch(inputText))
            {
                // Nếu đầu vào không phù hợp với điều kiện, xóa ký tự cuối cùng
                textBox.Text = inputText.Remove(inputText.Length - 1);
                textBox.CaretIndex = textBox.Text.Length; // Đặt vị trí con trỏ vào cuối
            }
        }

        private void FloatingPasswordBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FloatingPasswordBox.Password = string.Empty;
        }

        private void FloatingPasswordBoxCurrent_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FloatingPasswordBoxCurrent.Password = string.Empty;
        }
    }
}
