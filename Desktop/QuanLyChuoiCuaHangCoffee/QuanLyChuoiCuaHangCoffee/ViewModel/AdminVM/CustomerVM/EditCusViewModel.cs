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
        private string _NameEdit { get; set; }
        public string NameEdit { get => _NameEdit; set { _NameEdit = value; OnPropertyChanged(); } }

        private string _PhoneEdit { get; set; }
        public string PhoneEdit { get => _PhoneEdit; set { _PhoneEdit = value; OnPropertyChanged(); } }

        private DateTime _DOBEdit { get; set; }
        public DateTime DOBEdit { get => _DOBEdit; set { _DOBEdit = value; OnPropertyChanged(); } }

        private string _AddressEdit { get; set; }
        public string AddressEdit { get => _AddressEdit; set { _AddressEdit = value; OnPropertyChanged(); } }

        private string _EmailEdit { get; set; }
        public string EmailEdit { get => _EmailEdit; set { _EmailEdit = value; OnPropertyChanged(); } }

        private string _CCCDEdit { get; set; }
        public string CCCDEdit { get => _CCCDEdit; set { _CCCDEdit = value; OnPropertyChanged(); } }

        private string _UsernameEdit { get; set; }
        public string UsernameEdit { get => _UsernameEdit; set { _UsernameEdit = value; OnPropertyChanged(); } }

        private string _PasswordEdit { get; set; }
        public string PasswordEdit { get => _PasswordEdit; set { _PasswordEdit = value; OnPropertyChanged(); } }

        private bool _IsNullNameEdit { get; set; }
        public bool IsNullNameEdit { get => _IsNullNameEdit; set { _IsNullNameEdit = value; OnPropertyChanged(); } }

        private bool _IsNullEmailEdit { get; set; }
        public bool IsNullEmailEdit { get => _IsNullEmailEdit; set { _IsNullEmailEdit = value; OnPropertyChanged(); } }

        private bool _IsNullPhoneEdit { get; set; }
        public bool IsNullPhoneEdit { get => _IsNullPhoneEdit; set { _IsNullPhoneEdit = value; OnPropertyChanged(); } }

        private DateTime _SelectedDate { get; set; }
        public DateTime SelectedDate { get => _SelectedDate; set { _SelectedDate = value; OnPropertyChanged(); } }
        #endregion

        #region command

        public ICommand ConfirmEditCus { get; set; }
        public ICommand SelectedDateChanged { get; set; }

        #endregion

        private void ResetProperty()
        {
            _AddressEdit = string.Empty;
            _CCCDEdit = string.Empty;
            _DOBEdit = DateTime.Now;
            _EmailEdit = string.Empty;
            _NameEdit = string.Empty;
            _PasswordEdit = string.Empty;
            _PhoneEdit = string.Empty;
            _UsernameEdit = string.Empty;
        }
    }
}
