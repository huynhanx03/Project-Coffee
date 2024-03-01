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

namespace QuanLyChuoiCuaHangCoffee.Views.Customer.SettingPage
{
    /// <summary>
    /// Interaction logic for MainSettingCusPage.xaml
    /// </summary>
    public partial class MainSettingCusPage : Page
    {
        public static ImageBrush Image;
        public MainSettingCusPage()
        {
            InitializeComponent();
            Image = img;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]{0,12}$");

            TextBox textBox = (TextBox)sender;
            string inputText = textBox.Text;

            if (!regex.IsMatch(inputText))
            {

                textBox.Text = inputText.Remove(inputText.Length - 1);
                textBox.CaretIndex = textBox.Text.Length;
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
