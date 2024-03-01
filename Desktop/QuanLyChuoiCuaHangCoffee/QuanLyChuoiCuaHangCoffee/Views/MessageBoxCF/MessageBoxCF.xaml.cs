using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF
{
    /// <summary>
    /// Interaction logic for MessageBoxCF.xaml
    /// </summary>
    public partial class MessageBoxCF : Window
    {
        public MessageBoxCF(string Message, MessageType type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;

            switch (type)
            {
                case MessageType.Accept:
                    System.Media.SystemSounds.Beep.Play();
                    imageStatus.Source = new BitmapImage(new Uri("pack://application:,,,/QuanLyChuoiCuaHangCoffee;component/Resources/check-circle.png"));
                    break;
                case MessageType.Waitting:
                    System.Media.SystemSounds.Beep.Play();
                    imageStatus.Source = new BitmapImage(new Uri("pack://application:,,,/QuanLyChuoiCuaHangCoffee;component/Resources/waitting.png"));
                    break;
                case MessageType.Error:
                    System.Media.SystemSounds.Beep.Play();
                    imageStatus.Source = new BitmapImage(new Uri("pack://application:,,,/QuanLyChuoiCuaHangCoffee;component/Resources/x-circle.png"));
                    break;
            }

            switch (Buttons)
            {
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.OK:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

    public enum MessageButtons
    {
        YesNo,
        OK
    }
    public enum MessageType
    {
        Accept,
        Waitting,
        Error
    }
}
