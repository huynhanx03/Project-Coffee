using Coffee.Models;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DALs
{
    public class CustomerDAL
    {
        private static CustomerDAL _ins;
        public static CustomerDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustomerDAL();
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
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("KhachHang/" + customerID, new { DiemTichLuy = pointRank });

                    return ("Cập nhật điểm thành công", pointRank);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, -1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<(string, CustomerDTO)> getCustomer(string customerID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("KhachHang/" + customerID);

                    CustomerDTO customer = response.ResultAs<CustomerDTO>();

                    return ("Lấy khách hàng thành công", customer);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Láy rank của khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<(string, string)> getRankCustomer(string customerID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("ChiTietMucDoThanThiet/" + customerID + "/ChiTiet");

                    Dictionary<string, DetailRankModel>  detailRankData = response.ResultAs<Dictionary<string, DetailRankModel>>();

                    return ("Lấy mức độ thân thiết của khách hàng thành công", detailRankData.Last().Value.MaMucDoThanThiet);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<(string, List<string>)> getCustomerIDByRankMininum(string rankID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("ChiTietMucDoThanThiet/");

                    Dictionary<string, string> customerData = response.ResultAs<Dictionary<string, string>>();

                    List<string> customerIDList = customerData.Keys.ToList();
                    
                    List<string> result = new List<string>();

                    foreach (var customerID in customerIDList)
                    {
                        FirebaseResponse responseDetailRank = await context.Client.GetTaskAsync("ChiTietMucDoThanThiet/" + customerID + "/ChiTiet");

                        Dictionary<string, DetailRankModel> detailRankData = responseDetailRank.ResultAs<Dictionary<string, DetailRankModel>>();

                        int compare = string.Compare(detailRankData.Last().Value.MaMucDoThanThiet, rankID);

                        if (compare >= 0)
                            result.Add(customerID);
                    }

                    return ("Lấy danh sách mã khách hàng thành công", result);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Tạo mức độ thân thiết mới cho khách hầng
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="detailRank"></param>
        /// <returns></returns>
        public async Task<(string, bool)> createRankCustomer(string customerID, DetailRankModel detailRank)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("ChiTietMucDoThanThiet/" + customerID + "/ChiTiet/" + detailRank.MaMucDoThanThiet, detailRank);

                    return ("Lấy mức độ thân thiết của khách hàng thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
