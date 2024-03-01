using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM.DashboardCusVM
{
    public class DashboardCusViewModel : BaseViewModel
    {
        #region variable
        private string _NameCus { get; set; }
        public string NameCus { get => _NameCus; set { _NameCus = value; OnPropertyChanged(); } }

        private string _GenderCus { get; set; }
        public string GenderCus { get => _GenderCus; set { _GenderCus = value; OnPropertyChanged(); } }

        private DateTime _DOBCus { get; set; }
        public DateTime DOBCus { get => _DOBCus; set { _DOBCus = value; OnPropertyChanged(); } }

        private DateTime _DateStartCus { get; set; }
        public DateTime DateStartCus { get => _DateStartCus; set { _DateStartCus = value; OnPropertyChanged(); } }

        private string _CusRank { get; set; }
        public string CusRank { get => _CusRank; set { _CusRank = value; OnPropertyChanged(); } }

        private int _CultivatedPointed { get; set; }
        public int CultivatedPointed { get => _CultivatedPointed; set { _CultivatedPointed = value; OnPropertyChanged(); } }

        private int _OrdersCus { get; set; }
        public int OrdersCus { get => _OrdersCus; set { _OrdersCus = value; OnPropertyChanged(); } }

        private string _TotalSpending { get; set; }
        public string TotalSpending { get => _TotalSpending; set { _TotalSpending = value; OnPropertyChanged(); } }

        private string _ImgRankSource { get; set;}
        public string ImgRankSource { get => _ImgRankSource; set { _ImgRankSource = value; OnPropertyChanged(); } }

        private string _ImgAvatarSource { get; set; }
        public string ImgAvatarSource { get => _ImgAvatarSource; set { _ImgAvatarSource = value; OnPropertyChanged(); } }
        #endregion

        public ICommand LoadInformation { get; set; }

        public DashboardCusViewModel()
        {
            LoadInformation = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NameCus = CustomerServices.TENKH;
                GenderCus = "Nam";
                DOBCus = CustomerServices.DOB;
                DateStartCus = CustomerServices.NGBATDAU;
                CusRank = CustomerServices.RANKKH;
                CultivatedPointed = CustomerServices.TICHDIEM;
                OrdersCus = CustomerServices.SODONHANG;
                TotalSpending = Helper.FormatVNMoney(CustomerServices.TICHDIEM * 100);
                ImgAvatarSource = CustomerServices.IMAGESOURCE;
                switch (CustomerServices.RANKKH)
                {
                    case "Thường":
                        {
                            ImgRankSource = "/Resources/norank.png";
                            return;
                        }
                    case "Đồng":
                        {
                            ImgRankSource = "/Resources/bronze.png";
                            return;
                        }
                    case "Bạc":
                        {
                            ImgRankSource = "/Resources/silver.png";
                            return;
                        }
                    case "Vàng":
                        {
                            ImgRankSource = "/Resources/gold.png";
                            return;
                        }
                    case "Kim cương":
                        {
                            ImgRankSource = "/Resources/diamond.png";
                            return;
                        }
                }
            });
        }
    }
}
