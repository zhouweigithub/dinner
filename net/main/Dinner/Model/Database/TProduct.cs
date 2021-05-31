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
    /// 商品
    /// </summary>
    [Table("t_product")]
    [Index(nameof(Category), Name = "category")]
    [Index(nameof(Name), Name = "ix_name", IsUnique = true)]
    public partial class TProduct
    {
        public TProduct()
        {
            TCart = new HashSet<TCart>();
        }

        /// <summary>
        /// 商品id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        [Column("name")]
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        [Column("category")]
        public int Category { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        [Column("sales")]
        public int Sales { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        [Column("img")]
        [StringLength(256)]
        public string Img { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }

        [ForeignKey(nameof(Category))]
        [InverseProperty(nameof(TCategory.TProduct))]
        public virtual TCategory CategoryNavigation { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<TCart> TCart { get; set; }
    }
}