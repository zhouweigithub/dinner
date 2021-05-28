using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// &#29992;&#25143;
    /// </summary>
    [Table("t_user")]
    public partial class TUser
    {

        /// <summary>
        /// 自增ID
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#21807;&#19968;&#32534;&#30721;
        /// </summary>
        [Required]
        [Column("code")]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// &#20844;&#21496;id
        /// </summary>
        [Column("companyid")]
        public int Companyid { get; set; }

        /// <summary>
        /// &#26165;&#31216;
        /// </summary>
        [Column("nick")]
        [StringLength(20)]
        public string Nick { get; set; }

        public string HeadImg { get; set; }

        public string Phone { get; set; }


        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
    }
}
