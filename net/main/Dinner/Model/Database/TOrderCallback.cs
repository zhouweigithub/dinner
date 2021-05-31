﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 微信支付回调
    /// </summary>
    [Table("t_order_callback")]
    public partial class TOrderCallback
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("orderid")]
        [StringLength(32)]
        public string Orderid { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        [Required]
        [Column("wx_orderid")]
        [StringLength(32)]
        public string WxOrderid { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [ForeignKey(nameof(Orderid))]
        [InverseProperty(nameof(TOrder.TOrderCallback))]
        public virtual TOrder Order { get; set; }
    }
}