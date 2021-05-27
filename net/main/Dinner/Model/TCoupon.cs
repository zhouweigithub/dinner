using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#20248;&#24800;&#21367;&#20449;&#24687;
    /// </summary>
    [Table("t_coupon")]
    public partial class TCoupon
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#20248;&#24800;&#21367;&#21517;&#31216;
        /// </summary>
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// &#20248;&#24800;&#37329;&#39069;
        /// </summary>
        [Column("money")]
        public decimal Money { get; set; }

        /// <summary>
        /// &#20351;&#29992;&#24320;&#22987;&#26085;&#26399;
        /// </summary>
        [Column("start_time", TypeName = "datetime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// &#20351;&#29992;&#25130;&#27490;&#26085;&#26399;
        /// </summary>
        [Column("end_time", TypeName = "datetime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// &#20248;&#24800;&#21367;&#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "timestamp")]
        public DateTime Crtime { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
