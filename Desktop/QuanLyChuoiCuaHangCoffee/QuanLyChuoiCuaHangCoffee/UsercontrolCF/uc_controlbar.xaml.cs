using QuanLyChuoiCuaHangCoffee.ViewModel;
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

namespace QuanLyChuoiCuaHangCoffee.UsercontrolCF
{
    /// <summary>
    /// Interaction logic for uc_controlbar.xaml
    /// </summary>
    public partial class uc_controlbar : UserControl
    {
        public uc_controlbarViewModel cbViewModel { get; set; }
        public uc_controlbar()
        {
            InitializeComponent();
            this.DataContext = cbViewModel = new uc_controlbarViewModel();
        }
    }
}
