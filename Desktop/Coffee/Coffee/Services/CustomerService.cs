using Coffee.DALs;
using Coffee.Models;
using Coffee.Utils;
using Coffee.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class CustomerService
    {
        private static CustomerService _ins;
        public static CustomerService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustomerService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Cập nhật điểm cho khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pointRank"></param>
        /// <returns></returns>
        public async Task<(string, double)> updatePointRankCustomer(string customerID, double pointRank)
        {
            (string label, CustomerDTO customer) = await CustomerDAL.Ins.getCustomer(customerID);

            return await CustomerDAL.Ins.updatePointRankCustomer(customerID, customer.DiemTichLuy + pointRank);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pointRank"></param>
        /// <returns></returns>
        public async Task<(string, bool)> checkUpdateRankCustomer(string customerID, double pointRank)
        {
            (string label, string rankID) = await CustomerDAL.Ins.getRankCustomer(customerID);

            string nextRankID = Helper.nextID(rankID, "TT");

            (string labelGetRank, RankModel rank) = await RankService.Ins.getRank(nextRankID);

            if (rank != null)
            {
                if (pointRank >= rank.DiemMucDoThanThiet)
                {
                    DetailRankModel detailRank = new DetailRankModel
                    {
                        MaMucDoThanThiet = rank.MaMucDoThanThiet,
                        NgayDatDuoc = DateTime.Now.ToString("dd/MM/yyyy")
                    };

                    return await CustomerDAL.Ins.createRankCustomer(customerID, detailRank);
                }
            }

            return ("Lỗi", false);
        }

        /// <summary>
        /// Láy rank của khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<(string, string)> getRankCustomer(string customerID)
        {
            return await CustomerDAL.Ins.getRankCustomer(customerID);
        }
    }
}
