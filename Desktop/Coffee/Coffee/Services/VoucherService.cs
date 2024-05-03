using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Models;
using Coffee.Utils;
using Coffee.Utils.Helper;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class VoucherService
    {
        private static VoucherService _ins;
        public static VoucherService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new VoucherService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        ///     Trả về danh sách phiếu giảm giá
        /// </returns>
        public async Task<(string, List<VoucherDTO>)> getAllVoucher()
        {
            return await VoucherDAL.Ins.getAllVoucher();
        }

        /// <summary>
        /// Thêm phiếu giảm giá
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public async Task<(string, VoucherDTO)> createVoucher(VoucherDTO voucher)
        {
            string MaxVouhcerID = await VoucherDAL.Ins.getMaxVoucherID();

            string NewVoucher = Helper.nextID(MaxVouhcerID, "VC");

            voucher.MaPhieuGiamGia = NewVoucher;

            (string label, VoucherDTO voucherCreate) = await VoucherDAL.Ins.createVoucher(voucher);

            (string labelGetCustomerID, List<string> customerIDList) = await CustomerDAL.Ins.getCustomerIDByRankMininum(voucher.HangToiThieu);

            await VoucherDAL.Ins.createDetailVoucher(voucher.MaPhieuGiamGia, customerIDList);

            return (label, voucherCreate);
        }

        /// <summary>
        /// Xoá phiếu giảm giá
        /// </summary>
        /// <param name="VoucherID"> mã phiếu giảm giá </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteVoucher(string VoucherID)
        {
            return await VoucherDAL.Ins.DeleteVoucher(VoucherID);
        }
    }
}
