using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Utils.Helper;
using Coffee.Views.MessageBox;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coffee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     

        public MainWindow()
        {
            InitializeComponent();
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            t();
        }
        public async void t()
        {
            TableDTO table = new TableDTO
            {
                MaBan = "BA0010",
                TenBan = "X"
            };

            TableDAL.Ins.updateTable(table);
        }
    }
}
