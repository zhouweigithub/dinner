﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public partial class TPay
    {
        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Orderid { get; set; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string WxOrderid { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crtime { get; set; }
    }
}