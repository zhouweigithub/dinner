using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// &#29992;&#25143;&#21453;&#39304;
    /// </summary>
    [Table("t_feedback")]
    public partial class TFeedback
    {

        /// <summary>
        /// 自增id
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#29992;&#25143;id
        /// </summary>
        [Column("userid")]
        public int Userid { get; set; }

        /// <summary>
        /// &#20869;&#23481;
        /// </summary>
        [Required]
        [Column("msg")]
        [StringLength(300)]
        public string Msg { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime Crtime { get; set; }
        [Column("user_id")]
        public long? UserId1 { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
