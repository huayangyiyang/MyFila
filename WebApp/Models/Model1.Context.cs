﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyShopEntities : DbContext
    {
        public MyShopEntities()
            : base("name=MyShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<cart> cart { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<GoodsColor> GoodsColor { get; set; }
        public virtual DbSet<GoodsComment> GoodsComment { get; set; }
        public virtual DbSet<GoodsImage> GoodsImage { get; set; }
        public virtual DbSet<GoodsSize> GoodsSize { get; set; }
        public virtual DbSet<GoodsType> GoodsType { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<Userinfo> Userinfo { get; set; }
    }
}
