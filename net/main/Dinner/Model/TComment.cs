using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#35780;&#35770;
    /// </summary>
    [Table("t_comment")]
    public partial class TComment
    {

        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#35746;&#21333;&#32534;&#21495;
        /// </summary>
        [Column("orderid")]
        [StringLength(30)]
        public string Orderid { get; set; }

        /// <summary>
        /// &#20869;&#23481;
        /// </summary>
        [Column("msg")]
        [StringLength(200)]
        public string Msg { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("order_id")]
        [StringLength(191)]
        public string OrderId1 { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
