using QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM;
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

namespace QuanLyChuoiCuaHangCoffee.Views.Login
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_Click_Close(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Mask.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void sdt_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]{0,10}$");

            TextBox textBox = (TextBox)sender;
            string inputText = textBox.Text;

            if (!regex.IsMatch(inputText))
            {
                textBox.Text = inputText.Remove(inputText.Length - 1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
}
