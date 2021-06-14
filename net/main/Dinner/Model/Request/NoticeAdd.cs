using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{

    /// <summary>
    /// 添加公告内容
    /// </summary>
    public class NoticeAdd
    {
        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 生效起始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 生效结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
