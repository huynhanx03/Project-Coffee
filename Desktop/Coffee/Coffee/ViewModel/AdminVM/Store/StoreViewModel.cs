using Coffee.DTOs;
using Coffee.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Store
{
    public partial class StoreViewModel: BaseViewModel, IConstraintViewModel
    {
        #region variable

        public Grid MaskName { get; set; }

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        
        #endregion

        public StoreViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            confirmCreateDiscountProductIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                confirmCreateDiscountProduct();
            });

            loadDiscountProductListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadDiscountProductList();
            });

            uploadImageIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                uploadImage();
            });

            createBannerIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                createBanner();
            });

            loadBannerIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadBanner();
            });

            deleteBannerIC = new RelayCommand<BannerModel>((p) => { return true; }, (p) =>
            {
                deleteBanner(p);
            });
        }

        #region function
        /// <summary>
        /// Kiểm tra chỉ được nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion
    }
}
