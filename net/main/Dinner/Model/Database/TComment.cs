﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 评论
    /// </summary>
    public partial class TComment
    {
        /// <summary>
        /// 自增id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Orderid { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Crtime { get; set; }
    }
}