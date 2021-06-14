using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 用户更新参数
    /// </summary>
    public class NoticeUpdate
    {
        /// <summary>
        /// 公告id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 公告生效起始日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 公告生效结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
