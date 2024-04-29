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
    public partial class StatisticViewModel:BaseViewModel
    {
        #region variable
        private ObservableCollection<ImportDTO> _BillImportList;
        public ObservableCollection<ImportDTO> BillImportList
        {
            get { return _BillImportList; }
            set { _BillImportList = value; OnPropertyChanged(); }
        }
        private ImportDTO _SelectedBillImport;
        public ImportDTO SelectedBillImport
        {
            get { return _SelectedBillImport; }
            set { _SelectedBillImport = value; OnPropertyChanged(); }
        }
        #endregion

        #region Icommand
        public ICommand loadWarehouseListIC {  get; set; }
        #endregion
        /// <summary>
        /// load danh sách hóa đơn nhập kho
        /// </summary>
        private async void loadWareHouseList()
        {
            (string label, List<ImportDTO> billimportlist) = await BillImportService.Ins.getListBillImport();

            if (billimportlist != null)
            {
                BillImportList = new ObservableCollection<ImportDTO>(billimportlist);
            }
        }
        /// <summary>
        /// xóa hóa đơn nhập kho
        /// </summary>
        public async void deleteBillImport()
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận xoá hóa đơn nhập kho?", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isDeleteBillImportList) = await BillImportService.Ins.DeleteBillImport(SelectedBillImport.MaPhieuNhapKho);

                if (isDeleteBillImportList)
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                    loadWareHouseList();
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
