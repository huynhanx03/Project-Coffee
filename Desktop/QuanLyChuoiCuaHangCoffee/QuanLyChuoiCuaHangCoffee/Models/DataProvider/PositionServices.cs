using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class PositionServices
    {
        public PositionServices() { }
        private static PositionServices _ins;
        public static PositionServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new PositionServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<CHUCDANH> FindPosition(string _tenchucdanh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var position = context.CHUCDANHs.Where(p => p.TENCHUCDANH == _tenchucdanh).FirstOrDefault();
                    return position;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
