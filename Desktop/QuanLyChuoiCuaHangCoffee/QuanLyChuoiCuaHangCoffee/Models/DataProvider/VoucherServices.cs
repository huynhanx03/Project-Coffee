using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class VoucherServices
    {
        private static VoucherServices _ins;
        public static VoucherServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new VoucherServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task CreateVoucher(List<string> _listVoucher, int _percentage, DateTime _expiredDate, string _reason)
        {
            try
            {
                List<VOUCHER> vouchers = _listVoucher.Select(x => new VOUCHER
                {
                    CODE = x,
                    DISCOUNT = _percentage,
                    DATEEXPIRED = _expiredDate,
                    VOUCHERSTATUS = VOUCHER_STATUS.UNRELEASED,
                    REASON = _reason,
                }).ToList();

                using (var context = new CoffeeManagementEntities())
                {
                    context.VOUCHERs.AddRange(vouchers);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<VoucherDTO>> GetListVoucher()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listVoucher = (from s in context.VOUCHERs
                                       select new VoucherDTO
                                       {
                                           MAVOUCHER = s.MAVOUCHER,
                                           CODEVOUCHER = s.CODE,
                                           DISCOUNT = (int)s.DISCOUNT,
                                           DATEEXPIRED = (DateTime)s.DATEEXPIRED,
                                           STATUS = s.VOUCHERSTATUS,
                                           REASON = s.REASON,
                                       }).ToListAsync();
                    return await listVoucher;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<VoucherDTO>> GetListVoucherCondition(string _s)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listvoucher = (from s in context.VOUCHERs
                                       where s.VOUCHERSTATUS == _s
                                       select new VoucherDTO
                                       {
                                           MAVOUCHER = s.MAVOUCHER,
                                           CODEVOUCHER = s.CODE,
                                           DISCOUNT = (int)s.DISCOUNT,
                                           DATEEXPIRED = (DateTime)s.DATEEXPIRED,
                                           STATUS = s.VOUCHERSTATUS,
                                           REASON = s.REASON,
                                       }).ToListAsync();
                    return await listvoucher;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<VoucherDTO>> GetListVoucherCus()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listVoucher = (from s in context.VOUCHERs
                                       where s.VOUCHERSTATUS != VOUCHER_STATUS.RELEASED && s.VOUCHERSTATUS != VOUCHER_STATUS.USED && s.VOUCHERSTATUS != VOUCHER_STATUS.EXPIRED
                                       select new VoucherDTO
                                       {
                                           MAVOUCHER = s.MAVOUCHER,
                                           CODEVOUCHER = s.CODE,
                                           DISCOUNT = (int)s.DISCOUNT,
                                           DATEEXPIRED = (DateTime)s.DATEEXPIRED,
                                           STATUS = s.VOUCHERSTATUS,
                                           REASON = s.REASON,
                                       }).ToListAsync();
                    return await listVoucher;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(bool, string)> ReleaseMultiVoucher(List<string> _listVoucherCode)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    foreach (var item in _listVoucherCode)
                    {
                        var voucher = context.VOUCHERs.Where(p => p.CODE == item).FirstOrDefault();
                        if (voucher == null)
                        {
                            return (false, "Tồn tại voucher không có trong danh sách");
                        } else
                        {
                            voucher.VOUCHERSTATUS = VOUCHER_STATUS.RELEASED;
                            context.SaveChanges();
                        }
                    }
                    return (true, "Phát hành thành công");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(VoucherDTO, string)> FindVoucher(string _code)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var voucher = context.VOUCHERs.Where(p => p.CODE == _code).FirstOrDefault();
                    if (voucher == null)
                    {
                        return (null, "Không tồn tại voucher");
                    }
                    else if (voucher.VOUCHERSTATUS == VOUCHER_STATUS.EXPIRED)
                    {
                        return (null, "Voucher đã hết hạn");
                    }
                    else if (voucher.VOUCHERSTATUS == VOUCHER_STATUS.USED)
                    {
                        return (null, "Voucher đã được sử dụng");
                    }
                    else
                    {
                        var voucherDTO = new VoucherDTO
                        {
                            MAVOUCHER = voucher.MAVOUCHER,
                            CODEVOUCHER = voucher.CODE,
                            DISCOUNT = (int)voucher.DISCOUNT,
                            DATEEXPIRED = (DateTime)voucher.DATEEXPIRED,
                            STATUS = voucher.VOUCHERSTATUS,
                            REASON = voucher.REASON,
                        };
                        return (voucherDTO, "");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateStatusVoucher(string _code)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var voucher = context.VOUCHERs.Where(p => p.CODE == _code).FirstOrDefault();
                    if (voucher != null)
                    {
                        voucher.VOUCHERSTATUS = VOUCHER_STATUS.USED;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateExpiredVoucher() 
        {             
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var listVoucher = context.VOUCHERs.ToList();
                    foreach (var item in listVoucher)
                    {
                        if (item.DATEEXPIRED < DateTime.Now && item.VOUCHERSTATUS != VOUCHER_STATUS.USED)
                        {
                            item.VOUCHERSTATUS = VOUCHER_STATUS.EXPIRED;
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
