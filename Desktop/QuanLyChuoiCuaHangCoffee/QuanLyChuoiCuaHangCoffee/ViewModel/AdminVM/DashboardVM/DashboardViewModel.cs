using Library.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.DashboardVM
{
    public class DashboardViewModel : BaseViewModel
    {
        #region variable
        private SeriesCollection _ProfitData { get; set; }
        public SeriesCollection ProfitData { get => _ProfitData; set { _ProfitData = value; OnPropertyChanged(); } }

        private SeriesCollection _TopBeverage { get; set; }
        public SeriesCollection TopBeverage { get => _TopBeverage; set { _TopBeverage = value; OnPropertyChanged(); } }

        private SeriesCollection _TopFood { get; set; }
        public SeriesCollection TopFood { get => _TopFood; set { _TopFood = value; OnPropertyChanged(); } }

        private SeriesCollection _TopCus { get; set; }
        public SeriesCollection TopCus { get => _TopCus; set { _TopCus = value; OnPropertyChanged(); } }

        private ComboBoxItem _SelectedProfitPeriod;
        public ComboBoxItem SelectedProfitPeriod
        {
            get { return _SelectedProfitPeriod; }
            set { _SelectedProfitPeriod = value; OnPropertyChanged(); }
        }

        private string _SelectedProfitTime;
        public string SelectedProfitTime
        {
            get { return _SelectedProfitTime; }
            set { _SelectedProfitTime = value; OnPropertyChanged(); }
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; }
        }

        private ObservableCollection<ProductsDTO> _TopBeverageDetail { get; set; }
        public ObservableCollection<ProductsDTO> TopBeverageDetail { get => _TopBeverageDetail; set { _TopBeverageDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<ProductsDTO> _TopFoodDetail { get; set; }
        public ObservableCollection<ProductsDTO> TopFoodDetail { get => _TopFoodDetail; set { _TopFoodDetail = value; OnPropertyChanged(); } }

        private ObservableCollection<CustomerDTO> _TopCusDetail { get; set; }
        public ObservableCollection<CustomerDTO> TopCusDetail { get => _TopCusDetail; set { _TopCusDetail = value; OnPropertyChanged(); } }

        private string _Profit { get; set; }
        public string Profit { get => _Profit; set { _Profit = value; OnPropertyChanged(); } }

        private int _LabelMaxValue { get; set; }
        public int LabelMaxValue { get => _LabelMaxValue; set { _LabelMaxValue = value; OnPropertyChanged(); } }
        #endregion

        #region icommand
        public ICommand ChangeProfitPeriodCF { get; set; }
        public ICommand LoadPieChart { get; set; }
        #endregion


        public DashboardViewModel()
        {
            ChangeProfitPeriodCF = new RelayCommand<ComboBox>((p) => { return true; }, async (p) =>
            {
                await ChangePeriod();
            });

            LoadPieChart = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadTopBeverage(2023);
                await LoadTopFood(2023);
                await LoadTopCus();
            });
        }

        #region profit
        public async Task ChangePeriod()
        {
            if (SelectedProfitPeriod != null)
            {
                switch (SelectedProfitPeriod.Content.ToString())
                {
                    case "Theo năm":
                        {
                            if (SelectedProfitTime != null)
                            {
                                if (SelectedProfitTime.Length == 4)
                                    SelectedYear = int.Parse(SelectedProfitTime);
                                await LoadProfitByYear();
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            if (SelectedProfitTime != null)
                            {
                                await LoadProfitByMonth();
                            }
                            return;
                        }
                }
            }
        }

        public async Task LoadProfitByYear()
        {
            if (SelectedProfitTime.Length != 4) return;
            LabelMaxValue = 12;

            try
            {
                (List<decimal> ListIncomeMonth, decimal TotalProfit) = await StatisticServices.Ins.GetRevenueByYear(SelectedYear);
                (List<decimal> ListExpenseMonth, decimal TotalExpense) = await StatisticServices.Ins.GetExpenseByYear(SelectedYear);

                ListIncomeMonth.Insert(0, 0);
                ListExpenseMonth.Insert(0, 0);

                for (int i = 1; i <= 12; i++)
                {
                    ListIncomeMonth[i] /= 1000000;
                    ListExpenseMonth[i] /= 1000000;
                }

                Profit = Helper.FormatVNMoney((TotalProfit - TotalExpense));

                ProfitData = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu",
                    Values = new ChartValues<decimal>(ListIncomeMonth),
                    PointGeometry = DefaultGeometries.Square,
                },
                new LineSeries
                {
                    Title = "Chi",
                    Values = new ChartValues<decimal>(ListExpenseMonth),
                }
            };
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu.", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống.", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }

        public async Task LoadProfitByMonth()
        {
            try
            {
                if (SelectedProfitTime.Length == 4) return;
                LabelMaxValue = 31;

                (List<decimal> ListIncomeDay, decimal TotalProfit) = await StatisticServices.Ins.GetRevenueByMonth(int.Parse(SelectedProfitTime.Remove(0, 6)), SelectedYear);
                (List<decimal> ListExpenseDay, decimal TotalExpense) = await StatisticServices.Ins.GetExpenseByMonth(int.Parse(SelectedProfitTime.Remove(0, 6)), SelectedYear);

                ListIncomeDay.Insert(0, 0);
                ListExpenseDay.Insert(0, 0);

                for (int i = 1; i <= ListIncomeDay.Count - 1; i++)
                {
                    ListIncomeDay[i] /= 1000000;
                    ListExpenseDay[i] /= 1000000;
                }

                Profit = Helper.FormatVNMoney((TotalProfit - TotalExpense));

                ProfitData = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Thu",
                        Values = new ChartValues<decimal> (ListIncomeDay),
                        PointGeometry = DefaultGeometries.Square,
                    },
                    new LineSeries
                    {
                        Title = "Chi",
                        Values = new ChartValues<decimal> (ListExpenseDay),
                    }
                };
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu.", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống.", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }

        #endregion

        #region top beverage
        public async Task LoadTopBeverage(int year)
        {
            try
            {
                TopBeverageDetail = new ObservableCollection<ProductsDTO>(await StatisticServices.Ins.GetTopBeverage(year));
                TopBeverage = new SeriesCollection();
                foreach (var item in TopBeverageDetail)
                {
                    PieSeries p = new PieSeries
                    {
                        Values = new ChartValues<decimal> { item.SOLUONG },
                        Title = item.TENMON,
                        DataLabels = true,
                    };
                    TopBeverage.Add(p);
                }
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }

        }

        public async Task LoadTopFood(int year)
        {
            try
            {
                TopFoodDetail = new ObservableCollection<ProductsDTO>(await StatisticServices.Ins.GetTopFood(year));
                TopFood = new SeriesCollection();
                foreach (var item in TopFoodDetail)
                {
                    PieSeries p = new PieSeries
                    {
                        Values = new ChartValues<decimal> { item.SOLUONG },
                        Title = item.TENMON,
                        DataLabels = true,
                    };
                    TopFood.Add(p);
                }
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }

        }
        #endregion

        public async Task LoadTopCus()
        {
            try
            {
                TopCusDetail = new ObservableCollection<CustomerDTO>(await StatisticServices.Ins.GetTopCus());
                TopCus = new SeriesCollection();

                decimal money = 0;
                foreach(var item in TopCusDetail)
                {
                    if (item.HOTEN == "Khách vãng lai")
                    {
                        money += item.CHITIEU;
                    }
                }

                foreach (var item in TopCusDetail)
                {
                    if (item.HOTEN != "Khách vãng lai")
                    {
                        PieSeries p = new PieSeries
                        {
                            Values = new ChartValues<decimal> { item.CHITIEU },
                            Title = item.HOTEN,
                            DataLabels = true,
                        };
                        TopCus.Add(p);
                    }
                }

                TopCus.Add(new PieSeries
                {
                    Values = new ChartValues<decimal> { money },
                    Title = "Khách vãng lai",
                    DataLabels = true,
                });
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
            }
        }
    }
}
