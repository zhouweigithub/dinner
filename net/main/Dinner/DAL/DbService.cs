using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DbService : DbContext
    {

        public DbService(DbContextOptions<DbService> options) : base(options)
        {

        }

        public virtual DbSet<TCategory> TCategory { get; set; }
        public virtual DbSet<TComment> TComment { get; set; }
        public virtual DbSet<TCompany> TCompany { get; set; }
        public virtual DbSet<TCoupon> TCoupon { get; set; }
        public virtual DbSet<TFeedback> TFeedback { get; set; }
        public virtual DbSet<TOrder> TOrder { get; set; }
        public virtual DbSet<TOrderCallback> TOrderCallback { get; set; }
        public virtual DbSet<TOrderCoupon> TOrderCoupon { get; set; }
        public virtual DbSet<TOrderProduct> TOrderProduct { get; set; }
        public virtual DbSet<TProduct> TProduct { get; set; }
        public virtual DbSet<TUser> TUser { get; set; }
        public virtual DbSet<TUserCoupon> TUserCoupon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.HasComment("商品分类");

                entity.Property(e => e.Id).HasComment("自增id");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Name).HasComment("名称");

                entity.Property(e => e.State)
                    .HasDefaultValueSql("'0'")
                    .HasComment("状态0正常 1禁用");
            });

            modelBuilder.Entity<TComment>(entity =>
            {
                entity.HasComment("评论");

                entity.Property(e => e.Id).HasComment("自增id");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Msg).HasComment("内容");

                entity.Property(e => e.Orderid).HasComment("订单编号");
            });

            modelBuilder.Entity<TCompany>(entity =>
            {
                entity.HasComment("公司");

                entity.Property(e => e.Id).HasComment("自增主键");

                entity.Property(e => e.Address).HasComment("公司地址");

                entity.Property(e => e.Code).HasComment("唯一编码");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Name).HasComment("公司名字");
            });

            modelBuilder.Entity<TCoupon>(entity =>
            {
                entity.HasComment("优惠卷信息");

                entity.Property(e => e.Id).HasComment("主键");

                entity.Property(e => e.Crtime)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("优惠卷创建时间");

                entity.Property(e => e.EndTime).HasComment("使用截止日期");

                entity.Property(e => e.Money)
                    .HasPrecision(10, 2)
                    .HasComment("优惠金额");

                entity.Property(e => e.Name).HasComment("优惠卷名称");

                entity.Property(e => e.StartTime).HasComment("使用开始日期");
            });

            modelBuilder.Entity<TFeedback>(entity =>
            {
                entity.HasComment("用户反馈");

                entity.Property(e => e.Id).HasComment("自增id");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Msg).HasComment("内容");

                entity.Property(e => e.Userid).HasComment("用户id");
            });

            modelBuilder.Entity<TOrder>(entity =>
            {
                entity.HasComment("订单信息");

                entity.Property(e => e.Id).HasComment("订单编号");

                entity.Property(e => e.CouponMoney)
                    .HasPrecision(20, 2)
                    .HasComment("优惠金额");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Money)
                    .HasPrecision(20, 2)
                    .HasComment("订单金额");

                entity.Property(e => e.PayMoney)
                    .HasPrecision(20, 2)
                    .HasComment("实际支付金额");

                entity.Property(e => e.State).HasComment("状态");

                entity.Property(e => e.Userid).HasComment("用户id");
            });

            modelBuilder.Entity<TOrderCallback>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("PRIMARY");

                entity.HasComment("微信支付回调");

                entity.Property(e => e.Orderid).HasComment("订单编号");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.State)
                    .HasDefaultValueSql("'0'")
                    .HasComment("状态");

                entity.Property(e => e.WxOrderid).HasComment("微信支付订单号");
            });

            modelBuilder.Entity<TOrderCoupon>(entity =>
            {
                entity.HasKey(e => new { e.Orderid, e.Couponid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasComment("订单中使用的优惠卷");

                entity.Property(e => e.Orderid).HasComment("订单编号");

                entity.Property(e => e.Couponid).HasComment("优惠卷id");

                entity.Property(e => e.Count)
                    .HasDefaultValueSql("'0'")
                    .HasComment("数量");

                entity.Property(e => e.Money)
                    .HasPrecision(10, 2)
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("总金额");
            });

            modelBuilder.Entity<TOrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.Orderid, e.Productid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasComment("订单中的商品信息");

                entity.Property(e => e.Orderid).HasComment("订单编号");

                entity.Property(e => e.Productid).HasComment("商品id");

                entity.Property(e => e.Count)
                    .HasDefaultValueSql("'0'")
                    .HasComment("数量");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Money)
                    .HasPrecision(10, 2)
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("金额");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("单价");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasComment("商品");

                entity.Property(e => e.Id).HasComment("自增主键");

                entity.Property(e => e.Category)
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品分类");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Name).HasComment("商品名称");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("价格");

                entity.Property(e => e.Sales)
                    .HasDefaultValueSql("'0'")
                    .HasComment("销量");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.TProduct)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("t_product_ibfk_1");
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasComment("用户");

                entity.Property(e => e.Id).HasComment("自增ID");

                entity.Property(e => e.Code).HasComment("唯一编码");

                entity.Property(e => e.Companyid)
                    .HasDefaultValueSql("'0'")
                    .HasComment("公司id");

                entity.Property(e => e.Crtime).HasComment("创建时间");

                entity.Property(e => e.Nick).HasComment("昵称");
            });

            modelBuilder.Entity<TUserCoupon>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Couponid })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasComment("用户的优惠卷");

                entity.Property(e => e.Userid).HasComment("用户id");

                entity.Property(e => e.Couponid).HasComment("优惠卷id");

                entity.Property(e => e.Count)
                    .HasDefaultValueSql("'0'")
                    .HasComment("数量");
            });

        }

    }
}
