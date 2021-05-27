using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#21830;&#21697;
    /// </summary>
    [Table("t_product")]
    [Index(nameof(Category), Name = "category")]
    public partial class TProduct
    {

        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#21830;&#21697;&#21517;&#31216;
        /// </summary>
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// &#21830;&#21697;&#20998;&#31867;
        /// </summary>
        [Column("category")]
        public int? Category { get; set; }

        /// <summary>
        /// &#20215;&#26684;
        /// </summary>
        [Column("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// &#38144;&#37327;
        /// </summary>
        [Column("sales")]
        public int? Sales { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }

        [ForeignKey(nameof(Category))]
        [InverseProperty(nameof(TCategory.TProduct))]
        public virtual TCategory CategoryNavigation { get; set; }
    }
}
