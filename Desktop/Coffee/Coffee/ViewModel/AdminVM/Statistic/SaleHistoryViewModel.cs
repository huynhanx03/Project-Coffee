using Coffee.DTOs;
using Coffee.Services;
using Coffee.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Statistic
{
    public partial class StatisticViewModel : BaseViewModel
    {
        #region variable
        private ObservableCollection<BillDTO> _BillList;
        public ObservableCollection<BillDTO> BillList
        {
            get { return _BillList; }
            set { _BillList = value; OnPropertyChanged(); }
        }
        private BillDTO _SelectedBill;
        public BillDTO SelectedBill
        {
            get { return _SelectedBill; }
            set { _SelectedBill = value; OnPropertyChanged(); }
        }
        #endregion

        #region Icommend
        public ICommand loadBillListIC { get; set; }
        #endregion


        /// <summary>
        /// load danh sách hóa đơn
        /// </summary>
        private async void loadBillList()
        {
            (string label, List<BillDTO> billlist) = await BillService.Ins.getListBill();

            if (billlist != null)
            {
                BillList = new ObservableCollection<BillDTO>(billlist);
            }
        }
        /// <summary>
        /// xóa hóa đơn
        /// </summary>
        public async void deleteBill()
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận xoá hóa đơn?", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isDeleteBillList) = await BillService.Ins.DeleteBill(SelectedBill.MaHoaDon);

                if (isDeleteBillList)
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                    loadBillList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
        }
    }
}
