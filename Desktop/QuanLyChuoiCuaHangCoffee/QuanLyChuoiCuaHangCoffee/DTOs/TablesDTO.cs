using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class TablesDTO
    {
        public int MABAN { get; set; }
        public string TRANGTHAI { get; set; }
    }

    public class MenuOfTableDTO
    {
        public int MABAN { get; set; }
        public string TIMEIn { get; set; }
        public string GHICHU { get; set; }
        public ObservableCollection<MenuItemDTO> Products { get; set; }
    }
}
