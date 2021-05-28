using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    /// <summary>
    /// &#20844;&#21496;
    /// </summary>
    [Table("t_company")]
    [Index(nameof(Code), Name = "IX_CODE", IsUnique = true)]
    [Index(nameof(Name), Name = "IX_NAME", IsUnique = true)]
    public partial class TCompany
    {

        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// &#20844;&#21496;&#21517;&#23383;
        /// </summary>
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// &#21807;&#19968;&#32534;&#30721;
        /// </summary>
        [Column("code")]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// &#20844;&#21496;&#22320;&#22336;
        /// </summary>
        [Column("address")]
        [StringLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// &#21019;&#24314;&#26102;&#38388;
        /// </summary>
        [Column("crtime", TypeName = "datetime")]
        public DateTime? Crtime { get; set; }
        [Column("cr_time", TypeName = "datetime")]
        public DateTime? CrTime1 { get; set; }
    }
}
