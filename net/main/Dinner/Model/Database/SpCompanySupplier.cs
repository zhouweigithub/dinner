﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Model.Database
{
    /// <summary>
    /// 公司与供货商关系
    /// </summary>
    [Table("sp_company_supplier")]
    public partial class SpCompanySupplier
    {
        /// <summary>
        /// 公司id
        /// </summary>
        [Key]
        [Column("companyid")]
        public int Companyid { get; set; }
        /// <summary>
        /// 供货商id
        /// </summary>
        [Column("suplier")]
        public int Suplier { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        [Column("start_time", TypeName = "date")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [Column("end_time", TypeName = "date")]
        public DateTime EndTime { get; set; }
    }
}