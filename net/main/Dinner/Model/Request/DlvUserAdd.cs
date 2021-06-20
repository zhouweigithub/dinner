using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 添加送货员信息
    /// </summary>
    public class DlvUserAdd
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 送货人名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 送货人地址
        /// </summary>
        public string Address { get; set; }
    }
}
