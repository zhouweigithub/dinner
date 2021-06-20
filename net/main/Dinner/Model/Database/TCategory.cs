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
    /// 商品分类
    /// </summary>
    [Table("t_category")]
    [Index(nameof(Name), Name = "ix_name", IsUnique = true)]
    public partial class TCategory
    {
        public TCategory()
        {
            TProduct = new HashSet<TProduct>();
        }

        /// <summary>
        /// 商品分类id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品分类名称
        /// </summary>
        [Required]
        [Column("name")]
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 状态0正常 1禁用
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<TProduct> TProduct { get; set; }
    }
}