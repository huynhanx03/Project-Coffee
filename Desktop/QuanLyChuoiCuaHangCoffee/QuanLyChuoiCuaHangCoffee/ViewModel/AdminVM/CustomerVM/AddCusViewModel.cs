using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.CustomerVM
{
    public partial class CustomerViewModel : BaseViewModel
    {
        #region variable
        private string _NameRe { get; set; }
        public string NameRe { get => _NameRe; set { _NameRe = value; OnPropertyChanged(); } }

        private string _EmailRe { get; set; }
        public string EmailRe { get => _EmailRe; set { _EmailRe = value; OnPropertyChanged(); } }

        private string _PhoneRe { get; set; }
        public string PhoneRe { get => _PhoneRe; set { _PhoneRe = value; OnPropertyChanged(); } }

        private string _UsernameRe { get; set; }
        public string UsernameRe { get => _UsernameRe; set { _UsernameRe = value; OnPropertyChanged(); } }

        private string _PasswordRe { get; set; }
        public string PasswordRe { get => _PasswordRe; set { _PasswordRe = value; OnPropertyChanged(); } }

        #endregion

        #region command
        public ICommand ConfirmAddCus { get; set; }
        #endregion
    }
}
