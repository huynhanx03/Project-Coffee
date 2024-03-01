using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class CheckOutServices
    {
        public CheckOutServices() { }
        private static CheckOutServices _ins;
        public static CheckOutServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CheckOutServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<string> CheckOut(string _makh, string _manv, int _maban, string _datetime, decimal _total, int _isDiscount, string _ghichu, ObservableCollection<MenuItemDTO> _listproduct, string _timein, string _timeout, string _code) 
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = context.KHACHHANGs.Where(p => p.IDKHACHHANG == _makh).FirstOrDefault();
                    if (cus != null)
                    {
                        DONHANG newBill = new DONHANG();
                        newBill.MADH = CreateNextId(context.DONHANGs.Max(p => p.MADH), "DH");
                        newBill.IDKHACHHANG = cus.IDKHACHHANG;
                        newBill.IDNHANVIEN = _manv;
                        newBill.MABAN = _maban;
                        newBill.NGAYDH = DateTime.Parse(_datetime);
                        newBill.TONGGIATRIDONHANG = _total;
                        newBill.GHICHU = _ghichu;
                        newBill.TIME_IN = DateTime.Parse(_timein);
                        newBill.TIME_OUT = DateTime.Parse(_timeout);
                        newBill.DISCOUNT = _isDiscount;
                        context.DONHANGs.Add(newBill);

                        foreach (var item in _listproduct)
                        {
                            ObservableCollection<ImportProductIngredient> listProduct = new ObservableCollection<ImportProductIngredient>();
                            MON mon = await ProductServices.Ins.FindProduct(item.MAMON, item.SIZE);

                            if (mon != null)
                            {
                                //tìm danh sách nguyên liệu của món và trừ đi nguyên liệu trong kho
                                listProduct = new ObservableCollection<ImportProductIngredient>(await IngredientsServices.Ins.FindIngredients(mon.MAMON));
                                foreach (var ingredient in listProduct)
                                {
                                    var nguyenlieu = context.NGUYENLIEUx.Where(p => p.MANGUYENLIEU == ingredient.MaNguyenLieu).FirstOrDefault();
                                    nguyenlieu.SOLUONGTRONGKHO -= ingredient.SoLuong * int.Parse(item.SOLUONG);
                                }
                                context.SaveChanges();

                                await ProductServices.Ins.UpdateQuantity();
                            }

                            CTDH newCT = new CTDH();
                            newCT.MADH = newBill.MADH;
                            newCT.MAMON = item.MAMON;
                            newCT.SIZEMON = item.SIZE;
                            newCT.SOLUONG = int.Parse(item.SOLUONG);
                            newCT.TONGTIEN = item.THANHTIEN;
                            context.CTDHs.Add(newCT);
                        }

                        cus.TICHDIEM += (int)_total / 100;
                        cus.SODONHANG += 1;
                        await CustomerServices.Ins.UpdateRankCus(cus);
                        await VoucherServices.Ins.UpdateStatusVoucher(_code);
                        context.SaveChanges();
                        return newBill.MADH;
                    }
                    //Nếu không có tài khoản khách hàng thì sẽ tạo một khách hàng vãng lai mới
                    else
                    {
                        USER newUser = new USER();
                        newUser.IDUSER = CreateNextId(context.KHACHHANGs.Max(p => p.IDKHACHHANG), "KH");
                        newUser.USERNAME = "";
                        newUser.USERPASSWORD = "";
                        newUser.HOTEN = "Khách vãng lai";
                        newUser.EMAIL = "";
                        newUser.CCCD = "";
                        newUser.SODT = "";
                        newUser.DOB = DateTime.Now;
                        newUser.DIACHI = "";
                        newUser.NGBATDAU = DateTime.Now;
                        newUser.ROLE = 3;
                        newUser.IMAGESOURCE = "Resources/cup-of-coffee-avatar.jpg";
                        context.USERS.Add(newUser);

                        KHACHHANG newCus = new KHACHHANG();
                        newCus.IDKHACHHANG = CreateNextId(context.KHACHHANGs.Max(p => p.IDKHACHHANG), "KH");
                        newCus.TICHDIEM = 0;
                        newCus.SODONHANG = 1;
                        newCus.HANGTHANHVIEN = RANK.NORMAL;

                        context.KHACHHANGs.Add(newCus);

                        DONHANG newBill = new DONHANG();
                        newBill.MADH = CreateNextId(context.DONHANGs.Max(p => p.MADH), "DH");
                        newBill.IDKHACHHANG = newCus.IDKHACHHANG;
                        newBill.IDNHANVIEN = _manv;
                        newBill.MABAN = _maban;
                        newBill.NGAYDH = DateTime.Parse(_datetime);
                        newBill.TONGGIATRIDONHANG = _total;
                        newBill.GHICHU = _ghichu;
                        newBill.TIME_IN = DateTime.Parse(_timein);
                        newBill.TIME_OUT = DateTime.Parse(_timeout);
                        newBill.DISCOUNT = _isDiscount;
                        context.DONHANGs.Add(newBill);

                        foreach (var item in _listproduct)
                        {
                            ObservableCollection<ImportProductIngredient> listProduct = new ObservableCollection<ImportProductIngredient>();
                            MON mon = await ProductServices.Ins.FindProduct(item.MAMON, item.SIZE);

                            if (mon != null)
                            {
                                //tìm danh sách nguyên liệu của món và trừ đi nguyên liệu trong kho
                                listProduct = new ObservableCollection<ImportProductIngredient>(await IngredientsServices.Ins.FindIngredients(mon.MAMON));
                                foreach (var ingredient in listProduct)
                                {
                                    var nguyenlieu = context.NGUYENLIEUx.Where(p => p.MANGUYENLIEU == ingredient.MaNguyenLieu).FirstOrDefault();
                                    nguyenlieu.SOLUONGTRONGKHO -= ingredient.SoLuong * int.Parse(item.SOLUONG);
                                }
                                context.SaveChanges();

                                await ProductServices.Ins.UpdateQuantity();
                            }

                            CTDH newCT = new CTDH();
                            newCT.MADH = newBill.MADH;
                            newCT.MAMON = item.MAMON;
                            newCT.SIZEMON = item.SIZE;
                            newCT.SOLUONG = int.Parse(item.SOLUONG);
                            newCT.TONGTIEN = item.THANHTIEN;
                            context.CTDHs.Add(newCT);
                        }
                        context.SaveChanges();
                        return newBill.MADH;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private string CreateNextId(string maxId, string name)
        {
            if (maxId is null)
            {
                return name + "0001";
            }
            string newIdString = $"000{int.Parse(maxId.Substring(2)) + 1}";
            return name + newIdString.Substring(newIdString.Length - 4, 4);
        }
    }
}
