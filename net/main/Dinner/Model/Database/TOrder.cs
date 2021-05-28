using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#35746;&#21333;&#20449;&#24687;
    /// </summary>
    [Table("t_order")]
    public partial class TOrder
    {

        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        [Column("id")]
        [StringLength(30)]
        public string Id { get; set; }

        /// <summary>
        /// &#29992;&#25143;id
        /// </summary>
        [Column("userid")]
        public int Userid { get; set; }

        /// <summary>
        /// &#35746;&#21333;&#37329;&#39069;
        /// </summary>
        [Column("money")]
        public decimal Money { get; set; }

        /// <summary>
        /// &#20248;&#24800;&#37329;&#39069;
        /// </summary>
        [Column("coupon_money")]
        public decimal CouponMoney { get; set; }

        /// <summary>
        /// &#23454;&#38469;&#25903;&#20184;&#37329;&#39069;
        /// </summary>
        [Column("pay_money")]
        public decimal PayMoney { get; set; }

        /// <summary>
        /// &#29366;&#24577;
        /// </summary>
        [Column("state")]
        public int State { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
        [Column("user_id")]
        public long? UserId1 { get; set; }
    }
}
