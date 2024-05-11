﻿using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class OrderService
    {
        private static OrderService _ins;
        public static OrderService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new OrderService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách đơn hàng
        /// </returns>
        public async Task<(string, List<OrderDTO>)> getListOrder()
        {
            return await OrderDAL.Ins.getListOrder();
        }

        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<(string, bool)> updateStatusOrder(string orderId, string status)
        {
            return await OrderDAL.Ins.updateStatusOrder(orderId, status);
        }
    }
}
