﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 用户的优惠卷
    /// </summary>
    public partial class TUserCoupon
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 优惠卷id
        /// </summary>
        public int Couponid { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        public int CouponId1 { get; set; }
        public int UserId1 { get; set; }

        public virtual TCoupon Coupon { get; set; }
        public virtual TUser User { get; set; }
    }
}