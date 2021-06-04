using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 优惠券新增参数
    /// </summary>
    public class CouponAdd
    {
        /// <summary>
        /// 优惠卷名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 使用开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 使用截止日期
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
