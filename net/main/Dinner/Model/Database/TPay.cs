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
    /// 支付信息
    /// </summary>
    [Table("t_pay")]
    [Index(nameof(Orderid), Name = "fk_orderid2")]
    public partial class TPay
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [Column("orderid")]
        [StringLength(32)]
        public string Orderid { get; set; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        [Required]
        [Column("wx_orderid")]
        [StringLength(64)]
        public string WxOrderid { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [ForeignKey(nameof(Orderid))]
        [InverseProperty(nameof(TOrder.TPay))]
        public virtual TOrder Order { get; set; }
    }
}