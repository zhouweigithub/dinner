using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#24494;&#20449;&#25903;&#20184;&#22238;&#35843;
    /// </summary>
    [Table("t_order_callback")]
    public partial class TOrderCallback
    {

        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("orderid")]
        [StringLength(30)]
        public string Orderid { get; set; }

        /// <summary>
        /// &#24494;&#20449;&#25903;&#20184;&#35746;&#21333;&#21495;
        /// </summary>
        [Column("wx_orderid")]
        [StringLength(30)]
        public string WxOrderid { get; set; }

        /// <summary>
        /// &#29366;&#24577;
        /// </summary>
        [Column("state")]
        public int? State { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("order_id")]
        [StringLength(191)]
        public string OrderId1 { get; set; }
        [Column("wx_order_id")]
        [StringLength(191)]
        public string WxOrderId1 { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
