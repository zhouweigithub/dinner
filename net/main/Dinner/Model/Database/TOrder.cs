// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Table("t_order")]
    [Index(nameof(Userid), Name = "fk_order_userid")]
    public partial class TOrder
    {
        public TOrder()
        {
            TComment = new HashSet<TComment>();
            TOrderCoupon = new HashSet<TOrderCoupon>();
            TOrderProduct = new HashSet<TOrderProduct>();
            TPay = new HashSet<TPay>();
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("id")]
        [StringLength(32)]
        public string Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [Column("userid")]
        public int Userid { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        [Column("money")]
        public decimal Money { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        [Column("coupon_money")]
        public decimal CouponMoney { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        [Column("pay_money")]
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Column("phone")]
        [StringLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 状态（0待支付，1已支付，2已完成，9已取消，10已删除）
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("crdate", TypeName = "date")]
        public DateTime Crdate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [ForeignKey(nameof(Userid))]
        [InverseProperty(nameof(TUser.TOrder))]
        public virtual TUser User { get; set; }
        [InverseProperty("Order")]
        public virtual TOrderCallback TOrderCallback { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<TComment> TComment { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<TOrderCoupon> TOrderCoupon { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<TOrderProduct> TOrderProduct { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<TPay> TPay { get; set; }
    }
}