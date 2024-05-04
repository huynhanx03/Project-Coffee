using Coffee.DTOs;
using Coffee.Services;
using Coffee.Views.Admin.EvaluatePage;
using Coffee.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Evaluate
{
    public partial class EvaluateViewModel: BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }

        private ObservableCollection<EvaluateDTO> _EvaluateList;

        public ObservableCollection<EvaluateDTO> EvaluateList
        {
            get { return _EvaluateList; }
            set { _EvaluateList = value; OnPropertyChanged(); }
        }

        private List<EvaluateDTO> __EvaluateList;

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand loadEvaluateListIC { get; set; }
        public ICommand searchEvaluateIC { get; set; }
        public ICommand deleteEvaluateIC { get; set; }
        #endregion

        public EvaluateViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            loadEvaluateListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadEvaluateList();
            });

            deleteEvaluateIC = new RelayCommand<EvaluateDTO>((p) => { return true; }, (p) =>
            {
                deleteEvaluate(p);
            });

            searchEvaluateIC = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (__EvaluateList != null)
                        EvaluateList = new ObservableCollection<EvaluateDTO>(__EvaluateList.FindAll(x => x.TenSanPham.ToLower().Contains(p.Text.ToLower()) || x.TenKhachHang.ToLower().Contains(p.Text.ToLower())));
                }
            });
        }

        #region function
        /// <summary>
        /// Load danh sách đánh giá
        /// </summary>
        private async void loadEvaluateList()
        {
            (string label, List<EvaluateDTO> Evaluates) = await EvaluateService.Ins.getListEvaluate();

            if (Evaluates != null)
            {
                EvaluateList = new ObservableCollection<EvaluateDTO>(Evaluates);
                __EvaluateList = new List<EvaluateDTO>(Evaluates);
            }
        }

        /// <summary>
        /// Xoá đánh giá
        /// </summary>
        public async void deleteEvaluate(EvaluateDTO evaluate)
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận xoá đánh giá?", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isDeleteEvaluate) = await EvaluateService.Ins.DeleteEvaluate(evaluate);

                if (isDeleteEvaluate)
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                    loadEvaluateList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
        }

        #endregion
    }
}
