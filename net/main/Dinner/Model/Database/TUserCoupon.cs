using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#29992;&#25143;&#30340;&#20248;&#24800;&#21367;
    /// </summary>
    [Table("t_user_coupon")]
    public partial class TUserCoupon
    {

        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        [Column("userid")]
        public int Userid { get; set; }

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
        [Column("coupon_id")]
        public long? CouponId1 { get; set; }
        [Column("user_id")]
        public long? UserId1 { get; set; }
    }
}
