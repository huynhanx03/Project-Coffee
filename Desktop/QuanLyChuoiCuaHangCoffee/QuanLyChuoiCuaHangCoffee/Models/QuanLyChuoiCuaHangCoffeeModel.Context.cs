﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyChuoiCuaHangCoffee.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CoffeeManagementEntities : DbContext
    {
        public CoffeeManagementEntities()
            : base("name=CoffeeManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BAN> BANs { get; set; }
        public virtual DbSet<CTDH> CTDHs { get; set; }
        public virtual DbSet<CTMON> CTMONs { get; set; }
        public virtual DbSet<CTNHAPKHO> CTNHAPKHOes { get; set; }
        public virtual DbSet<CHUCDANH> CHUCDANHs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<MON> MONs { get; set; }
        public virtual DbSet<NGUYENLIEU> NGUYENLIEUx { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NHAPKHO> NHAPKHOes { get; set; }
        public virtual DbSet<SIZE> SIZEs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<VOUCHER> VOUCHERs { get; set; }
    }
}
