using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#21830;&#21697;&#20998;&#31867;
    /// </summary>
    [Table("t_category")]
    public partial class TCategory
    {
        public TCategory()
        {
            TProduct = new HashSet<TProduct>();
        }


        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#21517;&#31216;
        /// </summary>
        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// &#29366;&#24577;0&#27491;&#24120; 1&#31105;&#29992;
        /// </summary>
        [Column("state")]
        public int? State { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }

        [InverseProperty("CategoryNavigation")]
        public virtual ICollection<TProduct> TProduct { get; set; }
    }
}
