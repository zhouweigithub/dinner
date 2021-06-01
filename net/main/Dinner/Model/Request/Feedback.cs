using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{

    /// <summary>
    /// 用户反馈信息
    /// </summary>
    public class FeedbackAdd
    {
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string content { get; set; }
    }
}
