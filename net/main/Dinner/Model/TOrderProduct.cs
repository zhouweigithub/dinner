using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#35746;&#21333;&#20013;&#30340;&#21830;&#21697;&#20449;&#24687;
    /// </summary>
    [Table("t_order_product")]
    public partial class TOrderProduct
    {

        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("orderid")]
        [StringLength(30)]
        public string Orderid { get; set; }

        /// <summary>
        /// &#21830;&#21697;id
        /// </summary>
        [Key]
        [Column("productid")]
        public int Productid { get; set; }

        /// <summary>
        /// &#21333;&#20215;
        /// </summary>
        [Column("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// &#25968;&#37327;
        /// </summary>
        [Column("count")]
        public int? Count { get; set; }

        /// <summary>
        /// &#37329;&#39069;
        /// </summary>
        [Column("money")]
        public decimal? Money { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("order_id")]
        [StringLength(191)]
        public string OrderId1 { get; set; }
        [Column("product_id")]
        public long? ProductId1 { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
