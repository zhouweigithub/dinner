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
    /// 供货商信息
    /// </summary>
    [Table("sp_user")]
    public partial class SpUser
    {
        public SpUser()
        {
            RCompanySupplier = new HashSet<RCompanySupplier>();
            RProductSuplier = new HashSet<RProductSuplier>();
            SpException = new HashSet<SpException>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 供货商名称
        /// </summary>
        [Required]
        [Column("name")]
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 供货商地址
        /// </summary>
        [Required]
        [Column("address")]
        [StringLength(128)]
        public string Address { get; set; }
        /// <summary>
        /// 状态 0正常 1禁用
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [InverseProperty("Suplier")]
        public virtual ICollection<RCompanySupplier> RCompanySupplier { get; set; }
        [InverseProperty("Suplier")]
        public virtual ICollection<RProductSuplier> RProductSuplier { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<SpException> SpException { get; set; }
    }
}