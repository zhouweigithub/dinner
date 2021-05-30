﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class TUser
    {
        public TUser()
        {
            TUserCoupon = new HashSet<TUserCoupon>();
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 公司id
        /// </summary>
        public int Companyid { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 头像图片
        /// </summary>
        public string Headimg { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crtime { get; set; }

        public virtual TCompany Company { get; set; }
        public virtual ICollection<TUserCoupon> TUserCoupon { get; set; }
    }
}