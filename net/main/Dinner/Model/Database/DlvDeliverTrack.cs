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
    /// 送货员送货情况追踪
    /// </summary>
    [Table("dlv_deliver_track")]
    public partial class DlvDeliverTrack
    {
        /// <summary>
        /// 送货追踪id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 送货员id
        /// </summary>
        [Column("deliverid")]
        public int Deliverid { get; set; }
        /// <summary>
        /// 详细日期
        /// </summary>
        [Column("crdate", TypeName = "date")]
        public DateTime Crdate { get; set; }
        /// <summary>
        /// 详细时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }
    }
}