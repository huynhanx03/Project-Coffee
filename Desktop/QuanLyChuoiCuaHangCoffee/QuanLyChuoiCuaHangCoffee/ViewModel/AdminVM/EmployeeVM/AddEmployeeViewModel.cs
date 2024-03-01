using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.EmployeeVM
{
    public partial class EmployeeViewModel : BaseViewModel
    {
        #region variable
        private string _NameAdd { get; set; }
        public string NameAdd { get => _NameAdd; set { _NameAdd = value; OnPropertyChanged(); } }

        private string _PhoneAdd { get; set; }
        public string PhoneAdd { get => _PhoneAdd; set { _PhoneAdd = value; OnPropertyChanged(); } }

        private DateTime _DOBAdd { get; set; }
        public DateTime DOBAdd { get => _DOBAdd; set { _DOBAdd = value; OnPropertyChanged(); } }

        private string _AddressAdd { get; set; }
        public string AddressAdd { get => _AddressAdd; set { _AddressAdd = value; OnPropertyChanged(); } }

        private string _EmailAdd { get; set; }
        public string EmailAdd { get => _EmailAdd; set { _EmailAdd = value; OnPropertyChanged(); } }

        private string _CCCDAdd { get; set; }
        public string CCCDAdd { get => _CCCDAdd; set { _CCCDAdd = value; OnPropertyChanged(); } }

        private string _UsernameAdd { get; set; }
        public string UsernameAdd { get => _UsernameAdd; set { _UsernameAdd = value; OnPropertyChanged(); } }

        private string _PasswordAdd { get; set; }
        public string PasswordAdd { get => _PasswordAdd; set { _PasswordAdd = value; OnPropertyChanged(); } }

        private bool _IsNullNameAdd { get; set; }
        public bool IsNullNameAdd { get => _IsNullNameAdd; set { _IsNullNameAdd = value; OnPropertyChanged(); } }

        private bool _IsNullEmailAdd { get; set; }
        public bool IsNullEmailAdd { get => _IsNullEmailAdd; set { _IsNullEmailAdd = value; OnPropertyChanged(); } }

        private bool _IsNullPhoneAdd { get; set; }
        public bool IsNullPhoneAdd { get => _IsNullPhoneAdd; set { _IsNullPhoneAdd = value; OnPropertyChanged(); } }

        private string _SelectedPosition { get; set; }
        public string SelectedPosition { get => _SelectedPosition; set { _SelectedPosition = value; OnPropertyChanged(); } }

        private DateTime _SelectedDate { get; set; }
        public DateTime SelectedDate { get => _SelectedDate; set { _SelectedDate = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _ListPosition { get; set; }
        public ObservableCollection<string> ListPosition { get => _ListPosition; set { _ListPosition = value; OnPropertyChanged(); } }
        #endregion

        #region command

        public ICommand ConfirmAddEmployee { get; set; }
        public ICommand SelectedDateChanged { get; set; }

        #endregion
    }
}
