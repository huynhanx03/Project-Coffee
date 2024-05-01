using Coffee.Properties;
using Coffee.Views.Admin.ChatPage;
using Coffee.Views.Admin.EmployeePage;
using Coffee.Views.Admin.IngredientPage;
using Coffee.Views.Admin.MenuPage;
using Coffee.Views.Admin.SettingPage;
using Coffee.Views.Admin.StatisticPage;
using Coffee.Views.Admin.TablePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Coffee.ViewModel.AdminVM
{
    public class MainAdminViewModel: BaseViewModel
    {
        public int _role { get; set; }
        public int role 
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(); } 
        }

        public ICommand loadDashboardPageIC { get; set; }
        public ICommand loadTablesPageIC { get; set; }
        public ICommand loadMenuPageIC { get; set; }
        public ICommand loadIngredientsPageIC { get; set; }
        public ICommand loadStatisticPageIC { get; set; }
        public ICommand loadEmployeePageIC { get; set; }
        public ICommand loadSettingPageIC { get; set; }
        public ICommand loadChatPageIC { get; set; }
        public ICommand loadRoleIC { get; set; }
        public ICommand changeLanguageIC { get; set; }

        private string _optionName { get; set; }
        public string optionName
        {
            get { return _optionName; }
            set { _optionName = value; OnPropertyChanged(); }
        }

        public MainAdminViewModel()
        {
            loadRoleIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                role = 1;
            });

            loadDashboardPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                //optionName = "Trang chủ";
            });

            loadTablesPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainTablePage();
                //optionName = "Bàn";
            });

            loadMenuPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainMenuPage();
                //optionName = "Thực đơn";
            });

            loadIngredientsPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainIngredientPage();
                //optionName = "Nguyên liệu";
            });

            loadStatisticPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainStatisticPage();
                //optionName = "Hoá đơn";
            });

            loadEmployeePageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainEmployeePage();
                //optionName = "Nhân viên";
            });

            loadSettingPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingPage();
                //optionName = (string)Application.Current.Resources["Setting"];
            });

            loadChatPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainChatPage();
            });

            changeLanguageIC = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                ResourceDictionary dic = new ResourceDictionary();

                switch (p.Name)
                {
                    case "btnVi":
                        dic.Source = new Uri("..\\ResourcesXAML\\Languages\\LanguageVi.xaml", UriKind.Relative);
                        break;

                    case "btnEn":
                        dic.Source = new Uri("..\\ResourcesXAML\\Languages\\LanguageEn.xaml", UriKind.Relative);
                        break;

                    default:
                        dic.Source = new Uri("..\\ResourcesXAML\\Languages\\LanguageVi.xaml", UriKind.Relative);
                        break;
                }

                Application.Current.Resources.MergedDictionaries.Add(dic);
            });
        }
    }
}
