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
    public class UserUpdate
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadIng { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
    }
}
