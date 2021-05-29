﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public partial class TOrder
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponMoney { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crtime { get; set; }
    }
}