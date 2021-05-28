using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#35746;&#21333;&#20013;&#20351;&#29992;&#30340;&#20248;&#24800;&#21367;
    /// </summary>
    [Table("t_order_coupon")]
    public partial class TOrderCoupon
    {

        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("orderid")]
        [StringLength(30)]
        public string Orderid { get; set; }

        /// <summary>
        /// &#20248;&#24800;&#21367;id
        /// </summary>
        [Key]
        [Column("couponid")]
        public int Couponid { get; set; }

        /// <summary>
        /// &#25968;&#37327;
        /// </summary>
        [Column("count")]
        public int? Count { get; set; }

        /// <summary>
        /// &#24635;&#37329;&#39069;
        /// </summary>
        [Column("money")]
        public decimal? Money { get; set; }
        [Column("order_id")]
        [StringLength(191)]
        public string OrderId1 { get; set; }
        [Column("coupon_id")]
        public long? CouponId1 { get; set; }
    }
}
